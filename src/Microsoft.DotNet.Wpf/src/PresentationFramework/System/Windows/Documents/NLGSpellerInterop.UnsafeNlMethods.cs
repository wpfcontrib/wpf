// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Windows.Documents
{
    using System.Runtime.InteropServices;
    using MS.Internal.PresentationFramework;

    internal partial class NLGSpellerInterop
    {
        private static class UnsafeNlMethods
        {
            [DllImport(DllImport.PresentationNative, PreserveSig = false)]
            internal static extern void NlLoad();

            [DllImport(DllImport.PresentationNative, PreserveSig = true)]
            internal static extern void NlUnload();

            [DllImport(DllImport.PresentationNative, PreserveSig = false)]
            internal static extern void NlGetClassObject(ref Guid clsid, ref Guid iid, [MarshalAs(UnmanagedType.Interface)] out object classObject);
        }
    }
}

