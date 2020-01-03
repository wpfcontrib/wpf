// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Windows.Documents
{
    using MS.Internal;
    using MS.Internal.WindowsRuntime.Windows.Data.Text;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents.Tracing;
    using System.Windows.Input;
    using System.Windows.Threading;

    using System.Windows.Documents.MsSpellCheckLib;

    internal partial class WinRTSpellerInterop
    {

        internal static class Extensions
        {
            //public static List<SpellerWordSegment> ComprehensiveGetTokens(this WordsSegmenter segmenter, string sentence)
            //{
            //    var tokens = segmenter.GetTokens(sentence);
            //    var updatedTokens = new List<SpellerWordSegment>();

            //    int predictedNextTokenStartPosition = 0;
            //    for (int i = 0; i < tokens.Count; i++)
            //    {
            //        int nextTokenStartPosition = (int)tokens[i].SourceTextSegment.StartPosition;
            //        if (nextTokenStartPosition > predictedNextTokenStartPosition)
            //        {
            //            // There is a "gap" between the last recorded token and the current token.
            //            // Identify the missing token and add it as a "suplementary word segment" - but only if the token
            //            // turns out to be a substantial one (i.e., if the string is non-blank/non-empty). 

            //        }
            //    }
            //}
        }
    }
}
