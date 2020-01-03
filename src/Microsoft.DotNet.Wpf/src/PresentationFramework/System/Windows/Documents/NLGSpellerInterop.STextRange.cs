// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Windows.Documents
{
    using System.Runtime.InteropServices;

    internal partial class NLGSpellerInterop
    {

        /// <summary>
        /// ITextRange implementation compatible with NLG API's
        ///  typedef struct STextRange
        /// {
        ///     long Start;
        ///     long Length;
        /// };
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct STextRange : SpellerInteropBase.ITextRange
        {
            #region SpellerInteropBase.ITextRange

            public int Start
            {
                get { return _start; }
            }

            public int Length
            {
                get { return _length; }
            }

            #endregion SpellerInteropBase.ITextRange

            private readonly int _start;
            private readonly int _length;
        }
    }
}

