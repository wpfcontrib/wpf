// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//
// Description: Custom COM marshalling code and interfaces for interaction
//              with the Natural Language Group's nl6 proofing engine.
//

namespace System.Windows.Documents
{
    using System.Runtime.InteropServices;

    internal partial class NLGSpellerInterop
    {
        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("C090356B-A6A5-442a-A204-CFD5415B5902")]
        private interface IProcessingOptions
        {
            // /* [propget][restricted][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get__NewEnum )(
            //     IProcessingOptions * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get__NewEnum();

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *GetEnumerator )(
            //     IProcessingOptions * This,
            //     /* [retval][out] */ IEnumVARIANT **ppSent);
            void stub_GetEnumerator();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Locale )(
            //     IProcessingOptions * This,
            //     /* [ref][retval][out] */ LCID *pval);
            void stub_get_Locale();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Count )(
            //     IProcessingOptions * This,
            //     /* [ref][retval][out] */ long *pval);
            void stub_get_Count();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Name )(
            //     IProcessingOptions * This,
            //     /* [in] */ long index,
            //     /* [ref][retval][out] */ BSTR *pval);
            void stub_get_Name();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Item )(
            //     IProcessingOptions * This,
            //     /* [in] */ VARIANT index,
            //     /* [ref][retval][out] */ VARIANT *pval);
            void stub_get_Item();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_Item )(
            //     IProcessingOptions * This,
            //     /* [in] */ VARIANT index,
            //     /* [in] */ VARIANT val);
            void put_Item(object index, object val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsReadOnly )(
            //     IProcessingOptions * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsReadOnly();
        }
    }
}

