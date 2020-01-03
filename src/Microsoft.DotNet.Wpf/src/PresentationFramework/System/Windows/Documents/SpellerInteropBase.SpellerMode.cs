// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace System.Windows.Documents
{
    internal abstract partial class SpellerInteropBase
    {
        [Flags]
        internal enum SpellerMode
        {
            None                          = 0x0000,
            WordBreaking                  = 0x0001, 
            SpellingErrors                = 0x0002, 
            Suggestions                   = 0x0004,
            SpellingErrorsWithSuggestions = SpellingErrors | Suggestions, 
            All                           = WordBreaking | SpellingErrorsWithSuggestions,
        };
    }
}
