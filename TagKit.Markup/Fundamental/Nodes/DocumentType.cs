using System;

namespace TagKit.Markup.Fundamental.Nodes
{
    /// <summary>
    /// Represents the document type node.
    /// </summary>
    sealed class DocumentType : Node, IDocumentType
    {
        public DocumentType(Document document, string doctypeTokenName)
        {
            throw new NotImplementedException();
        }

        #region Implementation of IChildNode

        public void Before(params INode[] nodes)
        {
            throw new NotImplementedException();
        }

        public void After(params INode[] nodes)
        {
            throw new NotImplementedException();
        }

        public void Replace(params INode[] nodes)
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IDocumentType

        public string Name { get; }
        public string PublicIdentifier { get; }
        public string SystemIdentifier { get; }

        #endregion
    }
}
