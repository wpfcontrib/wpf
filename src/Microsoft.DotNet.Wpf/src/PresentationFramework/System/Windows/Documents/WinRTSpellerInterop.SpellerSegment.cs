// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Windows.Documents
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Documents.Tracing;

    using System.Windows.Documents.MsSpellCheckLib;
    using MS.Internal.WindowsRuntime.Windows.Data.Text;

    internal partial class WinRTSpellerInterop
    {
        [DebuggerDisplay("{SourceString.Substring(TextRange.Start, TextRange.Length)}; SubSegments.Count = {SubSegments.Count}")]
        internal class SpellerSegment: ISpellerSegment
        {
            #region Constructor

            public SpellerSegment(string sourceString, WordSegment segment, SpellChecker spellChecker, WinRTSpellerInterop owner)
            {
                _segment = segment;
                _spellChecker = spellChecker;
                _suggestions = null;
                _owner = owner;
                SourceString = sourceString;
            }

            public SpellerSegment(string sourceString, ITextRange textRange, SpellChecker spellChecker, WinRTSpellerInterop owner): 
                this(sourceString, (WordSegment)null, spellChecker, owner)
            {
                _textRange = new TextRange(textRange);
            }

            static SpellerSegment()
            {
                _empty = new List<ISpellerSegment>().AsReadOnly();
            }

            #endregion

            #region Private Methods

            private void EnumerateSuggestions()
            {
                List<string> result = new List<string>();
                _isClean = true;

                if (_spellChecker == null)
                {
                    _suggestions = result.AsReadOnly();
                    return;
                }

                //List<SpellChecker.SpellingError> spellingErrors = null;

                //using (new SpellerCOMActionTraceLogger(_owner, SpellerCOMActionTraceLogger.Actions.ComprehensiveCheck))
                //{
                //    spellingErrors = _spellChecker.ComprehensiveCheck(_segment.Text);
                //}

                var spellingErrors = _spellChecker.ComprehensiveCheckWithAlternateForms(this);

                if (spellingErrors == null)
                {
                    _suggestions = result.AsReadOnly();
                    return;
                }

                foreach (var spellingError in spellingErrors)
                {
                    result.AddRange(spellingError.Suggestions);
                    if (spellingError.CorrectiveAction != SpellChecker.CorrectiveAction.None)
                    {
                        _isClean = false;
                    }
                }

                _suggestions = result.AsReadOnly();
            }

            #endregion

            #region SpellerInteropBase.ISpellerSegment

            /// <inheritdoc/>
            public string SourceString { get; }


            /// <inheritdoc/>
            public string Text => _segment?.Text ?? SourceString?.Substring(TextRange.Start, TextRange.Length);

            /// <summary>
            /// Returns a read-only list of sub-segments of this segment
            /// WinRT word-segmenter doesn't really support sub-segments,
            ///   so we always return an empty list
            /// </summary>
            public IReadOnlyList<ISpellerSegment> SubSegments
            {
                get
                {
                    return SpellerSegment._empty;
                }
            }

            public ITextRange TextRange
            {
                get
                {
                    if (_textRange == null)
                    {
                        _textRange = new TextRange(_segment.SourceTextSegment);
                    }

                    return _textRange;
                }
            }

            /// <inheritdoc />
            public IReadOnlyList<string> AlternateForms
            {
                get
                {
                    if (_alternateForms == null)
                    {
                        var variants = new HashSet<string>(StringComparer.Ordinal);

                        if (_segment != null)
                        {
                            foreach (var altForm in _segment.AlternateForms)
                            {
                                if (!string.IsNullOrWhiteSpace(altForm?.AlternateText))
                                {
                                    variants.Add(altForm.AlternateText);
                                }
                            }
                        }

                        lock (_altFormsLock)
                        {
                            _alternateForms = new List<string>(variants).AsReadOnly();
                        }
                    }

                    return _alternateForms?.Count > 0 ? _alternateForms: null;
                }
            }

            public IReadOnlyList<string> Suggestions
            {
                get
                {
                    if (_suggestions == null)
                    {
                        EnumerateSuggestions();
                    }

                    return _suggestions;
                }
            }

            public bool IsClean
            {
                get
                {
                    if (_isClean == null)
                    {
                        EnumerateSuggestions();
                    }

                    return _isClean.Value;
                }
            }

            internal WinRTSpellerInterop Owner => _owner;

            public void EnumSubSegments(EnumTextSegmentsCallback segmentCallback, object data)
            {
                bool result = true;

                for (int i = 0; result && (i < SubSegments.Count); i++)
                {
                    result = segmentCallback(SubSegments[i], data);
                }
            }

            /// <inheritdoc/>
            public void RegisterAlternateForm(ISpellerSegment alternateForm)
            {
                // First, verify that the alternate form for the same source-text.
                if (!string.Equals(alternateForm.SourceString, SourceString, StringComparison.Ordinal))
                {
                    Trace.WriteLine($"Error: Not adding alternate-form: {nameof(alternateForm)}.{nameof(SourceString)}={alternateForm.SourceString} does not represent {nameof(SourceString)}={SourceString}");
                    return;
                }

                var altForms =
                    AlternateForms != null ?
                    new HashSet<string>(AlternateForms, StringComparer.Ordinal) :
                    new HashSet<string>(StringComparer.Ordinal);

                altForms.Add(SourceString.Substring(alternateForm.TextRange.Start, alternateForm.TextRange.Length).TrimEnd()); // Trim trailing whitespace

                lock (_altFormsLock)
                {
                    _alternateForms = new List<string>(altForms).AsReadOnly();
                }
            }

            /// <inheritdoc/>
            public void RegisterAlternateForms(ISpellerSegment suffix)
            {
                // Verify that 'suffix' represents the same source-string. 
                if (!string.Equals(suffix.SourceString, SourceString, StringComparison.Ordinal))
                {
                    Trace.WriteLine($"Error: [{nameof(RegisterAlternateForm)}]: Not adding alternate-forms: {nameof(suffix)}.{nameof(SourceString)}={suffix.SourceString} does not represent {nameof(SourceString)}={SourceString}");
                    return;
                }

                // Verify that the current substring and 'suffix' are contiguous
                if (TextRange.Start + TextRange.Length != suffix.TextRange.Start)
                {
                    Trace.WriteLine($"Error: [{nameof(RegisterAlternateForms)}]: Not adding alternate forms: {nameof(suffix)}.{nameof(TextRange)} is not contiguous with the current segment.");
                    Trace.WriteLine($"Error: [{nameof(RegisterAlternateForms)}]: \tTextRange.Start + TextRange.Length = {TextRange.Start + TextRange.Length}; suffix.TextRange.Start = {suffix.TextRange.Start}");
                    return;
                }

                var altForms =
                    AlternateForms != null ?
                    new HashSet<string>(AlternateForms, StringComparer.Ordinal) :
                    new HashSet<string>(StringComparer.Ordinal);

                for (int l = 1; l <= suffix.TextRange.Length; l++)
                {
                    altForms.Add(SourceString.Substring(TextRange.Start, TextRange.Length + l).TrimEnd());
                }

                lock (_altFormsLock)
                {
                    _alternateForms = new List<string>(altForms).AsReadOnly();
                }
            }

            #endregion SpellerInteropBase.ISpellerSegment

            #region Private Fields

            private WordSegment _segment;

            SpellChecker _spellChecker;
            private IReadOnlyList<string> _suggestions;
            private bool? _isClean = null;

            private static readonly IReadOnlyList<ISpellerSegment> _empty;

            private static object _altFormsLock = new object();
            private IReadOnlyList<string> _alternateForms = null;

            /// <remarks>
            /// This field is used only to support TraceLogging telemetry
            /// logged using <see cref="SpellerCOMActionTraceLogger"/>. It
            /// has no other functional use.
            /// </remarks>
            private WinRTSpellerInterop _owner;
            private ITextRange _textRange;

            #endregion Private Fields
        }

    }
}
