// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace System.Windows.Documents
{
    internal abstract partial class SpellerInteropBase
    {
        /// <summary>
        /// Identifies, by position, a sub-string of a source text string
        /// </summary>
        internal interface ITextRange
        {
            int Start { get; }
            int Length { get; }
        }
    }
}
