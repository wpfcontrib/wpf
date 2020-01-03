// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

//
// Description: Custom COM marshalling code and interfaces for interaction
//              with the Natural Language Group's nl6 proofing engine.
//

namespace System.Windows.Documents
{
    using System.Collections;
    using System.Runtime.InteropServices;
    using MS.Internal;
    using MS.Win32;
    using System.Globalization;
    using System.Security;
    using System.IO;
    using System.Windows.Controls;

    // Custom COM marshalling code and interfaces for interaction
    // with the Natural Language Group's nl6 proofing engine.
    internal partial class NLGSpellerInterop : SpellerInteropBase
    {
        //------------------------------------------------------
        //
        //  Constructors
        //
        //------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Construct an NLG-based speller interop layer
        /// </summary>
        internal NLGSpellerInterop()
        {
            // Start the lifetime of Natural Language library
            UnsafeNlMethods.NlLoad();

            bool exceptionThrown = true;
            try
            {
                //
                // Allocate the TextChunk.
                //

                _textChunk = CreateTextChunk();

                //
                // Allocate the TextContext.
                //

                ITextContext textContext = CreateTextContext();
                try
                {
                    _textChunk.put_Context(textContext);
                }
                finally
                {
                    Marshal.ReleaseComObject(textContext);
                }

                //
                // Set nl properties.
                //
                _textChunk.put_ReuseObjects(true);
                Mode = SpellerMode.None;

                //  reenable MWE checking when perf is acceptable.
                // We're disabling multi-word error checking for the short term
                // because it is so expensive, 30-50% extra elapsed time.
                MultiWordMode = false;

                exceptionThrown = false;
            }
            finally
            {
                if (exceptionThrown)
                {
                    if (_textChunk != null)
                    {
                        Marshal.ReleaseComObject(_textChunk);
                        _textChunk = null;
                    }

                    UnsafeNlMethods.NlUnload();
                }
            }
        }

        ~NLGSpellerInterop()
        {
            Dispose(false);
        }

        #endregion Constructors

        //------------------------------------------------------
        //
        //  IDispose Methods
        //
        //------------------------------------------------------

        #region IDispose Methods

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Internal interop resource cleanup
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                throw new ObjectDisposedException(SR.Get(SRID.TextEditorSpellerInteropHasBeenDisposed));

            if (_textChunk != null)
            {
                Marshal.ReleaseComObject(_textChunk);
                _textChunk = null;
            }

            // Stop the lifetime of Natural Language library
            UnsafeNlMethods.NlUnload();

            _isDisposed = true;
        }

        #endregion IDispose Methods

        //------------------------------------------------------
        //
        //  Internal Methods
        //
        //------------------------------------------------------

        #region Internal Methods

        internal override void SetLocale(CultureInfo culture)
        {
            _textChunk.put_Locale(culture.LCID);
        }

        // Sets an indexed option on the speller's TextContext.
        private void SetContextOption(string option, object value)
        {
            ITextContext textContext;

            _textChunk.get_Context(out textContext);

            if (textContext != null)
            {
                try
                {
                    IProcessingOptions options;

                    textContext.get_Options(out options);
                    if (options != null)
                    {
                        try
                        {
                            options.put_Item(option, value);
                        }
                        finally
                        {
                            Marshal.ReleaseComObject(options);
                        }
                    }
                }
                finally
                {
                    Marshal.ReleaseComObject(textContext);
                }
            }
        }

