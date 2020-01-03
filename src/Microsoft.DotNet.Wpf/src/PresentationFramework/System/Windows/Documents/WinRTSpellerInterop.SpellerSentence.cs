// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Windows.Documents
{
    using MS.Internal.WindowsRuntime.Windows.Data.Text;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Documents.Tracing;

    using System.Windows.Documents.MsSpellCheckLib;

    internal partial class WinRTSpellerInterop
    {
        [DebuggerDisplay("Sentence = {_sentence}")]
        private class SpellerSentence: ISpellerSentence
        {
            public SpellerSentence(string sourceText, string sentence, WordsSegmenter wordBreaker, SpellChecker spellChecker, WinRTSpellerInterop owner)
            {
                _sentence = sentence;
                _wordBreaker = wordBreaker;
                _spellChecker = spellChecker;
                _segments = null;
                _owner = owner;

                SourceText = sourceText;
            }

            #region SpellerInteropBase.ISpellerSentence

            /// <inheritdoc/>
            public string SourceText { get; }

            public IReadOnlyList<ISpellerSegment> Segments
            {
                get
                {
                    if (_segments == null)
                    {
                        _segments = _wordBreaker.ComprehensiveGetTokens(_sentence, _spellChecker, _owner);
                    }

                    return _segments;
                }
            }

            public int EndOffset
            {
                get
                {
                    int endOffset = -1;

                    if (Segments.Count > 0)
                    {
                        ITextRange textRange = Segments[Segments.Count - 1].TextRange;
                        endOffset = textRange.Start + textRange.Length;
                    }

                    return endOffset;
                }
            }

            #endregion

            private string _sentence;
            private WordsSegmenter _wordBreaker;
            private SpellChecker  _spellChecker;
            private IReadOnlyList<SpellerSegment> _segments;

            /// <remarks>
            /// This field is used only to support TraceLogging telemetry
            /// logged using <see cref="SpellerCOMActionTraceLogger"/>. It
            /// has no other functional use.
            /// </remarks>
            private WinRTSpellerInterop _owner;
        }
    }
}
