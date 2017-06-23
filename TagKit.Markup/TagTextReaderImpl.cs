using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using TagKit.Markup.Properties;

namespace TagKit.Markup
{
    internal partial class TagTextReaderImpl : TagReader, ITagLineInfo, ITagNamespaceResolver
    {
        private static UTF8Encoding s_utf8BomThrowing;
        private static UTF8Encoding UTF8BomThrowing =>
    s_utf8BomThrowing ?? (s_utf8BomThrowing = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true, throwOnInvalidBytes: true));

        #region ParsingState
        /// <summary>
        /// Parsing state (aka. scanner data) - holds parsing buffer and entity input data information
        /// </summary>
        private struct ParsingState
        {
            // character buffer
            internal char[] chars;
            internal int charPos;
            internal int charsUsed;
            internal Encoding encoding;
            internal bool appendMode;

            // input stream & byte buffer
            internal Stream stream;
            internal Decoder decoder;
            internal byte[] bytes;
            internal int bytePos;
            internal int bytesUsed;

            // input text reader
            internal TextReader textReader;

            // current line number & position
            internal int lineNo;
            internal int lineStartPos;

            // base uri of the current entity
            internal string baseUriStr;
            internal Uri baseUri;

            // eof flag of the entity
            internal bool isEof;
            internal bool isStreamEof;

            // entity type & id
            internal IDtdEntityInfo entity;
            internal int entityId;

            // normalization
            internal bool eolNormalized;

#if !SILVERLIGHT // Needed only for XmlTextReader (reporting of entities)
            // EndEntity reporting
            internal bool entityResolvedManually;
#endif

            internal void Clear()
            {
                chars = null;
                charPos = 0;
                charsUsed = 0;
                encoding = null;
                stream = null;
                decoder = null;
                bytes = null;
                bytePos = 0;
                bytesUsed = 0;
                textReader = null;
                lineNo = 1;
                lineStartPos = -1;
                baseUriStr = string.Empty;
                baseUri = null;
                isEof = false;
                isStreamEof = false;
                eolNormalized = true;
#if !SILVERLIGHT // Needed only for XmlTextReader (reporting of entities)
                entityResolvedManually = false;
#endif
            }

            internal void Close(bool closeInput)
            {
                if (closeInput)
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    else if (textReader != null)
                    {
                        textReader.Close();
                    }
                }
            }

            internal int LineNo
            {
                get
                {
                    return lineNo;
                }
            }

