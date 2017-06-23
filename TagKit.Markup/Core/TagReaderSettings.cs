using System;
using System.IO;
using TagKit.Markup.Properties;

namespace TagKit.Markup
{
    /// <summary>
    /// TagReaderSettings class specifies basic features of an TagReader.
    /// </summary>
    public sealed class TagReaderSettings
    {
        #region Fields
        private bool _useAsync;

        #region Nametable
        private TagNameTable _nameTable;
        #endregion

        #region TagResolver
        private TagResolver _tagResolver = null;
        #endregion

        #region Text settings
        private int _lineNumberOffset;
        private int _linePositionOffset;
        #endregion

        #region Conformance settings
        private ConformanceLevel _conformanceLevel;
        private bool _checkCharacters;

        private long _maxCharactersInDocument;
        private long _maxCharactersFromEntities;

        #endregion

        #region Filtering settings
        private bool _ignoreWhitespace;
        private bool _ignorePIs;
        private bool _ignoreComments;

        #endregion

        #region security settings
        private DtdProcessing _dtdProcessing;
        #endregion

        #region other settings
        private bool _closeInput;
        #endregion

        #region read-only flag
        private bool _isReadOnly;
        #endregion
        #endregion
        #region Constructor
        public TagReaderSettings()
        {
            Initialize();
        }


        #endregion

        #region Properties
        // Filtering settings
        public bool IgnoreWhitespace
        {
            get
            {
                return _ignoreWhitespace;
            }
            set
            {
                CheckReadOnly("IgnoreWhitespace");
                _ignoreWhitespace = value;
            }
        }

        public DtdProcessing DtdProcessing
        {
            get
            {
                return _dtdProcessing;
            }
            set
            {
                CheckReadOnly("DtdProcessing");

                if ((uint)value > (uint)DtdProcessing.Parse)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _dtdProcessing = value;
            }
        }

        public bool Async
        {
            get
            {
                return _useAsync;
            }
            set
            {
                CheckReadOnly("Async");
                _useAsync = value;
            }
        }

        // Nametable
        public TagNameTable NameTable
        {
            get
            {
                return _nameTable;
            }
            set
            {
                CheckReadOnly("NameTable");
                _nameTable = value;
            }
        }

        public bool IgnoreProcessingInstructions
        {
            get
            {
                return _ignorePIs;
            }
            set
            {
                CheckReadOnly("IgnoreProcessingInstructions");
                _ignorePIs = value;
            }
        }

        public bool IgnoreComments
        {
            get
            {
                return _ignoreComments;
            }
            set
            {
                CheckReadOnly("IgnoreComments");
                _ignoreComments = value;
            }
        }

        public bool CheckCharacters
        {
            get
            {
                return _checkCharacters;
            }
            set
            {
                CheckReadOnly("CheckCharacters");
                _checkCharacters = value;
            }
        }

        // Text settings
        public int LineNumberOffset
        {
            get
            {
                return _lineNumberOffset;
            }
            set
            {
                CheckReadOnly("LineNumberOffset");
                _lineNumberOffset = value;
            }
        }

        public int LinePositionOffset
        {
            get
            {
                return _linePositionOffset;
            }
            set
            {
                CheckReadOnly("LinePositionOffset");
                _linePositionOffset = value;
            }
        }

        public long MaxCharactersInDocument
        {
            get
            {
                return _maxCharactersInDocument;
            }
            set
            {
                CheckReadOnly("MaxCharactersInDocument");
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _maxCharactersInDocument = value;
            }
        }

        public long MaxCharactersFromEntities
        {
            get
            {
                return _maxCharactersFromEntities;
            }
            set
            {
                CheckReadOnly("MaxCharactersFromEntities");
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _maxCharactersFromEntities = value;
            }
        }


        // Conformance settings
        public ConformanceLevel ConformanceLevel
        {
            get
            {
                return _conformanceLevel;
            }
            set
            {
                CheckReadOnly("ConformanceLevel");

                if ((uint)value > (uint)ConformanceLevel.Document)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _conformanceLevel = value;
            }
        }

        #endregion

        #region Private methods
        private void Initialize()
        {
            _nameTable = null;
            _lineNumberOffset = 0;
            _linePositionOffset = 0;
            _checkCharacters = true;
            _conformanceLevel = ConformanceLevel.Document;

            _ignoreWhitespace = false;
            _ignorePIs = false;
            _ignoreComments = false;
            _dtdProcessing = DtdProcessing.Prohibit;
            _closeInput = false;

            _maxCharactersFromEntities = 0;
            _maxCharactersInDocument = 0;


            _useAsync = false;

            _isReadOnly = false;
        }
        private static TagResolver CreateDefaultResolver()
        {
            return new TagUrlResolver();
        }
        #endregion

        #region Internal methods

        private void CheckReadOnly(string propertyName)
        {
            if (_isReadOnly)
            {
                throw new TagException(Resources.Xml_ReadOnlyProperty, this.GetType().ToString() + '.' + propertyName);
            }
        }
        internal TagReader CreateReader(String inputUri, TagParserContext inputContext)
        {
            if (inputUri == null)
            {
                throw new ArgumentNullException(nameof(inputUri));
            }
            if (inputUri.Length == 0)
            {
                throw new ArgumentException(Resources.XmlConvert_BadUri, nameof(inputUri));
            }

            // resolve and open the url
            TagResolver tmpResolver = this.GetTagResolver();
            if (tmpResolver == null)
            {
                tmpResolver = CreateDefaultResolver();
            }

            // create text XML reader
            TagReader reader = new TagTextReaderImpl(inputUri, this, inputContext, tmpResolver);


            if (_useAsync)
            {
                reader = TagAsyncCheckReader.CreateAsyncCheckWrapper(reader);
            }
            return reader;
        }
        internal TagReader CreateReader(Stream input, Uri baseUri, string baseUriString, TagParserContext inputContext)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (baseUriString == null)
            {
                if (baseUri == null)
                {
                    baseUriString = string.Empty;
                }
                else
                {
                    baseUriString = baseUri.ToString();
                }
            }

            // create text XML reader
            TagReader reader = new TagTextReaderImpl(input, null, 0, this, baseUri, baseUriString, inputContext, _closeInput);


            if (_useAsync)
            {
                reader = TagAsyncCheckReader.CreateAsyncCheckWrapper(reader);
            }

            return reader;
        }
        internal TagReader CreateReader(TextReader input, string baseUriString, TagParserContext inputContext)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (baseUriString == null)
            {
                baseUriString = string.Empty;
            }

            // create xml text reader
            TagReader reader = new TagTextReaderImpl(input, this, baseUriString, inputContext);


            if (_useAsync)
            {
                reader = TagAsyncCheckReader.CreateAsyncCheckWrapper(reader);
            }

            return reader;
        }
        internal TagReader CreateReader(TagReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            // wrap with conformance layer (if needed)
            //return AddConformanceWrapper(reader);
            return null;
        }

        // TagResolver
        internal TagResolver GetTagResolver()
        {
            return _tagResolver;
        }

        #endregion
        #region Public methods



        #endregion

    }
}
