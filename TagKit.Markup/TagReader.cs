using System;
using System.IO;

namespace TagKit.Markup
{
    public abstract partial class TagReader : IDisposable
    {
        #region Static methods for creating readers
        /// <summary>
        /// Creates an TagReader according for parsing Tag from the given stream.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TagReader Create(Stream input)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Creates an TagReader according for parsing Tag from the given TextReader.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TagReader Create(TextReader input)
        {
            return Create(input, (TagReaderSettings)null, (string)string.Empty);
        }

        // Creates an XmlReader according to the settings and baseUri for parsing XML from the given TextReader.
        public static TagReader Create(TextReader input, TagReaderSettings settings, String baseUri)
        {
            if (settings == null)
            {
                settings = new TagReaderSettings();
            }
            return settings.CreateReader(input, baseUri, null);
        }
        #endregion

        #region MyRegion
        /// <summary>
        /// Closes the stream/TextReader (if CloseInput==true), changes the ReadState to Closed, and sets all the properties back to zero/empty string.
        /// </summary>
        public virtual void Close() { }

        /// <summary>
        /// Returns the read state of the TagReader.
        /// </summary>
        public abstract ReadState ReadState { get; }

        /// <summary>
        /// Moving through the Stream
        /// Reads the next node from the stream.
        /// </summary>
        /// <returns></returns>
        public abstract bool Read();

        #endregion
        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        { //the boolean flag may be used by subclasses to differentiate between disposing and finalizing
            if (disposing && ReadState != ReadState.Closed)
            {
                Close();
            }
        }

        #endregion
    }
}