        // Helper for methods that need to iterate over segments within a text run.
        // Returns the total number of segments encountered.
        internal override int EnumTextSegments(char[] text, int count,
            EnumSentencesCallback sentenceCallback, EnumTextSegmentsCallback segmentCallback, object data)
        {
            int segmentCount = 0;

            // Unintuively, the speller engine will grab and store the pointer
            // we pass into ITextChunk.SetInputArray.  So it's not safe to merely
            // pinvoke text directly.  We need to allocate a chunk of memory
            // and keep it fixed for the duration of this method call.
            IntPtr inputArray = Marshal.AllocHGlobal(count * 2);

            try
            {
                // Give the TextChunk its next block of text.
                Marshal.Copy(text, 0, inputArray, count);
                _textChunk.SetInputArray(inputArray, count);

                //
                // Iterate over sentences.
                //

                UnsafeNativeMethods.IEnumVariant sentenceEnumerator;

                // Note because we're in the engine's ReuseObjects mode, we may
                // not use ITextChunk.get_Sentences.  We must use the enumerator.
                _textChunk.GetEnumerator(out sentenceEnumerator);
                try
                {
                    NativeMethods.VARIANT variant = new NativeMethods.VARIANT();
                    int[] fetched = new int[1];
                    bool continueIteration = true;

                    sentenceEnumerator.Reset();

                    do
                    {
                        int result;

                        variant.Clear();

                        result = EnumVariantNext(sentenceEnumerator, variant, fetched);

                        if ((result != NativeMethods.S_OK) || (fetched[0] == 0))
                        {
                            break;
                        }

                        using (SpellerSentence sentence = new SpellerSentence(new string(text), (NLGSpellerInterop.ISentence)variant.ToObject()))
                        {
                            segmentCount += sentence.Segments.Count;

                            if (segmentCallback != null)
                            {
                                // Iterate over segments.
                                for (int i = 0; continueIteration && (i < sentence.Segments.Count); i++ )
                                {
                                    continueIteration = segmentCallback(sentence.Segments[i], data);
                                }
                            }

                            // Make another callback when we're done with the entire sentence.
                            if (sentenceCallback != null)
                            {
                                continueIteration = sentenceCallback(sentence, data);
                            }
                        }
                    }
                    while (continueIteration);

                    variant.Clear();
                }
                finally
                {
                    Marshal.ReleaseComObject(sentenceEnumerator);
                }
            }
            finally
            {
                Marshal.FreeHGlobal(inputArray);
            }

            return segmentCount;
        }

        /// <summary>
        /// Unloads given custom dictionary
        /// </summary>
        /// <param name="lexicon"></param>
        internal override void UnloadDictionary(object dictionary)
        {
            ILexicon lexicon = dictionary as ILexicon;
            Invariant.Assert(lexicon != null);

            ITextContext textContext = null;
            try
            {
                _textChunk.get_Context(out textContext);
                textContext.RemoveLexicon(lexicon);
            }
            finally
            {
                Marshal.ReleaseComObject(lexicon);

                if (textContext != null)
                {
                    Marshal.ReleaseComObject(textContext);
                }
            }
        }

        /// <summary>
        /// Loads custom dictionary
        /// </summary>
        /// <param name="lexiconFilePath"></param>
        /// <returns></returns>
        internal override object LoadDictionary(string lexiconFilePath)
        {
            return AddLexicon(lexiconFilePath);
        }


        /// <summary>
        /// Loads custom dictionary.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="trustedFolder"></param>
        /// <returns></returns>
        /// <remarks>
        /// There are 2 kinds of files we're trying to load here: Files specified by user directly, and files
        /// which we created and filled with data from pack Uri locations specified by user.
        /// These 'trusted' files are placed under <paramref name="trustedFolder"/>.
        ///
        /// Files specified in <paramref name="trustedFolder"/> are wrapped in FileIOPermission.Assert(),
        /// providing read access to trusted files under <paramref name="trustedFolder"/>, i.e. additionally
        /// we're making sure that specified trusted locations are under the trusted Folder.
        ///
        /// This is needed to differentiate a case when user passes in a local path location which just happens to be under
        /// trusted folder. We still want to fail in this case, since we want to trust only files that we've created.
        /// </remarks>
        internal override object LoadDictionary(Uri item, string trustedFolder)
        {
            return LoadDictionary(item.LocalPath);
        }

