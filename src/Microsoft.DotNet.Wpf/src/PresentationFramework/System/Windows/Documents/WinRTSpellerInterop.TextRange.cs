// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Windows.Documents
{
    internal partial class WinRTSpellerInterop
    {
        private struct TextRange: SpellerInteropBase.ITextRange
        {
            public TextRange(MS.Internal.WindowsRuntime.Windows.Data.Text.TextSegment textSegment)
            {
                _length = (int)textSegment.Length;
                _start = (int)textSegment.StartPosition;
            }

            public static explicit operator TextRange(MS.Internal.WindowsRuntime.Windows.Data.Text.TextSegment textSegment)
            {
                return new TextRange(textSegment);
            }

            #region SpellerInteropBase.ITextRange

            public int Start
            {
                get { return _start;  }
            }

            public int Length
            {
                get { return _length; }
            }

            #endregion

            private readonly int _start;
            private readonly int _length;
        }
    }
}
