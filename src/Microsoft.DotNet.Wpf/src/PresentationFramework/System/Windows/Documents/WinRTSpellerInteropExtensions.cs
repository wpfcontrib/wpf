// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MS.Internal.WindowsRuntime.Windows.Data.Text;
using System.Collections.Generic;

using System.Windows.Documents.MsSpellCheckLib;
using System.Windows.Documents.Tracing;
using static System.Windows.Documents.WinRTSpellerInterop;

namespace System.Windows.Documents
{
    internal static class WinRTSpellerInteropExtensions
    {
        public static IReadOnlyList<SpellerSegment> ComprehensiveGetTokens(
            this WordsSegmenter segmenter, 
            string text, 
            SpellChecker spellChecker, 
            WinRTSpellerInterop owner)
        {
            var tokens = segmenter.GetTokens(text);
            var allTokens = new List<SpellerSegment>();

            int predictedNextTokenStartPosition = 0;
            for (int i = 0; i < tokens.Count; i++)
            {
                int nextTokenStartPosition = (int)tokens[i].SourceTextSegment.StartPosition;
                if (nextTokenStartPosition > predictedNextTokenStartPosition)
                {
                    // There is a "gap" between the last recorded token and the current token.
                    // Identify the missing token and add it as a "suplementary word segment" - but only if the token
                    // turns out to be a substantial one (i.e., if the string is non-blank/non-empty). 
                    var missingToken = 
                        new SpellerSegment(
                            text, 
                            new WinRTSpellerInterop.TextRange(
                                predictedNextTokenStartPosition, 
                                nextTokenStartPosition - predictedNextTokenStartPosition), 
                            spellChecker, 
                            owner); 
                    if (!string.IsNullOrWhiteSpace(missingToken.Text) && 
                        allTokens.Count > 0)
                    {
                        allTokens[allTokens.Count - 1].RegisterAlternateForms(missingToken);
                    }
                }

                allTokens.Add(new SpellerSegment(text, tokens[i], spellChecker, owner));
                predictedNextTokenStartPosition = (int)(tokens[i].SourceTextSegment.StartPosition + tokens[i].SourceTextSegment.Length);
            }

            if (predictedNextTokenStartPosition < text.Length)
            {
                // There is a token possibly missing at the end of the string
                var missingToken =
                    new SpellerSegment(
                        text,
                        new WinRTSpellerInterop.TextRange(
                            predictedNextTokenStartPosition,
                            text.Length - predictedNextTokenStartPosition),
                        spellChecker,
                        owner);
                if (!string.IsNullOrWhiteSpace(missingToken.Text) && 
                    allTokens.Count > 0)
                {
                    allTokens[allTokens.Count - 1].RegisterAlternateForms(missingToken);
                }
            }

            return allTokens.AsReadOnly();
        }

        internal static IReadOnlyList<SpellChecker.SpellingError> ComprehensiveCheckWithAlternateForms(this SpellChecker speller, SpellerSegment segment)
        {
            var spellingErrors = new List<SpellChecker.SpellingError>();

            using (new SpellerCOMActionTraceLogger(segment.Owner, SpellerCOMActionTraceLogger.Actions.ComprehensiveCheck))
            {
                spellingErrors.AddRange(speller.ComprehensiveCheck(segment.Text));
                if (spellingErrors.Count > 0 && segment.AlternateForms?.Count > 0)
                {
                    // Spelling errors were found and there are alternate-forms of the word that can be tested 
                    foreach (var alternateForm in segment.AlternateForms)
                    {
                        var altFormSpellingErrors = speller.ComprehensiveCheck(alternateForm);
                        if (altFormSpellingErrors?.Count == 0)
                        {
                            // Found a variant/alternate-form of the word that is not tagged as a spelling-error
                            // Clear out the error list and stop searching
                            spellingErrors.Clear();
                            break;
                        }
                    }
                }
            }

            return spellingErrors.AsReadOnly();
        }
    }
}