            internal int LinePos
            {
                get
                {
                    return charPos - lineStartPos;
                }
            }
        }



        #endregion
        #region NodeData
        private class NodeData : IComparable
        {
            // static instance with no data - is used when XmlTextReader is closed
            static volatile NodeData s_None;

            // NOTE: Do not use this property for reference comparison. It may not be unique.
            internal static NodeData None
            {
                get
                {
                    if (s_None == null)
                    {
                        // no locking; s_None is immutable so it's not a problem that it may get initialized more than once
                        s_None = new NodeData();
                    }
                    return s_None;
                }
            }

            // type
            internal NodeType type;

            // name
            internal string localName;
            internal string prefix;
            internal string ns;
            internal string nameWPrefix;

            // value:
            // value == null -> the value is kept in the 'chars' buffer starting at valueStartPos and valueLength long
            string value;
            char[] chars;
            int valueStartPos;
            int valueLength;

            // main line info
            internal LineInfo lineInfo;

            // second line info
            internal LineInfo lineInfo2;

            // quote char for attributes
            internal char quoteChar;

            // depth
            internal int depth;

            // empty element / default attribute
            bool isEmptyOrDefault;

            // entity id
            internal int entityId;

            // helper members
            internal bool xmlContextPushed;

#if !SILVERLIGHT // Needed only for XmlTextReader (reporting of entities)
            // attribute value chunks
            internal NodeData nextAttrValueChunk;
#endif

            // type info
            internal object schemaType;
            internal object typedValue;

            internal NodeData()
            {
                Clear(NodeType.None);
                xmlContextPushed = false;
            }

            internal int LineNo
            {
                get
                {
                    return lineInfo.lineNo;
                }
            }

            internal int LinePos
            {
                get
                {
                    return lineInfo.linePos;
                }
            }

            internal bool IsEmptyElement
            {
                get
                {
                    return type == NodeType.Element && isEmptyOrDefault;
                }
                set
                {
                    Debug.Assert(type == NodeType.Element);
                    isEmptyOrDefault = value;
                }
            }

            internal bool IsDefaultAttribute
            {
                get
                {
                    return type == NodeType.Attribute && isEmptyOrDefault;
                }
                set
                {
                    Debug.Assert(type == NodeType.Attribute);
                    isEmptyOrDefault = value;
                }
            }

            internal bool ValueBuffered
            {
                get
                {
                    return value == null;
                }
            }

            internal string StringValue
            {
                get
                {
                    Debug.Assert(valueStartPos >= 0 || this.value != null, "Value not ready.");

                    if (this.value == null)
                    {
                        this.value = new string(chars, valueStartPos, valueLength);
                    }
                    return this.value;
                }
            }

            internal void TrimSpacesInValue()
            {
                if (ValueBuffered)
                {
                    TagTextReaderImpl.StripSpaces(chars, valueStartPos, ref valueLength);
                }
                else
                {
                    value = TagTextReaderImpl.StripSpaces(value);
                }
            }

            internal void Clear(NodeType type)
            {
                this.type = type;
                ClearName();
                value = string.Empty;
                valueStartPos = -1;
                nameWPrefix = string.Empty;
                schemaType = null;
                typedValue = null;
            }

            internal void ClearName()
            {
                localName = string.Empty;
                prefix = string.Empty;
                ns = string.Empty;
                nameWPrefix = string.Empty;
            }

            internal void SetLineInfo(int lineNo, int linePos)
            {
                lineInfo.Set(lineNo, linePos);
            }

            internal void SetLineInfo2(int lineNo, int linePos)
            {
                lineInfo2.Set(lineNo, linePos);
            }

            internal void SetValueNode(NodeType type, string value)
            {
                Debug.Assert(value != null);

                this.type = type;
                ClearName();
                this.value = value;
                this.valueStartPos = -1;
            }

            internal void SetValueNode(NodeType type, char[] chars, int startPos, int len)
            {
                this.type = type;
                ClearName();

                this.value = null;
                this.chars = chars;
                this.valueStartPos = startPos;
                this.valueLength = len;
            }

            internal void SetNamedNode(NodeType type, string localName)
            {
                SetNamedNode(type, localName, string.Empty, localName);
            }

            internal void SetNamedNode(NodeType type, string localName, string prefix, string nameWPrefix)
            {
                Debug.Assert(localName != null);
                Debug.Assert(localName.Length > 0);

                this.type = type;
                this.localName = localName;
                this.prefix = prefix;
                this.nameWPrefix = nameWPrefix;
                this.ns = string.Empty;
                this.value = string.Empty;
                this.valueStartPos = -1;
            }

            internal void SetValue(string value)
            {
                this.valueStartPos = -1;
                this.value = value;
            }

            internal void SetValue(char[] chars, int startPos, int len)
            {
                this.value = null;
                this.chars = chars;
                this.valueStartPos = startPos;
                this.valueLength = len;
            }

            internal void OnBufferInvalidated()
            {
                if (this.value == null)
                {
                    Debug.Assert(valueStartPos != -1);
                    Debug.Assert(chars != null);
                    this.value = new string(chars, valueStartPos, valueLength);
                }
                valueStartPos = -1;
            }

            internal void CopyTo(int valueOffset, BufferBuilder sb)
            {
                if (value == null)
                {
                    Debug.Assert(valueStartPos != -1);
                    Debug.Assert(chars != null);
                    sb.Append(chars, valueStartPos + valueOffset, valueLength - valueOffset);
                }
                else
                {
                    if (valueOffset <= 0)
                    {
                        sb.Append(value);
                    }
                    else
                    {
                        sb.Append(value, valueOffset, value.Length - valueOffset);
                    }
                }
            }

            internal int CopyTo(int valueOffset, char[] buffer, int offset, int length)
            {
                if (value == null)
                {
                    Debug.Assert(valueStartPos != -1);
                    Debug.Assert(chars != null);
                    int copyCount = valueLength - valueOffset;
                    if (copyCount > length)
                    {
                        copyCount = length;
                    }
                    TagTextReaderImpl.BlockCopyChars(chars, valueStartPos + valueOffset, buffer, offset, copyCount);
                    return copyCount;
                }
                else
                {
                    int copyCount = value.Length - valueOffset;
                    if (copyCount > length)
                    {
                        copyCount = length;
                    }
                    value.CopyTo(valueOffset, buffer, offset, copyCount);
                    return copyCount;
                }
            }

            internal int CopyToBinary(IncrementalReadDecoder decoder, int valueOffset)
            {
                if (value == null)
                {
                    Debug.Assert(valueStartPos != -1);
                    Debug.Assert(chars != null);
                    return decoder.Decode(chars, valueStartPos + valueOffset, valueLength - valueOffset);
                }
                else
                {
                    return decoder.Decode(value, valueOffset, value.Length - valueOffset);
                }
            }

            internal void AdjustLineInfo(int valueOffset, bool isNormalized, ref LineInfo lineInfo)
            {
                if (valueOffset == 0)
                {
                    return;
                }
                if (valueStartPos != -1)
                {
                    TagTextReaderImpl.AdjustLineInfo(chars, valueStartPos, valueStartPos + valueOffset, isNormalized, ref lineInfo);
                }
                else
                {
                    TagTextReaderImpl.AdjustLineInfo(value, 0, valueOffset, isNormalized, ref lineInfo);
                }
            }

            // This should be inlined by JIT compiler
            internal string GetNameWPrefix(TagNameTable nt)
            {
                if (nameWPrefix != null)
                {
                    return nameWPrefix;
                }
                else
                {
                    return CreateNameWPrefix(nt);
                }
            }

            internal string CreateNameWPrefix(TagNameTable nt)
            {
                Debug.Assert(nameWPrefix == null);
                if (prefix.Length == 0)
                {
                    nameWPrefix = localName;
                }
                else
                {
                    nameWPrefix = nt.Add(string.Concat(prefix, ":", localName));
                }
                return nameWPrefix;
            }

            int IComparable.CompareTo(object obj)
            {
                NodeData other = obj as NodeData;
                if (other != null)
                {
                    if (Ref.Equal(localName, other.localName))
                    {
                        if (Ref.Equal(ns, other.ns))
                        {
                            return 0;
                        }
                        else
                        {
                            return string.CompareOrdinal(ns, other.ns);
                        }
                    }
                    else
                    {
                        return string.CompareOrdinal(localName, other.localName);
                    }
                }
                else
                {
                    Debug.Assert(false, "We should never get to this point.");
                    // 'other' is null, 'this' is not null. Always return 1, like "".CompareTo(null).
                    return 1;
                }
            }
        }

        #endregion

        #region TagContext
        private class TagContext
        {
            internal TagSpace xmlSpace;
            internal string xmlLang;
            internal string defaultNamespace;
            internal TagContext previousContext;

            internal TagContext()
            {
                xmlSpace = TagSpace.None;
                xmlLang = string.Empty;
                defaultNamespace = string.Empty;
                previousContext = null;
            }

            internal TagContext(TagContext previousContext)
            {
                this.xmlSpace = previousContext.xmlSpace;
                this.xmlLang = previousContext.xmlLang;
                this.defaultNamespace = previousContext.defaultNamespace;
                this.previousContext = previousContext;
            }
        }

        #endregion
        #region Private helper types
        /// <summary>
        /// ParsingFunction = what should the reader do when the next Read() is called
        /// </summary>
        private enum ParsingFunction
        {
            ElementContent = 0,
            NoData,
            SwitchToInteractive,
            SwitchToInteractiveXmlDecl,
            DocumentContent,
            MoveToElementContent,
            PopElementContext,
            PopEmptyElementContext,
            ResetAttributesRootLevel,
            Error,
            Eof,
            ReaderClosed,
            EntityReference,
            InIncrementalRead,
            XmlDeclarationFragment,
            GoToEof,
            PartialTextValue,

            // these two states must be last; see InAttributeValueIterator property
            InReadAttributeValue,
            InReadValueChunk,
            InReadContentAsBinary,
            InReadElementContentAsBinary,
        }
        private enum ParsingMode
        {
            Full,
            SkipNode,
            SkipContent,
        }
        private enum EntityType
        {
            CharacterDec,
            CharacterHex,
            CharacterNamed,
            Expanded,
            Skipped,
            FakeExpanded,
        }
        private enum EntityExpandType
        {
            All,
            OnlyGeneral,
        }
        private enum IncrementalReadState
        {
            // Following values are used in ReadText, ReadBase64 and ReadBinHex (V1 streaming methods)
            Text,
            StartTag,
            PI,
            CDATA,
            Comment,
            Attributes,
            AttributeValue,
            ReadData,
            EndElement,
            End,

            // Following values are used in ReadTextChunk, ReadContentAsBase64 and ReadBinHexChunk (V2 streaming methods)
            ReadValueChunk_OnCachedValue,
            ReadValueChunk_OnPartialValue,

            ReadContentAsBinary_OnCachedValue,
            ReadContentAsBinary_OnPartialValue,
            ReadContentAsBinary_End,
        }

        #endregion

        #region Fields
        private readonly bool _useAsync;
        #region Later Init Fileds

        //later init means in the construction stage, do not open filestream and do not read any data from Stream/TextReader
        //the purpose is to make the Create of XmlReader do not block on IO.
        private class LaterInitParam
        {
            public bool useAsync = false;

            public Stream inputStream;
            public byte[] inputBytes;
            public int inputByteCount;
            public Uri inputbaseUri;
            public string inputUriStr;
            public TagResolver inputUriResolver;
            public TagParserContext inputContext;
            public TextReader inputTextReader;

            public InitInputType initType = InitInputType.Invalid;
        }

        private LaterInitParam _laterInitParam = null;

        private enum InitInputType
        {
            UriString,
            Stream,
            TextReader,
            Invalid
        }

        #endregion
        // XmlCharType instance
        private XmlCharType _xmlCharType = XmlCharType.Instance;

        // current parsing state (aka. scanner data) 
        private ParsingState _ps;

        // parsing function = what to do in the next Read() (3-items-long stack, usually used just 2 level)
        private ParsingFunction _parsingFunction;
        private ParsingFunction _nextParsingFunction;
        private ParsingFunction _nextNextParsingFunction;

        // stack of nodes
        private NodeData[] _nodes;
        // current node
        private NodeData _curNode;

        // current index
        private int _index = 0;

        // attributes info
        private int _curAttrIndex = -1;
        private int _attrCount;
        private int _attrHashtable;
        private int _attrDuplWalkCount;
        private bool _attrNeedNamespaceLookup;
        private bool _fullAttrCleanup;
        private NodeData[] _attrDuplSortingArray;

        // name table
        private TagNameTable _nameTable;
        private bool _nameTableFromSettings;

        // resolver
        private TagResolver _xmlResolver;

        // settings
        private bool _normalize;
        private bool _supportNamespaces = true;
        private WhitespaceHandling _whitespaceHandling;
        private DtdProcessing _dtdProcessing = DtdProcessing.Ignore;
        private bool _ignorePIs;
        private bool _ignoreComments;
        private bool _checkCharacters;
        private int _lineNumberOffset;
        private int _linePositionOffset;
        private bool _closeInput;
        private long _maxCharactersInDocument;
        private long _maxCharactersFromEntities;

        // this flag enables XmlTextReader backwards compatibility; 
        // when false, the reader has been created via XmlReader.Create
        private bool _v1Compat;

        // namespace handling
        private TagNamespaceManager _namespaceManager;
        private string _lastPrefix = string.Empty;

        // tag context (xml:space, xml:lang, default namespace)
        private TagContext _xmlContext;

        // stack of parsing states (=stack of entities)
        private ParsingState[] _parsingStatesStack;
        private int _parsingStatesStackTop = -1;

        // current node base uri and encoding
        private string _reportedBaseUri;
        private Encoding _reportedEncoding;

        // DTD
        private IDtdInfo _dtdInfo;

        // fragment parsing
        private NodeType _fragmentType = NodeType.Document;
        private TagParserContext _fragmentParserContext;

        // incremental read
        private IncrementalReadDecoder _incReadDecoder;
        private IncrementalReadState _incReadState;
        private LineInfo _incReadLineInfo;
        private BinHexDecoder _binHexDecoder;
        private Base64Decoder _base64Decoder;

        // ReadAttributeValue helpers

        // Validation helpers


        // misc
        private bool _addDefaultAttributesAndNormalize;
        private BufferBuilder _stringBuilder;
        private bool _rootElementParsed;
        private bool _standalone;
        private int _nextEntityId = 1;
        private ParsingMode _parsingMode;
        private ReadState _readState = ReadState.Initial;
        private int _documentStartBytePos;
        private int _readValueOffset;

        // Counters for security settings
        private long _charactersInDocument;
        private long _charactersFromEntities;

        // All entities that are currently being processed
        private Dictionary<IDtdEntityInfo, IDtdEntityInfo> _currentEntities;

        // DOM helpers

        // Outer XmlReader exposed to the user - either XmlTextReader or XmlTextReaderImpl (when created via XmlReader.Create).
        // Virtual methods called from within XmlTextReaderImpl must be called on the outer reader so in case the user overrides
        // some of the XmlTextReader methods we will call the overridden version.
        private TagReader _outerReader;

        //
        // Atomized string constants
        //
        private string _xml;
        private string _xmlNs;


        #endregion

        #region Constants

        private const int MaxBytesToMove = 128;
        private const int ApproxXmlDeclLength = 80;
        private const int NodesInitialSize = 8;
        private const int InitialAttributesCount = 4;
        private const int InitialParsingStateStackSize = 2;
        private const int InitialParsingStatesDepth = 2;
        private const int DtdChidrenInitialSize = 2;
        private const int MaxByteSequenceLen = 6;  // max bytes per character
        private const int MaxAttrDuplWalkCount = 250;
        private const int MinWhitespaceLookahedCount = 4096;

        private const string XmlDeclarationBeginning = "<?xml";
        private Stream input;
        private object p;
        private int v;
        private TagReaderSettings tagReaderSettings;
        private Uri baseUri;
        private string baseUriString;
        private TagParserContext inputContext;
        private TextReader input1;


        #endregion
        #region Constructors
        /// <summary>
        /// This constructor is used when creating TagTextReaderImpl reader via "TagReader.Create(..)"
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="settings"></param>
        /// <param name="context"></param>
        public TagTextReaderImpl(TagResolver resolver, TagReaderSettings settings, TagParserContext context)
        {
            _useAsync = settings.Async;
            _v1Compat = false;
            _outerReader = this;

            _xmlContext = new TagContext();

            // create or get nametable and namespace manager from XmlParserContext
            TagNameTable nt = settings.NameTable;
            if (context == null)
            {
                if (nt == null)
                {
                    nt = new NameTable();
                    Debug.Assert(_nameTableFromSettings == false);
                }
                else
                {
                    _nameTableFromSettings = true;
                }
                _nameTable = nt;
                _namespaceManager = new TagNamespaceManager(nt);
            }
            else
            {
                SetupFromParserContext(context, settings);
                nt = _nameTable;
            }

            nt.Add(string.Empty);
            _xml = nt.Add("xml");
            _xmlNs = nt.Add("xmlns");

            _xmlResolver = resolver;

            Debug.Assert(_index == 0);

            _nodes = new NodeData[NodesInitialSize];
            _nodes[0] = new NodeData();
            _curNode = _nodes[0];

            _stringBuilder = new BufferBuilder();


            _whitespaceHandling = (settings.IgnoreWhitespace) ? WhitespaceHandling.Significant : WhitespaceHandling.All;
            _normalize = true;
            _ignorePIs = settings.IgnoreProcessingInstructions;
            _ignoreComments = settings.IgnoreComments;
            _checkCharacters = settings.CheckCharacters;
            _lineNumberOffset = settings.LineNumberOffset;
            _linePositionOffset = settings.LinePositionOffset;
            _ps.lineNo = _lineNumberOffset + 1;
            _ps.lineStartPos = -_linePositionOffset - 1;
            _curNode.SetLineInfo(_ps.LineNo - 1, _ps.LinePos - 1);
            _dtdProcessing = settings.DtdProcessing;
            _maxCharactersInDocument = settings.MaxCharactersInDocument;
            _maxCharactersFromEntities = settings.MaxCharactersFromEntities;

            _charactersInDocument = 0;
            _charactersFromEntities = 0;

            _fragmentParserContext = context;

            _parsingFunction = ParsingFunction.SwitchToInteractiveXmlDecl;
            _nextParsingFunction = ParsingFunction.DocumentContent;

            switch (settings.ConformanceLevel)
            {
                case ConformanceLevel.Auto:
                    _fragmentType = NodeType.None;
                    break;
                case ConformanceLevel.Fragment:
                    _fragmentType = NodeType.Element;
                    break;
                case ConformanceLevel.Document:
                    _fragmentType = NodeType.Document;
                    break;
                default:
                    Debug.Assert(false);
                    goto case ConformanceLevel.Document;
            }
        }
        internal TagTextReaderImpl(string uriStr, TagReaderSettings settings, TagParserContext context, TagResolver uriResolver)
            : this(settings.GetTagResolver(), settings, context)
        {
            Uri baseUri = uriResolver.ResolveUri(null, uriStr);
            string baseUriStr = baseUri.ToString();

            // get BaseUri from XmlParserContext
            if (context != null)
            {
                if (context.BaseURI != null && context.BaseURI.Length > 0 &&
                    !UriEqual(baseUri, baseUriStr, context.BaseURI, settings.GetTagResolver()))
                {
                    if (baseUriStr.Length > 0)
                    {
                        Throw(Resources.Xml_DoubleBaseUri);
                    }
                    Debug.Assert(baseUri == null);
                    baseUriStr = context.BaseURI;
                }
            }

            _reportedBaseUri = baseUriStr;
            _closeInput = true;
            _laterInitParam = new LaterInitParam();
            _laterInitParam.inputUriStr = uriStr;
            _laterInitParam.inputbaseUri = baseUri;
            _laterInitParam.inputContext = context;
            _laterInitParam.inputUriResolver = uriResolver;
            _laterInitParam.initType = InitInputType.UriString;
            if (!settings.Async)
            {
                //if not set Async flag, finish the init in create stage.
                FinishInitUriString();
            }
            else
            {
                _laterInitParam.useAsync = true;
            }
        }

        public TagTextReaderImpl(Stream input, object p, int v, TagReaderSettings tagReaderSettings, Uri baseUri, string baseUriString, TagParserContext inputContext, bool _closeInput)
        {
            this.input = input;
            this.p = p;
            this.v = v;
            this.tagReaderSettings = tagReaderSettings;
            this.baseUri = baseUri;
            this.baseUriString = baseUriString;
            this.inputContext = inputContext;
            this._closeInput = _closeInput;
        }

        public TagTextReaderImpl(TextReader input1, TagReaderSettings tagReaderSettings, string baseUriString, TagParserContext inputContext)
        {
            this.input1 = input1;
            this.tagReaderSettings = tagReaderSettings;
            this.baseUriString = baseUriString;
            this.inputContext = inputContext;
        }

        bool UriEqual(Uri uri1, string uri1Str, string uri2Str, TagResolver resolver)
        {
            if (resolver == null)
            {
                return uri1Str == uri2Str;
            }
            if (uri1 == null)
            {
                uri1 = resolver.ResolveUri(null, uri1Str);
            }
            Uri uri2 = resolver.ResolveUri(null, uri2Str);
            return uri1.Equals(uri2);
        }
        private void FinishInitUriString()
        {
            Stream stream = (Stream)_laterInitParam.inputUriResolver.GetEntity(_laterInitParam.inputbaseUri, string.Empty, typeof(Stream));

            if (stream == null)
            {
                throw new TagException(Resources.Xml_CannotResolveUrl, _laterInitParam.inputUriStr);
            }

            Encoding enc = null;
            // get Encoding from XmlParserContext
            if (_laterInitParam.inputContext != null)
            {
                enc = _laterInitParam.inputContext.Encoding;
            }

            try
            {
                // init ParsingState
                InitStreamInput(_laterInitParam.inputbaseUri, _reportedBaseUri, stream, null, 0, enc);

                _reportedEncoding = _ps.encoding;

                // parse DTD
                if (_laterInitParam.inputContext != null && _laterInitParam.inputContext.HasDtdInfo)
                {
                    ProcessDtdFromParserContext(_laterInitParam.inputContext);
                }
            }
            catch
            {
                stream.Dispose();
                throw;
            }
            _laterInitParam = null;
        }

        #endregion
        private void SetupFromParserContext(TagParserContext context, TagReaderSettings settings)
        {
            Debug.Assert(context != null);

            // setup nameTable
            TagNameTable nt = settings.NameTable;
            _nameTableFromSettings = (nt != null);

            // get name table from namespace manager in XmlParserContext, if available; 
            if (context.NamespaceManager != null)
            {
                // must be the same as XmlReaderSettings.NameTable, or null
                if (nt != null && nt != context.NamespaceManager.NameTable)
                {
                    throw new TagException(Resources.Xml_NametableMismatch);
                }
                // get the namespace manager from context
                _namespaceManager = context.NamespaceManager;
                _xmlContext.defaultNamespace = _namespaceManager.LookupNamespace(string.Empty);

                // get the nametable from ns manager
                nt = _namespaceManager.NameTable;

                Debug.Assert(nt != null);
                Debug.Assert(context.NameTable == null || context.NameTable == nt, "This check should have been done in XmlParserContext constructor.");
            }
            // get name table directly from XmlParserContext
            else if (context.NameTable != null)
            {
                // must be the same as XmlReaderSettings.NameTable, or null
                if (nt != null && nt != context.NameTable)
                {
                    throw new TagException(Resources.Xml_NametableMismatch, string.Empty);
                }
                nt = context.NameTable;
            }
            // no nametable provided -> create a new one
            else if (nt == null)
            {
                nt = new NameTable();
                Debug.Assert(_nameTableFromSettings == false);
            }
            _nameTable = nt;

            // make sure we have namespace manager
            if (_namespaceManager == null)
            {
                _namespaceManager = new TagNamespaceManager(nt);
            }

            // copy xml:space and xml:lang
            _xmlContext.xmlSpace = context.XmlSpace;
            _xmlContext.xmlLang = context.XmlLang;
        }


