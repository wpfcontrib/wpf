// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Windows.Documents
{
    using System.Runtime.InteropServices;
    using MS.Win32;
    using System.Collections.Generic;

    internal partial class NLGSpellerInterop
    {
        /// <summary>
        /// Implementation of ISpellerSegment that manages the lifetime of 
        /// an ITextSegment (NLG COM interface) object
        /// </summary>
        [System.Diagnostics.DebuggerDisplay("{DebuggerDisplay}")]
        private class SpellerSegment : ISpellerSegment, IDisposable
        {
            #region Constructor 

            public SpellerSegment(string sourceString, ITextSegment textSegment)
            {
                _textSegment = textSegment;
                SourceString = sourceString;
            }

            #endregion Constructor

            #region Private Methods

            /// <summary>
            /// Enumerates spelling suggestions for this segment
            /// </summary>
            private void EnumerateSuggestions()
            {
                List<string> suggestions = new List<string>();

                UnsafeNativeMethods.IEnumVariant variantEnumerator;

                _textSegment.get_Suggestions(out variantEnumerator);

                if (variantEnumerator == null)
                {
                    // nl6 will return null enum instead of an empty enum.
                    _suggestions = suggestions.AsReadOnly();
                    return;
                }

                try
                {
                    NativeMethods.VARIANT variant = new NativeMethods.VARIANT();
                    int[] fetched = new int[1];

                    while (true)
                    {
                        int result;

                        variant.Clear();
                        result = EnumVariantNext(variantEnumerator, variant, fetched);

                        if ((result != NativeMethods.S_OK) || (fetched[0] == 0))
                        {
                            break;
                        }

                        // Convert the VARIANT to string, and add it to our list.
                        // There's some special magic here.  The VARIANT is VT_UI2/ByRef.
                        // But under the hood it's really a raw WCHAR *.
                        suggestions.Add(Marshal.PtrToStringUni(variant.data1.Value));
                    }
                }
                finally
                {
                    Marshal.ReleaseComObject(variantEnumerator);
                }

                _suggestions = suggestions.AsReadOnly();
                return;
            }

            /// <summary>
            /// Enumerates sub-segments of this segment
            /// </summary>
            private void EnumerateSubSegments()
            {
                _textSegment.get_Count(out _subSegmentCount);

                List<ISpellerSegment> subSegments = new List<ISpellerSegment>();

                for (int i = 0; i < _subSegmentCount; i++)
                {
                    ITextSegment subSegment;
                    _textSegment.get_Item(i, out subSegment);

                    // subSegment COM object will get released by SpellerSegment's finalizer
                    subSegments.Add(new SpellerSegment(SourceString, subSegment));
                }

                _subSegments = subSegments.AsReadOnly();
            }

            #endregion

            #region SpellerInteropBase.ISpellerSegment

            /// <inheritdoc/>
            public string SourceString { get; }

            /// <summary>
            /// Returns a read-only list of sub-segments of this segment
            /// </summary>
            public IReadOnlyList<ISpellerSegment> SubSegments
            {
                get
                {
                    if (_subSegments == null)
                    {
                        EnumerateSubSegments();
                    }

                    return _subSegments;
                }
            }

            /// <summary>
            /// Identifies, by position, this segment in it's source sentence
            /// </summary>
            public ITextRange TextRange
            {
                get
                {
                    if (_sTextRange == null)
                    {
                        STextRange sTextRange;
                        _textSegment.get_Range(out sTextRange);

                        _sTextRange = sTextRange;
                    }

                    return _sTextRange.Value;
                }
            }

            /// <inheritdoc/>
            /// <remarks>
            /// NLG based speller does not support this property
            /// </remarks>
            public IReadOnlyList<string> AlternateForms => null;

            /// <inheritdoc/>
            public string Text => SourceString.Substring(TextRange.Start, TextRange.Length);

            /// <summary>
            /// Generates spelling suggestions for this segment
            /// If the segment has no suggestions (usually because it is not misspelled,
            /// but also possible for errors the engine cannot make sense of, or that are
            /// contained in sub-segments), this method returns an empty list
            /// </summary>
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

            /// <summary>
            /// Checks whether this segment is free of spelling errors
            /// </summary>
            public bool IsClean 
            {
                get
                {
                    return (RangeRole != RangeRole.ecrrIncorrect);
                }
            }

            /// <summary>
            /// Enumerates a segment's subsegments, making a callback on each iteration.
            /// </summary>
            /// <param name="segmentCallback"></param>
            /// <param name="data"></param>
            public void EnumSubSegments(EnumTextSegmentsCallback segmentCallback, object data)
            {
                bool result = true;

                // Walk the subsegments, the error's in there somewhere.
                for (int i = 0; result && (i < SubSegments.Count); i++)
                {
                    result = segmentCallback(SubSegments[i], data);
                }
            }

            /// <inheritdoc/>
            public void RegisterAlternateForm(ISpellerSegment alternateForm)
            {
                // Do nothing - not supported for NLG based speller
            }

            /// <inheritdoc/>
            public void RegisterAlternateForms(ISpellerSegment suffix)
            {
                // Do nothing - not supported for NLG based speller
            }

            #endregion SpellerInteropBase.ISpellerSegment

            #region IDisposable

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException("NLGSpellerInterop.SpellerSegment");
                }

                if (_subSegments != null)
                {
                    foreach (SpellerSegment subSegment in _subSegments)
                    {
                        // Don't call Dispose(disposing) here. That will 
                        // fail to suppress finalization of subsegment objects.
                        subSegment.Dispose();
                    }
                    _subSegments = null;
                }

                if (_textSegment != null)
                {
                    Marshal.ReleaseComObject(_textSegment);
                    _textSegment = null;
                }

                _disposed = true;
            }

            ~SpellerSegment()
            {
                Dispose(false);
            }

            #endregion

            #region Public Properties

            public RangeRole RangeRole
            {
                get
                {
                    if (_rangeRole == null)
                    {
                        RangeRole role;
                        _textSegment.get_Role(out role);

                        _rangeRole = role;
                    }

                    return _rangeRole.Value;
                }
            }


            #endregion Public Properties

            #region Private Properties

            /// <summary>
            /// Debugger Display String
            /// </summary>
            private string DebuggerDisplay
            {
                get
                {
                    var debuggerDisplay = Text;
                    var altStrings = string.Join(", ", AlternateForms ?? System.Linq.Enumerable.Empty<string>());
                    if (!string.IsNullOrWhiteSpace(altStrings))
                    {
                        debuggerDisplay += $"({altStrings})";
                    }

                    return debuggerDisplay;
                }
            }

            #endregion Private Properties

            #region Private Fields

            // SpellerInteropBase fields
            private STextRange? _sTextRange = null;
            private int _subSegmentCount;
            private IReadOnlyList<ISpellerSegment> _subSegments = null;
            private IReadOnlyList<string> _suggestions = null;

            // SpellerSegment specific fields
            private RangeRole? _rangeRole = null;
            private ITextSegment _textSegment;

            // IDisposable management
            private bool _disposed = false;

            #endregion Private Fields
        }
    }
}

