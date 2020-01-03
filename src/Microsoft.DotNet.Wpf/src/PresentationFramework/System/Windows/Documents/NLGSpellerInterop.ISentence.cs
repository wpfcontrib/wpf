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
        [Guid("F0C13A7A-199B-44be-8492-F91EAA50F943")]
        private interface ISentence
        {
            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_PropertyCount )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ long *pval);
            void stub_get_PropertyCount();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Property )(
            //     ISentence * This,
            //     /* [in] */ VARIANT index,
            //     /* [ref][retval][out] */ VARIANT *pval);
            void stub_get_Property();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_Property )(
            //     ISentence * This,
            //     /* [in] */ VARIANT index,
            //     /* [in] */ VARIANT val);
            void stub_put_Property();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Count )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ long *pval);
            void get_Count(out Int32 val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Parent )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ ITextChunk **pval);
            void stub_get_Parent();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Item )(
            //     ISentence * This,
            //     /* [in] */ long index,
            //     /* [ref][retval][out] */ ITextSegment **pval);
            void get_Item(Int32 index, [MarshalAs(UnmanagedType.Interface)] out ITextSegment val);

            // /* [propget][restricted][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get__NewEnum )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get__NewEnum();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Segments )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_Segments();

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *GetEnumerator )(
            //     ISentence * This,
            //     /* [retval][out] */ IEnumVARIANT **string);
            void stub_GetEnumerator();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsEndOfParagraph )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsEndOfParagraph();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsUnfinished )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsUnfinished();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsUnfinishedAtEnd )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsUnfinishedAtEnd();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Locale )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ LCID *pval);
            void stub_get_Locale();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsLocaleReliable )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsLocaleReliable();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Range )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ STextRange *pval);
            void stub_get_Range();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_RequiresNormalization )(
            //     ISentence * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_RequiresNormalization();

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *ToString )(
            //     ISentence * This,
            //     /* [retval][out] */ BSTR *string);
            void stub_ToString();

            // /* [helpstring][restricted] */ HRESULT ( STDMETHODCALLTYPE *CopyToString )(
            //     ISentence * This,
            //     /* [in][string] */ LPWSTR pStr,
            //     /* [in][out] */ long* pcch,
            //     /* [in] */ VARIANT_BOOL fAlwaysCopy);
            void stub_CopyToString();
        }
    }
}

