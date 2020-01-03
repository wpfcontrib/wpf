// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace System.Windows.Documents
{
    using System.Collections.Generic;

    internal abstract partial class SpellerInteropBase
    {
        /// <summary>
        /// Represents the spell-checkers notion of a 'word'
        /// </summary>
        internal interface ISpellerSegment
        {
            /// <summary>
            ///  Source String for which <see cref="TextRange"/> provides a position.
            /// </summary>
            string SourceString { get;  }

            /// <summary>
            /// Identifies sub-words, if any. 
            /// </summary>
            IReadOnlyList<ISpellerSegment> SubSegments { get; }

            /// <summary>
            /// Obtains the position of this segment in its source text string
            /// </summary>
            ITextRange TextRange { get; }

            /// <summary>
            /// Text represented by <see cref="TextRange"/>
            /// </summary>
            string Text => SourceString.Substring(TextRange.Start, TextRange.Length);

            /// <summary>
            /// Gets the alternate forms, if any, associated with the current word. 
            /// </summary>
            /// <remarks>
            /// Alternate forms are loosely associated with the current word. An alternate form may encompass less text, more text, or the same text as the original word.
            /// </remarks>
            IReadOnlyList<string> AlternateForms { get; }

            /// <summary>
            /// Queries the spell-checker to obtain suggestions for this segment
            /// </summary>
            IReadOnlyList<string> Suggestions { get; }

            /// <summary>
            /// Returns true if the segment has no spelling erorrs
            /// </summary>
            bool IsClean { get; }

            /// <summary>
            /// Enumerates a segment's subsegments, making a callback on each iteration.
            /// </summary>
            /// <param name="segmentCallback"></param>
            /// <param name="data"></param>
            void EnumSubSegments(EnumTextSegmentsCallback segmentCallback, object data);

            /// <summary>
            /// Register one alternate form. <paramref name="alternateForm"/> should be 
            /// an index into the same <see cref="SourceString"/>, and <see cref="TextRange"/> should
            /// be different from that of <paramref name="alternateForm"/>.
            /// </summary>
            /// <param name="alternateForm"></param>
            void RegisterAlternateForm(ISpellerSegment alternateForm);

            /// <summary>
            /// Register a set of alternate forms created by providing a suffix string to the current segment.
            /// The suffix string will be permuted to create strings of different lengths, that would in turn 
            /// be concatenated with <see cref="Text"/> to create new alternate-forms.
            /// </summary>
            /// <remarks>
            /// The <paramref name="suffix"/> should be contiguous with the current string, and must represent
            /// the same <see cref="SourceString"/>.
            /// </remarks>
            /// <param name="suffix"></param>
            void RegisterAlternateForms(ISpellerSegment suffix);
        }
    }
}
