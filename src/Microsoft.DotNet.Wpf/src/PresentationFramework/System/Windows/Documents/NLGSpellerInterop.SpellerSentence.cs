// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Windows.Documents
{
    using System.Runtime.InteropServices;
    using MS.Internal;
    using System.Collections.Generic;

    internal partial class NLGSpellerInterop
    {
        /// <summary>
        /// Implementation of ISpellerSentence that manages the lifetime of
        /// an ISentence (NLG COM interface) object
        /// </summary>
        private class SpellerSentence : ISpellerSentence, IDisposable
        {
            public string SourceText { get; }
            /// <summary>
            /// Constructs a SpellerSentence object 
            /// </summary>
            /// <param name="sentence"></param>
            public SpellerSentence(string text, ISentence sentence)
            {
                _disposed = false;

                SourceText = text;

                try
                {
                    int sentenceSegmentCount;
                    sentence.get_Count(out sentenceSegmentCount);

                    // Iterate over segments.
                    List<ISpellerSegment> segments = new List<ISpellerSegment>();

                    for (int i = 0; i < sentenceSegmentCount; i++)
                    {
                        NLGSpellerInterop.ITextSegment textSegment;
                        sentence.get_Item(i, out textSegment);

                        // SpellerSegment finalizer will take care of releasing the COM object
                        segments.Add(new SpellerSegment(text, textSegment));
                    }

                    _segments = segments.AsReadOnly();

                    Invariant.Assert(_segments.Count == sentenceSegmentCount);
                }
                finally
                {
                    Marshal.ReleaseComObject(sentence);
                }
            }

            #region SpellerInteropBase.ISpellerSentence

            /// <summary>
            /// Segments that this sentence is comprised of
            /// </summary>
            public IReadOnlyList<ISpellerSegment> Segments
            {
                get
                {
                    return _segments;
                }
            }

            /// <summary>
            /// Final symbol offset of a sentence.
            /// </summary>
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

            #endregion SpellerInteropBase.ISpellerSentence

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
                    throw new ObjectDisposedException("NLGSpellerInterop.SpellerSentence");
                }

                if (_segments != null)
                {
                    foreach (SpellerSegment segment in _segments)
                    {
                        segment.Dispose();
                    }

                    _segments = null;
                }

                _disposed = true;                
            }

            ~SpellerSentence()
            {
                Dispose(false);
            }

            #endregion

            #region Private Fields

            private IReadOnlyList<ISpellerSegment> _segments;
            private bool _disposed;

            #endregion Private Fields
        }
    }
}