        /// <summary>
        /// Releases all currently loaded lexicons.
        /// </summary>
        internal override void ReleaseAllLexicons()
        {
            ITextContext textContext = null;
            try
            {
                _textChunk.get_Context(out textContext);
                Int32 lexiconCount = 0;
                textContext.get_LexiconCount(out lexiconCount);
                while (lexiconCount > 0)
                {
                    ILexicon lexicon = null;
                    textContext.get_Lexicon(0, out lexicon);
                    textContext.RemoveLexicon(lexicon);
                    Marshal.ReleaseComObject(lexicon);
                    lexiconCount--;
                }
            }
            finally
            {
                if (textContext != null)
                {
                    Marshal.ReleaseComObject(textContext);
                }
            }
        }

        /// <summary>
        /// Sets the mode in which the spell-checker operates
        /// We care about 3 different modes here: 
        /// 
        /// 1. Shallow spellchecking - i.e., wordbreaking +      spellchecking + NOT (suggestions)
        /// 2. Deep spellchecking    - i.e., wordbreaking +      spellchecking +      suggestions
        /// 3. Wordbreaking only     - i.e., wordbreaking + NOT (spellchcking) + NOT (suggestions)
        /// </summary>
        internal override SpellerMode Mode
        {
            set
            {
                _mode = value;

                if (_mode.HasFlag(SpellerMode.SpellingErrors))
                {
                    SetContextOption("IsSpellChecking", true);

                    if (_mode.HasFlag(SpellerMode.Suggestions))
                    {
                        SetContextOption("IsSpellVerifyOnly", false);
                    }
                    else
                    {
                        SetContextOption("IsSpellVerifyOnly", true);
                    }
                }
                else if (_mode.HasFlag(SpellerMode.WordBreaking))
                {
                    SetContextOption("IsSpellChecking", false);
                }
            }
        }

        /// <summary>
        /// If true, multi-word spelling errors would be detected
        /// </summary>
        internal override bool MultiWordMode
        {
            set
            {
                _multiWordMode = value;
                SetContextOption("IsSpellSuggestingMWEs", _multiWordMode);
            }
        }

