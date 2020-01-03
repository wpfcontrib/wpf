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
        [Guid("AF4656B8-5E5E-4fb2-A2D8-1E977E549A56")]
        private interface ITextSegment
        {
            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsSurfaceString )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsSurfaceString();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Range )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ STextRange *pval);
            void get_Range([MarshalAs(UnmanagedType.Struct)] out STextRange val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Identifier )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ long *pval);
            void stub_get_Identifier();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Unit )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ MeasureUnit *pval);
            void stub_get_Unit();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Count )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ long *pval);
            void get_Count(out Int32 val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Item )(
            //     ITextSegment * This,
            //     /* [in] */ long index,
            //     /* [ref][retval][out] */ ITextSegment **pval);
            void get_Item(Int32 index, [MarshalAs(UnmanagedType.Interface)] out ITextSegment val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Expansions )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_Expansions();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Bases )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_Bases();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_SuggestionScores )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_SuggestionScores();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_PropertyCount )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ long *pval);
            void stub_get_PropertyCount();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Property )(
            //     ITextSegment * This,
            //     /* [in] */ VARIANT index,
            //     /* [ref][retval][out] */ VARIANT *pval);
            void stub_get_Property();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_Property )(
            //     ITextSegment * This,
            //     /* [in] */ VARIANT index,
            //     /* [in] */ VARIANT val);
            void stub_put_Property();

            // /* [helpstring][restricted] */ HRESULT ( STDMETHODCALLTYPE *CopyToString )(
            //     ISentence * This,
            //     /* [in][string] */ LPWSTR pStr,
            //     /* [in][out] */ long* pcch,
            //     /* [in] */ VARIANT_BOOL fAlwaysCopy);
            void stub_CopyToString();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Role )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ RangeRole *pval);
            void get_Role(out RangeRole val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_PrimaryType )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ PrimaryRangeType *pval);
            void stub_get_PrimaryType();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_SecondaryType )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ SecondaryRangeType *pval);
            void stub_get_SecondaryType();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_SpellingVariations )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_SpellingVariations();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_CharacterNormalizations )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_CharacterNormalizations();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Representations )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_Representations();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Inflections )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_Inflections();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Suggestions )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void get_Suggestions([MarshalAs(UnmanagedType.Interface)] out MS.Win32.UnsafeNativeMethods.IEnumVariant val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Lemmas )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_Lemmas();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_SubSegments )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_SubSegments();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Alternatives )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_Alternatives();

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *ToString )(
            //     ITextSegment * This,
            //     /* [retval][out] */ BSTR *string);
            void stub_ToString();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsPossiblePhraseStart )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsPossiblePhraseStart();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_SpellingScore )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ long *pval);
            void stub_get_SpellingScore();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsPunctuation )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsPunctuation();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsEndPunctuation )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsEndPunctuation();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsSpace )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsSpace();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsAbbreviation )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsAbbreviation();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsSmiley )(
            //     ITextSegment * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsSmiley();
        }
    }
}

