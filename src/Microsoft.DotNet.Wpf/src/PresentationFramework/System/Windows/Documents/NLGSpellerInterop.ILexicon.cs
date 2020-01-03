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
        [Guid("004CD7E2-8B63-4ef9-8D46-080CDBBE47AF")]
        internal interface ILexicon
        {
            //[
            //]
            //HRESULT ReadFrom ([in] BSTR filename);
            void ReadFrom ([MarshalAs( UnmanagedType.BStr )]string fileName);

            //[
            //]
            //HRESULT WriteTo ([in] BSTR filename);
            void stub_WriteTo ();

            //[
            //]
            //HRESULT GetEnumerator ([retval,out] ILexiconEntryEnumerator **enumerator);
            void stub_GetEnumerator ();

            //[
            //]
            //HRESULT IndexOf (
            //             [in] BSTR word,
            //             [out,retval] long *index);
            void stub_IndexOf();

            //[
            //]
            //HRESULT TagFor (
            //             [in] BSTR word,
            //             [in] long tagIndex,
            //             [out,retval] long *index);
            void stub_TagFor ();

            //[
            //]
            //HRESULT ContainsPrefix (
            //             [in] BSTR prefix,
            //             [out,retval] VARIANT_BOOL *containsPrefix);
            void stub_ContainsPrefix();

            //[
            //]
            //HRESULT Add ([in] BSTR entry);
            void stub_Add();

            //[
            //]
            //HRESULT Remove ([in] BSTR entry);
        	void stub_Remove();
            //[
            //    propget
            //]
            //HRESULT Version ([out, retval, ref] BSTR *pval);
            void stub_Version();


            //[
            //    helpstring("The number of elements in this collection."),
            //    propget
            //]
            //HRESULT Count ([out, retval, ref] long *pval);
            void stub_Count();


            //[
            //    helpstring("Get an enumerator of elements in this collection."),
            //    restricted,
            //    propget
            //]
            //HRESULT _NewEnum ([out, retval, ref] IEnumVARIANT **pval);
            void stub__NewEnum();


            //[
            //    propget
            //]
            //HRESULT Item (
            //             [in] long key,
            //    [out, retval, ref] ILexiconEntry **pval);
            void stub_get_Item();

            //[
            //    propput
            //]
            //HRESULT Item (
            //             [in] long key,
            //    [in] ILexiconEntry *val);
            void stub_set_Item();

            //[
            //    propget
            //]
            //HRESULT ItemByName (
            //             [in] BSTR key,
            //    [out, retval, ref] ILexiconEntry **pval);
            void stub_get_ItemByName();

            //[
            //    propput
            //]
            //HRESULT ItemByName (
            //             [in] BSTR key,
            //    [in] ILexiconEntry *val);
            void stub_set_ItemByName();

            //[
            //    propget
            //]
            //HRESULT PropertyCount ([out, retval, ref] long *pval);
            void stub_get0_PropertyCount();


            //[
            //    helpstring("The keys for this dictionary are the names of the properties, the value are VARIANTS."),
            //    propget
            //]
            //HRESULT Property (
            //             [in] VARIANT index,
            //    [out, retval, ref] VARIANT *pval);
            void stub_get1_Property();

            //[
            //    helpstring("The keys for this dictionary are the names of the properties, the value are VARIANTS."),
            //    propput
            //]
            //HRESULT Property (
            //             [in] VARIANT index,
            //    [in] VARIANT val);
            void stub_set_Property();

            //[
            //    propget
            //]
            //HRESULT IsSealed ([out, retval, ref] VARIANT_BOOL *pval);
            void stub_get_IsSealed();


            //[
            //    propget
            //]
            //HRESULT IsReadOnly ([out, retval, ref] VARIANT_BOOL *pval);
            void stub_get_IsReadOnly();
        }
    }
}

