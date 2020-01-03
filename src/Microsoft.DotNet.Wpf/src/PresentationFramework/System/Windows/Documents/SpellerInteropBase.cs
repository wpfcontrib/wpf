// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace System.Windows.Documents
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Controls;

    internal abstract class SpellerInteropBase: IDisposable
    {
        #region Internal Types

        // Callback delegates for EnumTextSegments method.
        internal delegate bool EnumSentencesCallback(ISpellerSentence sentence, object data);
        internal delegate bool EnumTextSegmentsCallback(ISpellerSegment textSegment, object data);

        /// <summary>
        /// Identifies, by position, a sub-string of a source text string
        /// </summary>
        internal interface ITextRange
        {
            int Start { get; }
            int Length { get; }
        }

        /// <summary>
        /// Represents the spell-checkers notion of a 'word'
        /// </summary>
        internal interface ISpellerSegment
        {
            /// <summary>
            ///  Source String for which <see cref="TextRange"/> provides a position.
            /// </summary>
            string SourceString { get;  }

            /// <summary>
            /// Identifies sub-words, if any. 
            /// </summary>
            IReadOnlyList<ISpellerSegment> SubSegments { get; }

            /// <summary>
            /// Obtains the position of this segment in its source text string
            /// </summary>
            ITextRange TextRange { get; }

            /// <summary>
            /// Text represented by <see cref="TextRange"/>
            /// </summary>
            string Text => SourceString.Substring(TextRange.Start, TextRange.Length);

            /// <summary>
            /// Gets the alternate forms, if any, associated with the current word. 
            /// </summary>
            /// <remarks>
            /// Alternate forms are loosely associated with the current word. An alternate form may encompass less text, more text, or the same text as the original word.
            /// </remarks>
            IReadOnlyList<string> AlternateForms { get; }

            /// <summary>
            /// Queries the spell-checker to obtain suggestions for this segment
            /// </summary>
            IReadOnlyList<string> Suggestions { get; }

            /// <summary>
            /// Returns true if the segment has no spelling erorrs
            /// </summary>
            bool IsClean { get; }

            /// <summary>
            /// Enumerates a segment's subsegments, making a callback on each iteration.
            /// </summary>
            /// <param name="segmentCallback"></param>
            /// <param name="data"></param>
            void EnumSubSegments(EnumTextSegmentsCallback segmentCallback, object data);

            /// <summary>
            /// Register one alternate form. <paramref name="alternateForm"/> should be 
            /// an index into the same <see cref="SourceString"/>, and <see cref="TextRange"/> should
            /// be different from that of <paramref name="alternateForm"/>.
            /// </summary>
            /// <param name="alternateForm"></param>
            void RegisterAlternateForm(ISpellerSegment alternateForm);

            /// <summary>
            /// Register a set of alternate forms created by providing a suffix string to the current segment.
            /// The suffix string will be permuted to create strings of different lengths, that would in turn 
            /// be concatenated with <see cref="Text"/> to create new alternate-forms.
            /// </summary>
            /// <remarks>
            /// The <paramref name="suffix"/> should be contiguous with the current string, and must represent
            /// the same <see cref="SourceString"/>.
            /// </remarks>
            /// <param name="suffix"></param>
            void RegisterAlternateForms(ISpellerSegment suffix);
        }

        /// <summary>
        /// Represents the spell-checker's notion of a 'sentence', which is in turn made 
        /// up of 'segments' (words) and 'sub-segments'
        /// </summary>
        internal interface ISpellerSentence
        {
            string SourceText { get; }

            IReadOnlyList<ISpellerSegment> Segments { get; }
            
            /// <summary>
            /// Returns the final symbol offset of a sentence.
            /// </summary>
            int EndOffset { get; }
        }

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
