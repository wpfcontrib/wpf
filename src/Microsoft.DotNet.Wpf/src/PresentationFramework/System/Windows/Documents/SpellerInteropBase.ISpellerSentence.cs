// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace System.Windows.Documents
{
    using System.Collections.Generic;

    internal abstract partial class SpellerInteropBase
    {
        /// <summary>
        /// Represents the spell-checker's notion of a 'sentence', which is in turn made 
        /// up of 'segments' (words) and 'sub-segments'
        /// </summary>
        internal interface ISpellerSentence
        {
            string SourceText { get; }

            IReadOnlyList<ISpellerSegment> Segments { get; }
            
            /// <summary>
            /// Returns the final symbol offset of a sentence.
            /// </summary>
            int EndOffset { get; }
        }
    }
}
