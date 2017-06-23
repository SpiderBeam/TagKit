using System;
using System.Diagnostics;
using System.IO;

namespace TagKit.Markup
{
    /// <summary>
    /// Represents a reader that provides fast, non-cached forward only stream access to Tag data. 
    /// </summary>
    [DebuggerDisplay("{debuggerDisplayProxy}")]
    public abstract partial class TagReader : IDisposable
    {
        #region Constants

        internal const int DefaultBufferSize = 4096;
        internal const int BiggerBufferSize = 8192;
        internal const int MaxStreamLengthForDefaultBufferSize = 64 * 1024; // 64kB

        internal const int AsyncBufferSize = 64 * 1024; //64KB

        #endregion
        #region Implementation of IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Static methods for creating readers
        /// <summary>
        /// Creates an TagReader for parsing Tag from the given Uri.
        /// </summary>
        /// <param name="inputUri"></param>
        /// <returns></returns>
        public static TagReader Create(string inputUri)
        {
            return TagReader.Create(inputUri, (TagReaderSettings)null, (TagParserContext)null);
        }
        /// <summary>
        /// Creates an TagReader according to the settings for parsing Tag from the given Uri.
        /// </summary>
        /// <param name="inputUri"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static TagReader Create(string inputUri, TagReaderSettings settings)
        {
            return TagReader.Create(inputUri, settings, (TagParserContext)null);
        }

        public static TagReader Create(String inputUri, TagReaderSettings settings, TagParserContext inputContext)
        {
            if (settings == null)
            {
                settings = new TagReaderSettings();
            }
            return settings.CreateReader(inputUri, inputContext);
        }

        // Creates an XmlReader according for parsing XML from the given stream.
        public static TagReader Create(Stream input)
        {
            return Create(input, (TagReaderSettings)null, (string)string.Empty);
        }
        // Creates an XmlReader according to the settings for parsing XML from the given stream.
        public static TagReader Create(Stream input, TagReaderSettings settings)
        {
            return Create(input, settings, string.Empty);
        }
        // Creates an XmlReader according to the settings and base Uri for parsing XML from the given stream.
        private static TagReader Create(Stream input, TagReaderSettings settings, String baseUri)
        {
            if (settings == null)
            {
                settings = new TagReaderSettings();
            }
            return settings.CreateReader(input, null, (string)baseUri, null);
        }
        // Creates an XmlReader according to the settings and parser context for parsing XML from the given stream.
        public static TagReader Create(Stream input, TagReaderSettings settings, TagParserContext inputContext)
        {
            if (settings == null)
            {
                settings = new TagReaderSettings();
            }
            return settings.CreateReader(input, null, (string)string.Empty, inputContext);
        }
        // Creates an XmlReader according for parsing XML from the given TextReader.
        public static TagReader Create(TextReader input)
        {
            return Create(input, (TagReaderSettings)null, (string)string.Empty);
        }
        // Creates an XmlReader according to the settings for parsing XML from the given TextReader.
        public static TagReader Create(TextReader input, TagReaderSettings settings)
        {
            return Create(input, settings, string.Empty);
        }
        // Creates an XmlReader according to the settings and baseUri for parsing XML from the given TextReader.
        private static TagReader Create(TextReader input, TagReaderSettings settings, String baseUri)
        {
            if (settings == null)
            {
                settings = new TagReaderSettings();
            }
            return settings.CreateReader(input, baseUri, null);
        }
        // Creates an XmlReader according to the settings and parser context for parsing XML from the given TextReader.
        public static TagReader Create(TextReader input, TagReaderSettings settings, TagParserContext inputContext)
        {
            if (settings == null)
            {
                settings = new TagReaderSettings();
            }
            return settings.CreateReader(input, string.Empty, inputContext);
        }
        // Creates an XmlReader according to the settings wrapped over the given reader.
        public static TagReader Create(TagReader reader, TagReaderSettings settings)
        {
            if (settings == null)
            {
                settings = new TagReaderSettings();
            }
            return settings.CreateReader(reader);
        }
        #endregion

        internal static int CalcBufferSize(Stream input)
        {
            // determine the size of byte buffer
            int bufferSize = DefaultBufferSize;
            if (input.CanSeek)
            {
                long len = input.Length;
                if (len < bufferSize)
                {
                    bufferSize = checked((int)len);
                }
                else if (len > MaxStreamLengthForDefaultBufferSize)
                {
                    bufferSize = BiggerBufferSize;
                }
            }

            // return the byte buffer size
            return bufferSize;
        }

        public void ReadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