#if !SILVERLIGHT_DISABLE_SECURITY
        [System.Security.SecuritySafeCritical]
#endif
        static internal unsafe void AdjustLineInfo(char[] chars, int startPos, int endPos, bool isNormalized, ref LineInfo lineInfo)
        {
            Debug.Assert(startPos >= 0);
            Debug.Assert(endPos < chars.Length);
            Debug.Assert(startPos <= endPos);

            fixed (char* pChars = &chars[startPos])
            {
                AdjustLineInfo(pChars, endPos - startPos, isNormalized, ref lineInfo);
            }
        }

#if !SILVERLIGHT_DISABLE_SECURITY
        [System.Security.SecuritySafeCritical]
#endif
        static internal unsafe void AdjustLineInfo(string str, int startPos, int endPos, bool isNormalized, ref LineInfo lineInfo)
        {
            Debug.Assert(startPos >= 0);
            Debug.Assert(endPos < str.Length);
            Debug.Assert(startPos <= endPos);

            fixed (char* pChars = str)
            {
                AdjustLineInfo(pChars + startPos, endPos - startPos, isNormalized, ref lineInfo);
            }
        }

        [System.Security.SecurityCritical]
        static internal unsafe void AdjustLineInfo(char* pChars, int length, bool isNormalized, ref LineInfo lineInfo)
        {
            int lastNewLinePos = -1;
            for (int i = 0; i < length; i++)
            {
                switch (pChars[i])
                {
                    case '\n':
                        lineInfo.lineNo++;
                        lastNewLinePos = i;
                        break;
                    case '\r':
                        if (isNormalized)
                        {
                            break;
                        }
                        lineInfo.lineNo++;
                        lastNewLinePos = i;
                        if (i + 1 < length && pChars[i + 1] == '\n')
                        {
                            i++;
                            lastNewLinePos++;
                        }
                        break;
                }
            }
            if (lastNewLinePos >= 0)
            {
                lineInfo.linePos = length - lastNewLinePos;
            }
        }
        internal static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
        {
            // PERF: Buffer.BlockCopy is faster than Array.Copy
            Array.Copy(src, srcOffset, dst, dstOffset, count);
        }
        // StripSpaces removes spaces at the beginning and at the end of the value and replaces sequences of spaces with a single space
        internal static string StripSpaces(string value)
        {
            int len = value.Length;
            if (len <= 0)
            {
                return string.Empty;
            }

            int startPos = 0;
            StringBuilder norValue = null;

            while (value[startPos] == 0x20)
            {
                startPos++;
                if (startPos == len)
                {
                    return " ";
                }
            }

            int i;
            for (i = startPos; i < len; i++)
            {
                if (value[i] == 0x20)
                {
                    int j = i + 1;
                    while (j < len && value[j] == 0x20)
                    {
                        j++;
                    }
                    if (j == len)
                    {
                        if (norValue == null)
                        {
                            return value.Substring(startPos, i - startPos);
                        }
                        else
                        {
                            norValue.Append(value, startPos, i - startPos);
                            return norValue.ToString();
                        }
                    }
                    if (j > i + 1)
                    {
                        if (norValue == null)
                        {
                            norValue = new StringBuilder(len);
                        }
                        norValue.Append(value, startPos, i - startPos + 1);
                        startPos = j;
                        i = j - 1;
                    }
                }
            }
            if (norValue == null)
            {
                return (startPos == 0) ? value : value.Substring(startPos, len - startPos);
            }
            else
            {
                if (i > startPos)
                {
                    norValue.Append(value, startPos, i - startPos);
                }
                return norValue.ToString();
            }
        }

        // StripSpaces removes spaces at the beginning and at the end of the value and replaces sequences of spaces with a single space
        internal static void StripSpaces(char[] value, int index, ref int len)
        {
            if (len <= 0)
            {
                return;
            }

            int startPos = index;
            int endPos = index + len;

            while (value[startPos] == 0x20)
            {
                startPos++;
                if (startPos == endPos)
                {
                    len = 1;
                    return;
                }
            }

            int offset = startPos - index;
            int i;
            for (i = startPos; i < endPos; i++)
            {
                char ch;
                if ((ch = value[i]) == 0x20)
                {
                    int j = i + 1;
                    while (j < endPos && value[j] == 0x20)
                    {
                        j++;
                    }
                    if (j == endPos)
                    {
                        offset += (j - i);
                        break;
                    }
                    if (j > i + 1)
                    {
                        offset += (j - i - 1);
                        i = j - 1;
                    }
                }
                value[i - offset] = ch;
            }
            len -= offset;
        }
        private void InitStreamInput(Uri baseUri, Stream stream, Encoding encoding)
        {
            Debug.Assert(baseUri != null);
            InitStreamInput(baseUri, baseUri.ToString(), stream, null, 0, encoding);
        }
        private void InitStreamInput(Uri baseUri, string baseUriStr, Stream stream, byte[] bytes, int byteCount, Encoding encoding)
        {
            Debug.Assert(_ps.charPos == 0 && _ps.charsUsed == 0 && _ps.textReader == null);
            Debug.Assert(baseUriStr != null);
            Debug.Assert(baseUri == null || (baseUri.ToString().Equals(baseUriStr)));

            _ps.stream = stream;
            _ps.baseUri = baseUri;
            _ps.baseUriStr = baseUriStr;

            // take over the byte buffer allocated in XmlReader.Create, if available
            int bufferSize;
            if (bytes != null)
            {
                _ps.bytes = bytes;
                _ps.bytesUsed = byteCount;
                bufferSize = _ps.bytes.Length;
            }
            else
            {
                // allocate the byte buffer 
                if (_laterInitParam != null && _laterInitParam.useAsync)
                {
                    bufferSize = AsyncBufferSize;
                }
                else
                {
                    bufferSize = TagReader.CalcBufferSize(stream);
                }
                if (_ps.bytes == null || _ps.bytes.Length < bufferSize)
                {
                    _ps.bytes = new byte[bufferSize];
                }
            }

            // allocate char buffer
            if (_ps.chars == null || _ps.chars.Length < bufferSize + 1)
            {
                _ps.chars = new char[bufferSize + 1];
            }

            // make sure we have at least 4 bytes to detect the encoding (no preamble of System.Text supported encoding is longer than 4 bytes)
            _ps.bytePos = 0;
            while (_ps.bytesUsed < 4 && _ps.bytes.Length - _ps.bytesUsed > 0)
            {
                int read = stream.Read(_ps.bytes, _ps.bytesUsed, _ps.bytes.Length - _ps.bytesUsed);
                if (read == 0)
                {
                    _ps.isStreamEof = true;
                    break;
                }
                _ps.bytesUsed += read;
            }

            // detect & setup encoding
            if (encoding == null)
            {
                encoding = DetectEncoding();
            }
            SetupEncoding(encoding);

            // eat preamble 
            byte[] preamble = _ps.encoding.GetPreamble();
            int preambleLen = preamble.Length;
            int i;
            for (i = 0; i < preambleLen && i < _ps.bytesUsed; i++)
            {
                if (_ps.bytes[i] != preamble[i])
                {
                    break;
                }
            }
            if (i == preambleLen)
            {
                _ps.bytePos = preambleLen;
            }

            _documentStartBytePos = _ps.bytePos;

            _ps.eolNormalized = !_normalize;

            // decode first characters
            _ps.appendMode = true;
            ReadData();
        }
        private void SetupEncoding(Encoding encoding)
        {
            if (encoding == null)
            {
                Debug.Assert(_ps.charPos == 0);
                _ps.encoding = Encoding.UTF8;
                _ps.decoder = new SafeAsciiDecoder();
            }
            else
            {
                _ps.encoding = encoding;

                switch (_ps.encoding.WebName)
                { // Encoding.Codepage is not supported in Silverlight
                    case "utf-16":
                        _ps.decoder = new UTF16Decoder(false);
                        break;
                    case "utf-16BE":
                        _ps.decoder = new UTF16Decoder(true);
                        break;
                    default:
                        _ps.decoder = encoding.GetDecoder();
                        break;
                }
            }
        }

        /// <summary>
        /// Stream input only: detect encoding from the first 4 bytes of the byte buffer starting at ps.bytes[ps.bytePos]
        /// </summary>
        /// <returns></returns>
        private Encoding DetectEncoding()
        {
            Debug.Assert(_ps.bytes != null);
            Debug.Assert(_ps.bytePos == 0);

            if (_ps.bytesUsed < 2)
            {
                return null;
            }
            int first2Bytes = _ps.bytes[0] << 8 | _ps.bytes[1];
            int next2Bytes = (_ps.bytesUsed >= 4) ? (_ps.bytes[2] << 8 | _ps.bytes[3]) : 0;

            switch (first2Bytes)
            {
                case 0xFEFF:
                    return Encoding.BigEndianUnicode;
                case 0xFFFE:
                    return Encoding.Unicode;
                case 0x3C00:
                    return Encoding.Unicode;
                case 0x003C:
                    return Encoding.BigEndianUnicode;
                case 0x4C6F:
                    if (next2Bytes == 0xA794)
                    {
                        Throw(Resources.Xml_UnknownEncoding, "ebcdic");
                    }
                    break;
                case 0xEFBB:
                    if ((next2Bytes & 0xFF00) == 0xBF00)
                    {
                        return UTF8BomThrowing;
                    }
                    break;
            }
            // Default encoding is ASCII (using SafeAsciiDecoder) until we read xml declaration. 
            // If we set UTF8 encoding now, it will throw exceptions (=slow) when decoding non-UTF8-friendly 
            // characters after the xml declaration, which may be perfectly valid in the encoding 
            // specified in xml declaration.
            return null;
        }
        private void ProcessDtdFromParserContext(TagParserContext context)
        {
            Debug.Assert(context != null && context.HasDtdInfo);

            switch (_dtdProcessing)
            {
                case DtdProcessing.Prohibit:
                    ThrowWithoutLineInfo(Resources.Xml_DtdIsProhibitedEx);
                    break;
                case DtdProcessing.Ignore:
                    // do nothing
                    break;

                case DtdProcessing.Parse:
                    ParseDtdFromParserContext();
                    break;

                default:
                    Debug.Assert(false, "Unhandled DtdProcessing enumeration value.");
                    break;
            }
        }
        private void ParseDtdFromParserContext()
        {
            Debug.Assert(_dtdInfo == null && _fragmentParserContext != null && _fragmentParserContext.HasDtdInfo);

            IDtdParser dtdParser = DtdParser.Create();

            // Parse DTD
            _dtdInfo = dtdParser.ParseFreeFloatingDtd(_fragmentParserContext.BaseURI, _fragmentParserContext.DocTypeName, _fragmentParserContext.PublicId,
                                                     _fragmentParserContext.SystemId, _fragmentParserContext.InternalSubset, new DtdParserProxy(this));

            if (_dtdInfo.HasDefaultAttributes || _dtdInfo.HasNonCDataAttributes)
            {
                _addDefaultAttributesAndNormalize = true;
            }
        }
        /// <summary>
        /// Reads more data to the character buffer, discarding already parsed chars / decoded bytes.
        /// </summary>
        /// <returns></returns>
        private int ReadData()
        {
            // Append Mode:  Append new bytes and characters to the buffers, do not rewrite them. Allocate new buffers
            //               if the current ones are full
            // Rewrite Mode: Reuse the buffers. If there is less than half of the char buffer left for new data, move 
            //               the characters that has not been parsed yet to the front of the buffer. Same for bytes.

            if (_ps.isEof)
            {
                return 0;
            }

            int charsRead;
            if (_ps.appendMode)
            {
                // the character buffer is full -> allocate a new one
                if (_ps.charsUsed == _ps.chars.Length - 1)
                {
                    // invalidate node values kept in buffer - applies to attribute values only
                    for (int i = 0; i < _attrCount; i++)
                    {
                        _nodes[_index + i + 1].OnBufferInvalidated();
                    }

                    char[] newChars = new char[_ps.chars.Length * 2];
                    BlockCopyChars(_ps.chars, 0, newChars, 0, _ps.chars.Length);
                    _ps.chars = newChars;
                }

                if (_ps.stream != null)
                {
                    // the byte buffer is full -> allocate a new one
                    if (_ps.bytesUsed - _ps.bytePos < MaxByteSequenceLen)
                    {
                        if (_ps.bytes.Length - _ps.bytesUsed < MaxByteSequenceLen)
                        {
                            byte[] newBytes = new byte[_ps.bytes.Length * 2];
                            BlockCopy(_ps.bytes, 0, newBytes, 0, _ps.bytesUsed);
                            _ps.bytes = newBytes;
                        }
                    }
                }

                charsRead = _ps.chars.Length - _ps.charsUsed - 1;
                if (charsRead > ApproxXmlDeclLength)
                {
                    charsRead = ApproxXmlDeclLength;
                }
            }
            else
            {
                int charsLen = _ps.chars.Length;
                if (charsLen - _ps.charsUsed <= charsLen / 2)
                {
                    // invalidate node values kept in buffer - applies to attribute values only
                    for (int i = 0; i < _attrCount; i++)
                    {
                        _nodes[_index + i + 1].OnBufferInvalidated();
                    }

                    // move unparsed characters to front, unless the whole buffer contains unparsed characters
                    int copyCharsCount = _ps.charsUsed - _ps.charPos;
                    if (copyCharsCount < charsLen - 1)
                    {
                        _ps.lineStartPos = _ps.lineStartPos - _ps.charPos;
                        if (copyCharsCount > 0)
                        {
                            BlockCopyChars(_ps.chars, _ps.charPos, _ps.chars, 0, copyCharsCount);
                        }
                        _ps.charPos = 0;
                        _ps.charsUsed = copyCharsCount;
                    }
                    else
                    {
                        char[] newChars = new char[_ps.chars.Length * 2];
                        BlockCopyChars(_ps.chars, 0, newChars, 0, _ps.chars.Length);
                        _ps.chars = newChars;
                    }
                }

                if (_ps.stream != null)
                {
                    // move undecoded bytes to the front to make some space in the byte buffer
                    int bytesLeft = _ps.bytesUsed - _ps.bytePos;
                    if (bytesLeft <= MaxBytesToMove)
                    {
                        if (bytesLeft == 0)
                        {
                            _ps.bytesUsed = 0;
                        }
                        else
                        {
                            BlockCopy(_ps.bytes, _ps.bytePos, _ps.bytes, 0, bytesLeft);
                            _ps.bytesUsed = bytesLeft;
                        }
                        _ps.bytePos = 0;
                    }
                }
                charsRead = _ps.chars.Length - _ps.charsUsed - 1;
            }

            if (_ps.stream != null)
            {
                if (!_ps.isStreamEof)
                {
                    // read new bytes
                    if (_ps.bytePos == _ps.bytesUsed && _ps.bytes.Length - _ps.bytesUsed > 0)
                    {
                        int read = _ps.stream.Read(_ps.bytes, _ps.bytesUsed, _ps.bytes.Length - _ps.bytesUsed);
                        if (read == 0)
                        {
                            _ps.isStreamEof = true;
                        }
                        _ps.bytesUsed += read;
                    }
                }

                int originalBytePos = _ps.bytePos;

                // decode chars
                charsRead = GetChars(charsRead);
                if (charsRead == 0 && _ps.bytePos != originalBytePos)
                {
                    // GetChars consumed some bytes but it was not enough bytes to form a character -> try again
                    return ReadData();
                }
            }
            else if (_ps.textReader != null)
            {
                // read chars
                charsRead = _ps.textReader.Read(_ps.chars, _ps.charsUsed, _ps.chars.Length - _ps.charsUsed - 1);
                _ps.charsUsed += charsRead;
            }
            else
            {
                charsRead = 0;
            }

            RegisterConsumedCharacters(charsRead, InEntity);

            if (charsRead == 0)
            {
                Debug.Assert(_ps.charsUsed < _ps.chars.Length);
                _ps.isEof = true;
            }
            _ps.chars[_ps.charsUsed] = (char)0;
            return charsRead;
        }
        private bool InEntity
        {
            get
            {
                return _parsingStatesStackTop >= 0;
            }
        }
        /// <summary>
        /// This method should be called every time the reader is about to consume some number of
        ///   characters from the input. It will count it against the security counters and
        ///   may throw if some of the security limits are exceeded.
        /// </summary>
        /// <param name="characters">Number of characters to be consumed.</param>
        /// <param name="inEntityReference">true if the characters are result of entity expansion.</param>
        private void RegisterConsumedCharacters(long characters, bool inEntityReference)
        {
            Debug.Assert(characters >= 0);
            if (_maxCharactersInDocument > 0)
            {
                long newCharactersInDocument = _charactersInDocument + characters;
                if (newCharactersInDocument < _charactersInDocument)
                {
                    // Integer overflow while counting
                    ThrowWithoutLineInfo(Resources.Xml_LimitExceeded, "MaxCharactersInDocument");
                }
                else
                {
                    _charactersInDocument = newCharactersInDocument;
                }
                if (_charactersInDocument > _maxCharactersInDocument)
                {
                    // The limit was exceeded for the total number of characters in the document
                    ThrowWithoutLineInfo(Resources.Xml_LimitExceeded, "MaxCharactersInDocument");
                }
            }

            if (_maxCharactersFromEntities > 0 && inEntityReference)
            {
                long newCharactersFromEntities = _charactersFromEntities + characters;
                if (newCharactersFromEntities < _charactersFromEntities)
                {
                    // Integer overflow while counting
                    ThrowWithoutLineInfo(Resources.Xml_LimitExceeded, "MaxCharactersFromEntities");
                }
                else
                {
                    _charactersFromEntities = newCharactersFromEntities;
                }
                if (_charactersFromEntities > _maxCharactersFromEntities)
                {
                    // The limit was exceeded for the number of characters from entities
                    ThrowWithoutLineInfo(Resources.Xml_LimitExceeded, "MaxCharactersFromEntities");
                }
            }
        }

        /// <summary>
        /// Stream input only: read bytes from stream and decodes them according to the current encoding
        /// </summary>
        /// <param name="maxCharsCount"></param>
        /// <returns></returns>
        private int GetChars(int maxCharsCount)
        {
            Debug.Assert(_ps.stream != null && _ps.decoder != null && _ps.bytes != null);
            Debug.Assert(maxCharsCount <= _ps.chars.Length - _ps.charsUsed - 1);

            // determine the maximum number of bytes we can pass to the decoder
            int bytesCount = _ps.bytesUsed - _ps.bytePos;
            if (bytesCount == 0)
            {
                return 0;
            }

            int charsCount;
            bool completed;
            try
            {
                // decode chars
                _ps.decoder.Convert(_ps.bytes, _ps.bytePos, bytesCount, _ps.chars, _ps.charsUsed, maxCharsCount, false, out bytesCount, out charsCount, out completed);
            }
            catch (ArgumentException)
            {
                InvalidCharRecovery(ref bytesCount, out charsCount);
            }

            // move pointers and return
            _ps.bytePos += bytesCount;
            _ps.charsUsed += charsCount;
            Debug.Assert(maxCharsCount >= charsCount);
            return charsCount;
        }
        private void InvalidCharRecovery(ref int bytesCount, out int charsCount)
        {
            int charsDecoded = 0;
            int bytesDecoded = 0;
            try
            {
                while (bytesDecoded < bytesCount)
                {
                    int chDec;
                    int bDec;
                    bool completed;
                    _ps.decoder.Convert(_ps.bytes, _ps.bytePos + bytesDecoded, 1, _ps.chars, _ps.charsUsed + charsDecoded, 1, false, out bDec, out chDec, out completed);
                    charsDecoded += chDec;
                    bytesDecoded += bDec;
                }
                Debug.Assert(false, "We should get an exception again.");
            }
            catch (ArgumentException)
            {
            }

            if (charsDecoded == 0)
            {
                Throw(_ps.charsUsed, Resources.Xml_InvalidCharInThisEncoding);
            }
            charsCount = charsDecoded;
            bytesCount = bytesDecoded;
        }


        internal static void BlockCopy(byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
        {
            Array.Copy(src, srcOffset, dst, dstOffset, count);
        }
        #region Throw methods: Sets the reader current position to pos, sets the error state and throws exception

        private void Throw(int pos, string res, string arg)
        {
            _ps.charPos = pos;
            Throw(res, arg);
        }

        private void Throw(int pos, string res, string[] args)
        {
            _ps.charPos = pos;
            Throw(res, args);
        }

        private void Throw(int pos, string res)
        {
            _ps.charPos = pos;
            Throw(res, string.Empty);
        }

        private void Throw(string res)
        {
            Throw(res, string.Empty);
        }

        private void Throw(string res, int lineNo, int linePos)
        {
            Throw(new TagException(res, string.Empty, lineNo, linePos, _ps.baseUriStr));
        }

        private void Throw(string res, string arg)
        {
            Throw(new TagException(res, arg, _ps.LineNo, _ps.LinePos, _ps.baseUriStr));
        }

        private void Throw(string res, string arg, int lineNo, int linePos)
        {
            Throw(new TagException(res, arg, lineNo, linePos, _ps.baseUriStr));
        }

        private void Throw(string res, string[] args)
        {
            Throw(new TagException(res, args, _ps.LineNo, _ps.LinePos, _ps.baseUriStr));
        }

        private void Throw(string res, string arg, Exception innerException)
        {
            Throw(res, new string[] { arg }, innerException);
        }

        private void Throw(string res, string[] args, Exception innerException)
        {
            Throw(new TagException(res, args, innerException, _ps.LineNo, _ps.LinePos, _ps.baseUriStr));
        }

        private void Throw(Exception e)
        {
            SetErrorState();
            TagException xmlEx = e as TagException;
            if (xmlEx != null)
            {
                _curNode.SetLineInfo(xmlEx.LineNumber, xmlEx.LinePosition);
            }
            throw e;
        }

        private void ReThrow(Exception e, int lineNo, int linePos)
        {
            Throw(new TagException(e.Message, (Exception)null, lineNo, linePos, _ps.baseUriStr));
        }

        private void ThrowWithoutLineInfo(string res)
        {
            Throw(new TagException(res, string.Empty, _ps.baseUriStr));
        }

        private void ThrowWithoutLineInfo(string res, string arg)
        {
            Throw(new TagException(res, arg, _ps.baseUriStr));
        }

        private void ThrowWithoutLineInfo(string res, string[] args, Exception innerException)
        {
            Throw(new TagException(res, args, innerException, 0, 0, _ps.baseUriStr));
        }

        private void ThrowInvalidChar(char[] data, int length, int invCharPos)
        {
            Throw(invCharPos, Resources.Xml_InvalidCharacter, TagException.BuildCharExceptionArgs(data, length, invCharPos));
        }

        private void SetErrorState()
        {
            _parsingFunction = ParsingFunction.Error;
            _readState = ReadState.Error;
        }



        #endregion
        #region Implementation of ITagLineInfo

        public bool HasLineInfo()
        {
            throw new NotImplementedException();
        }

        public int LineNumber { get; }
        public int LinePosition { get; }

        #endregion

        #region Implementation of ITagNamespaceResolver

        public IDictionary<string, string> GetNamespacesInScope(TagNamespaceScope scope)
        {
            throw new NotImplementedException();
        }

        public string LookupNamespace(string prefix)
        {
            throw new NotImplementedException();
        }

        public string LookupPrefix(string namespaceName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
