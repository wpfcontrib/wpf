// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace System.Windows.Documents
{
    internal partial class NLGSpellerInterop
    {
        /// <summary>
        /// RangeRole enum defined by NLG API's
        /// </summary>
        private enum RangeRole
        {
            ecrrSimpleSegment = 0,
            ecrrAlternativeForm = 1,
            ecrrIncorrect = 2,
            ecrrAutoReplaceForm = 3,
            ecrrCorrectForm = 4,
            ecrrPreferredForm = 5,
            ecrrNormalizedForm = 6,
            ecrrCompoundSegment = 7,
            ecrrPhraseSegment = 8,
            ecrrNamedEntity = 9,
            ecrrCompoundWord = 10,
            ecrrPhrase = 11,
            ecrrUnknownWord = 12,
            ecrrContraction = 13,
            ecrrHyphenatedWord = 14,
            ecrrContractionSegment = 15,
            ecrrHyphenatedSegment = 16,
            ecrrCapitalization = 17,
            ecrrAccent = 18,
            ecrrRepeated = 19,
            ecrrDefinition = 20,
            ecrrOutOfContext = 21,
        };
    }
}

