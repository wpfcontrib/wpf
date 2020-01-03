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
        [Guid("B6797CC0-11AE-4047-A438-26C0C916EB8D")]
        private interface ITextContext
        {
            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_PropertyCount )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ long *pval);
            void stub_get_PropertyCount();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Property )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT index,
            //     /* [ref][retval][out] */ VARIANT *pval);
            void stub_get_Property();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_Property )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT index,
            //     /* [in] */ VARIANT val);
            void stub_put_Property();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_DefaultDialectCount )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ long *pval);
            void stub_get_DefaultDialectCount();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_DefaultDialect )(
            //     ITextContext * This,
            //     /* [in] */ long index,
            //     /* [ref][retval][out] */ LCID *pval);
            void stub_get_DefaultDialect();

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *AddDefaultDialect )(
            //     ITextContext * This,
            //     /* [in] */ LCID dicalect);
            void stub_AddDefaultDialect();

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *RemoveDefaultDialect )(
            //     ITextContext * This,
            //     /* [in] */ LCID dicalect);
            void stub_RemoveDefaultDialect();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_LexiconCount )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ long *pval);
            void get_LexiconCount([MarshalAs(UnmanagedType.I4)] out Int32 lexiconCount);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Lexicon )(
            //     ITextContext * This,
            //     /* [in] */ long index,
            //     /* [ref][retval][out] */ ILexicon **pval);
            void get_Lexicon(Int32 index, [MarshalAs(UnmanagedType.Interface)] out ILexicon lexicon);

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *AddLexicon )(
            //     ITextContext * This,
            //     /* [in] */ ILexicon *pLexicon);
            void AddLexicon([In, MarshalAs(UnmanagedType.Interface)] ILexicon lexicon);

            // /* [helpstring] */ HRESULT ( STDMETHODCALLTYPE *RemoveLexicon )(
            //     ITextContext * This,
            //     /* [in] */ ILexicon *pLexicon);
            void RemoveLexicon([In, MarshalAs(UnmanagedType.Interface)] ILexicon lexicon);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Version )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ BSTR *pval);
            void stub_get_Version();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_ResourceLoader )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ ILoadResources **pval);
            void stub_get_ResourceLoader();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_ResourceLoader )(
            //     ITextContext * This,
            //     /* [in] */ ILoadResources *val);
            void stub_put_ResourceLoader();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Options )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ IProcessingOptions **pval);
            void get_Options([MarshalAs(UnmanagedType.Interface)] out IProcessingOptions val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Capabilities )(
            //     ITextContext * This,
            //     /* [in] */ LCID locale,
            //     /* [ref][retval][out] */ IProcessingOptions **pval);
            void get_Capabilities(Int32 locale, [MarshalAs(UnmanagedType.Interface)] out IProcessingOptions val);

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_Lexicons )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ IEnumVARIANT **pval);
            void stub_get_Lexicons();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_Lexicons )(
            //     ITextContext * This,
            //     /* [in] */ IEnumVARIANT *val);
            void stub_put_Lexicons();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_MaxSentences )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ long *pval);
            void stub_get_MaxSentences();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_MaxSentences )(
            //     ITextContext * This,
            //     /* [in] */ long val);
            void stub_put_MaxSentences();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsSingleLanguage )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsSingleLanguage();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsSingleLanguage )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsSingleLanguage();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsSimpleWordBreaking )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsSimpleWordBreaking();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsSimpleWordBreaking )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsSimpleWordBreaking();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_UseRelativeTimes )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_UseRelativeTimes();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_UseRelativeTimes )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_UseRelativeTimes();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IgnorePunctuation )(
            // ITextContext * This,
            // /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IgnorePunctuation();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IgnorePunctuation )(
            // ITextContext * This,
            // /* [in] */ VARIANT_BOOL val);
            void stub_put_IgnorePunctuation();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsCaching )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsCaching();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsCaching )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsCaching();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsShowingGaps )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsShowingGaps();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsShowingGaps )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsShowingGaps();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsShowingCharacterNormalizations )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsShowingCharacterNormalizations();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsShowingCharacterNormalizations )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsShowingCharacterNormalizations();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsShowingWordNormalizations )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsShowingWordNormalizations();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsShowingWordNormalizations )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsShowingWordNormalizations();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsComputingCompounds )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsComputingCompounds();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsComputingCompounds )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsComputingCompounds();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsComputingInflections )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsComputingInflections();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsComputingInflections )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsComputingInflections();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsComputingLemmas )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsComputingLemmas();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsComputingLemmas )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsComputingLemmas();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsComputingExpansions )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsComputingExpansions();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsComputingExpansions )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsComputingExpansions();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsComputingBases )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsComputingBases();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsComputingBases )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsComputingBases();

            // /* [propget][helpstring] */ HRESULT STDMETHODCALLTYPE get_IsComputingPartOfSpeechTags(
            // /* [ref][retval][out] */ VARIANT_BOOL *pval) = 0;
            void stub_get_IsComputingPartOfSpeechTags();

            // /* [propput][helpstring] */ HRESULT STDMETHODCALLTYPE put_IsComputingPartOfSpeechTags(
            // /* [in] */ VARIANT_BOOL val) = 0;
            void stub_put_IsComputingPartOfSpeechTags();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsFindingDefinitions )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsFindingDefinitions();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsFindingDefinitions )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsFindingDefinitions();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsFindingDateTimeMeasures )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsFindingDateTimeMeasures();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsFindingDateTimeMeasures )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsFindingDateTimeMeasures();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsFindingPersons )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsFindingPersons();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsFindingPersons )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsFindingPersons();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsFindingLocations )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsFindingLocations();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsFindingLocations )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsFindingLocations();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsFindingOrganizations )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsFindingOrganizations();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsFindingOrganizations )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsFindingOrganizations();

            // /* [propget][helpstring] */ HRESULT ( STDMETHODCALLTYPE *get_IsFindingPhrases )(
            //     ITextContext * This,
            //     /* [ref][retval][out] */ VARIANT_BOOL *pval);
            void stub_get_IsFindingPhrases();

            // /* [propput][helpstring] */ HRESULT ( STDMETHODCALLTYPE *put_IsFindingPhrases )(
            //     ITextContext * This,
            //     /* [in] */ VARIANT_BOOL val);
            void stub_put_IsFindingPhrases();
        }
    }
}

