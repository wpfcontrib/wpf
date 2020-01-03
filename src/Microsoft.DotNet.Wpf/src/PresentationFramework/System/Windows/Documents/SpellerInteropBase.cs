// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace System.Windows.Documents
{
    using System.Collections;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Controls;

    internal abstract partial class SpellerInteropBase: IDisposable
    {
        #region Internal Types

        // Callback delegates for EnumTextSegments method.
        internal delegate bool EnumSentencesCallback(ISpellerSentence sentence, object data);
        internal delegate bool EnumTextSegmentsCallback(ISpellerSegment textSegment, object data);

        #endregion Internal Types

        #region IDispose

        public abstract void Dispose();
        protected abstract void Dispose(bool disposing);

        #endregion 

        #region Factory

        public static SpellerInteropBase CreateInstance()
        {
            SpellerInteropBase spellerInterop = null;

            bool winRTSupport = false;
            
            try
            {
                spellerInterop = new WinRTSpellerInterop();
                winRTSupport = true;
            }
            catch (PlatformNotSupportedException)
            {
                winRTSupport = false;
            }
            catch (NotSupportedException)
            {
                // Any other exception besides PlatformNotSupportedException
                // indicates that WinRT API's are supportable on this OS 
                // platform, but failed to initialize for some reason.
                winRTSupport = true;
            }

            if (!winRTSupport)
            {
                try
                {
                    spellerInterop = new NLGSpellerInterop();
                }
                catch (Exception ex) when 
                    (ex is DllNotFoundException || ex is EntryPointNotFoundException)
                {
                    return null;
                }
            }

            return spellerInterop;
        }

        #endregion Factory

        #region Abstract Methods

        internal abstract void SetLocale(CultureInfo culture);


        // Helper for methods that need to iterate over segments within a text run.
        // Returns the total number of segments encountered.
        internal abstract int EnumTextSegments(char[] text, int count,
            EnumSentencesCallback sentenceCallback, EnumTextSegmentsCallback segmentCallback, object data);

        /// <summary>
        /// Unloads given custom dictionary
        /// </summary>
        /// <param name="lexicon"></param>
        internal abstract void UnloadDictionary(object dictionary); 

         /// <summary>
        /// Loads custom dictionary
        /// </summary>
        /// <param name="lexiconFilePath"></param>
        /// <returns></returns>
        internal abstract object LoadDictionary(string lexiconFilePath);


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
        internal abstract object LoadDictionary(Uri item, string trustedFolder);

        /// <summary>
        /// Releases all currently loaded lexicons.
        /// </summary>
        internal abstract void ReleaseAllLexicons();

        /// <summary>
        /// Sets the speller mode to be wordbreaking only, wordbreaking + spellchecking or 
        /// wordbreaking+spellchecking+suggestion generation
        /// </summary>
        internal abstract SpellerMode Mode
        {
            set;
        }

        /// <summary>
        /// Tells the spellchecker whether to check for multi-word spelling errors
        /// </summary>
        internal abstract bool MultiWordMode
        {
             set;
        }

        /// <summary>
        /// Sets spelling reform options
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="spellingReform"></param>
        internal abstract void SetReformMode(CultureInfo culture, SpellingReform spellingReform);

        /// <summary>
        /// Returns true if we have an engine capable of proofing the specified language.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        internal abstract bool CanSpellCheck(CultureInfo culture);

        #endregion Abstract Methods
    }
}
