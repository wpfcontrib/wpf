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
        [Guid("549F997E-0EC3-43d4-B443-2BF8021010CF")]
        private interface ITextChunk
        {
            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_InputText )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ BSTR *pval);
            void stub_get_InputText();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_InputText )(
            //     ITextChunk * This,
            //     /* [in] */ BSTR val);
            void stub_put_InputText();

            // /* [restricted][helpstring] */ HRESULT ( STDMETHODCALLTYPE *SetInputArray )(
            //     ITextChunk * This,
            //     /* [string][in] */ LPCWSTR str,
            //     /* [in] */ long size);
            void SetInputArray([In] IntPtr inputArray, Int32 size);

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *RegisterEngine )(
            //     ITextChunk * This,
            //     /* [in] */ GUID *guid,
            //     /* [in] */ BSTR dllName);
            void stub_RegisterEngine();

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *UnregisterEngine )(
            //     ITextChunk * This,
            //     /* [in] */ GUID *guid);
            void stub_UnregisterEngine();

            // /* [propget][restricted][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_InputArray )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ LPCWSTR *pval);
            void stub_get_InputArray();

            // /* [propget][restricted][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_InputArrayRange )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ STextRange *pval);
            void stub_get_InputArrayRange();

            // /* [propput][restricted][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_InputArrayRange )(
            //     ITextChunk * This,
            //     /* [in] */ STextRange val);
            void stub_put_InputArrayRange();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Count )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ long *pval);
            void get_Count(out Int32 val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Item )(
            //     ITextChunk * This,
            //     /* [in] */ long index,
            //     /* [ref][retval][out] */ ISentence **pval);
            void get_Item(Int32 index, [MarshalAs(UnmanagedType.Interface)] out ISentence val);

            // /* [propget][restricted][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get__NewEnum )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get__NewEnum();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Sentences )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void get_Sentences([MarshalAs(UnmanagedType.Interface)] out MS.Win32.UnsafeNativeMethods.IEnumVariant val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_PropertyCount )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ long *pval);
            void stub_get_PropertyCount();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Property )(
            //     ITextChunk * This,
            //     /* [in] */ VARIANT index,
            //     /* [ref][retval][out] */ VARIANT *pval);
            void stub_get_Property();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_Property )(
            //     ITextChunk * This,
            //     /* [in] */ VARIANT index,
            //     /* [in] */ VARIANT val);
            void stub_put_Property();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Context )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ ITextContext **pval);
            void get_Context([MarshalAs(UnmanagedType.Interface)] out ITextContext val);

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_Context )(
            //     ITextChunk * This,
            //     /* [in] */ ITextContext *val);
            void put_Context([MarshalAs(UnmanagedType.Interface)] ITextContext val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Locale )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ LCID *pval);
            void stub_get_Locale();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_Locale )(
            //     ITextChunk * This,
            //     /* [in] */ LCID val);
            void put_Locale(Int32 val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsLocaleReliable )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsLocaleReliable();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsLocaleReliable )(
            //     ITextChunk * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsLocaleReliable();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsEndOfDocument )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsEndOfDocument();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsEndOfDocument )(
            //     ITextChunk * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsEndOfDocument();

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *GetEnumerator )(
            //     ITextChunk * This,
            //     /* [retval][out] */ IEnumVARIANT **ppSent);
            void GetEnumerator([MarshalAs(UnmanagedType.Interface)] out MS.Win32.UnsafeNativeMethods.IEnumVariant val);

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *ToString )(
            //     ITextChunk * This,
            //     /* [retval][out] */ BSTR *pstr);
            void stub_ToString();

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *ProcessStream )(
            //     ITextChunk * This,
            //     /* [in] */ IRangedTextSource *input,
            //     /* [out][in] */ IRangedTextSink *output);
            void stub_ProcessStream();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_ReuseObjects )(
            //     ITextChunk * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void get_ReuseObjects(out bool val);

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_ReuseObjects )(
            //     ITextChunk * This,
            //     /* [in] */ VARIANT_BOOL val);
            void put_ReuseObjects(bool val);
        }
    }
}

