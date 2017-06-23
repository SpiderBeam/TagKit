using System;

namespace TagKit.Markup
{
    internal class DtdParserProxy : IDtdParserAdapter
    {
        private TagTextReaderImpl tagTextReaderImpl;

        public DtdParserProxy(TagTextReaderImpl tagTextReaderImpl)
        {
            this.tagTextReaderImpl = tagTextReaderImpl;
        }

        #region Implementation of IDtdParserAdapter

        public TagNameTable NameTable { get; }
        public ITagNamespaceResolver NamespaceResolver { get; }
        public Uri BaseUri { get; }
        public char[] ParsingBuffer { get; }
        public int ParsingBufferLength { get; }
        public int CurrentPosition { get; set; }
        public int LineNo { get; }
        public int LineStartPosition { get; }
        public bool IsEof { get; }
        public int EntityStackLength { get; }
        public bool IsEntityEolNormalized { get; }
        public int ReadData()
        {
            throw new NotImplementedException();
        }

        public void OnNewLine(int pos)
        {
            throw new NotImplementedException();
        }

        public int ParseNumericCharRef(BufferBuilder internalSubsetBuilder)
        {
            throw new NotImplementedException();
        }

        public int ParseNamedCharRef(bool expand, BufferBuilder internalSubsetBuilder)
        {
            throw new NotImplementedException();
        }

        public void ParsePI(BufferBuilder sb)
        {
            throw new NotImplementedException();
        }

        public void ParseComment(BufferBuilder sb)
        {
            throw new NotImplementedException();
        }

        public bool PushEntity(IDtdEntityInfo entity, out int entityId)
        {
            throw new NotImplementedException();
        }

        public bool PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId)
        {
            throw new NotImplementedException();
        }

        public bool PushExternalSubset(string systemId, string publicId)
        {
            throw new NotImplementedException();
        }

        public void PushInternalDtd(string baseUri, string internalDtd)
        {
            throw new NotImplementedException();
        }

        public void OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo)
        {
            throw new NotImplementedException();
        }

        public void OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo)
        {
            throw new NotImplementedException();
        }

        public void Throw(Exception e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}