        /// <summary>
        /// Sets spelling reform mode
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="spellingReform"></param>
        internal override void SetReformMode(CultureInfo culture, SpellingReform spellingReform)
        {
            const int
                BothPreAndPost = 0,
                Prereform      = 1,
                Postreform     = 2;

            string option;

            switch (culture.TwoLetterISOLanguageName)
            {
                case "de":
                    option = "GermanReform";
                    break;

                case "fr":
                    option = "FrenchReform";
                    break;

                default:
                    option = null;
                    break;
            }

            if (option != null)
            {
                switch (spellingReform)
                {
                    case SpellingReform.Prereform:
                        SetContextOption(option, Prereform);
                        break;

                    case SpellingReform.Postreform:
                        SetContextOption(option, Postreform);
                        break;

                    case SpellingReform.PreAndPostreform:
                        if (option == "GermanReform")
                        {
                            // BothPreAndPost is disallowed for german -- the engine has undefined results.
                            SetContextOption(option, Postreform);
                        }
                        else
                        {
                            SetContextOption(option, BothPreAndPost);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Returns true if we have an engine capable of proofing the specified language.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        internal override bool CanSpellCheck(CultureInfo culture)
        {
            bool canSpellCheck;

            switch (culture.TwoLetterISOLanguageName)
            {
                case "en":
                case "de":
                case "fr":
                case "es":
                    canSpellCheck = true;
                    break;

                default:
                    canSpellCheck = false;
                    break;
            }

            return canSpellCheck;
        }

#endregion Internal methods


        //------------------------------------------------------
        //
        //  Private Methods
        //
        //------------------------------------------------------

        #region Private Methods



        //------------------------------------------------------
        //
        //  ILexicon management methods
        //
        //------------------------------------------------------
        #region ILexicon management methods

        /// <summary>
        /// Adds new custom dictionary to the spell engine.
        /// </summary>
        /// <param name="lexiconFilePath"></param>
        /// <returns>Reference to new ILexicon</returns>
        ///
        private ILexicon AddLexicon(string lexiconFilePath)
        {
            ITextContext textContext = null;
            ILexicon lexicon = null;
            bool exception = true;
            bool hasDemand = false;

            try
            {
                hasDemand = true;

                lexicon = NLGSpellerInterop.CreateLexicon();
                lexicon.ReadFrom(lexiconFilePath);
                _textChunk.get_Context(out textContext);
                textContext.AddLexicon(lexicon);
                exception = false;
            }
            catch (Exception e)
            {
                // We'll provide details of exception only if Demand to access lexiconFilePath was satisfied.
                // Otherwise it's a security concern to disclose this data.
                if (hasDemand)
                {
                    throw new ArgumentException(SR.Get(SRID.CustomDictionaryFailedToLoadDictionaryUri, lexiconFilePath), e);
                }
                else
                {
                    throw;// Demand has failed so we're rethrowing security exception.
                }
            }
            finally
            {
                if ((exception) &&(lexicon != null))
                {
                    Marshal.ReleaseComObject(lexicon);
                }
                if (null != textContext)
                {
                    Marshal.ReleaseComObject(textContext);
                }
            }
            return lexicon;
        }

        #endregion ILexicon management methods


        // Returns an object exported from NaturalLanguage6.dll's class factory.
        private static object CreateInstance(Guid clsid, Guid iid)
        {
            object classObject;
            UnsafeNlMethods.NlGetClassObject(ref clsid, ref iid, out classObject);
            return classObject;
        }

        // Creates a new ITextContext instance.
        private static ITextContext CreateTextContext()
        {
            return (ITextContext)CreateInstance(CLSID_ITextContext, IID_ITextContext);
        }

        // Creates a new ITextChunk instance.
        private static ITextChunk CreateTextChunk()
        {
            return (ITextChunk)CreateInstance(CLSID_ITextChunk, IID_ITextChunk);
        }

        // Creates a new ILexicon instance.
        private static ILexicon CreateLexicon()
        {
            return (ILexicon)CreateInstance(CLSID_Lexicon, IID_ILexicon);
        }



        // Helper for IEnumVariant.Next call -- the debugger isn't displaying
        // variables in any method with the call.
        private static int EnumVariantNext(UnsafeNativeMethods.IEnumVariant variantEnumerator, NativeMethods.VARIANT variant, int[] fetched)
        {
            int result;

            unsafe
            {
                fixed (void* pVariant = &variant.vt)
                {
                    result = variantEnumerator.Next(1, (IntPtr)pVariant, fetched);
                }
            }

            return result;
        }

#endregion Private methods

        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------

        #region Private Fields

        private ITextChunk _textChunk;

        // True after this object has been disposed.
        private bool _isDisposed;

        // Speller mode 
        private SpellerMode _mode;

        // Multi-word error checking mode
        private bool _multiWordMode;

        // 333E6924-4353-4934-A7BE-5FB5BDDDB2D6
        private static readonly Guid CLSID_ITextContext = new Guid(0x333E6924, 0x4353, 0x4934, 0xA7, 0xBE, 0x5F, 0xB5, 0xBD, 0xDD, 0xB2, 0xD6);

        // B6797CC0-11AE-4047-A438-26C0C916EB8D
        private static readonly Guid IID_ITextContext = new Guid(0xB6797CC0, 0x11AE, 0x4047, 0xA4, 0x38, 0x26, 0xC0, 0xC9, 0x16, 0xEB, 0x8D);

        // 89EA5B5A-D01C-4560-A874-9FC92AFB0EFA
        private static readonly Guid CLSID_ITextChunk = new Guid(0x89EA5B5A, 0xD01C, 0x4560, 0xA8, 0x74, 0x9F, 0xC9, 0x2A, 0xFB, 0x0E, 0xFA);

        // 549F997E-0EC3-43d4-B443-2BF8021010CF
        private static readonly Guid IID_ITextChunk = new Guid(0x549F997E, 0x0EC3, 0x43d4, 0xB4, 0x43, 0x2B, 0xF8, 0x02, 0x10, 0x10, 0xCF);

        private static readonly Guid CLSID_Lexicon = new Guid("D385FDAD-D394-4812-9CEC-C6575C0B2B38");
        private static readonly Guid IID_ILexicon = new Guid("004CD7E2-8B63-4ef9-8D46-080CDBBE47AF");

        #endregion Private Fields
    }
}

