using System;
using TagKit.Markup.Events;

namespace TagKit.Markup.Fundamental.Nodes
{

    /// <summary>
    /// Represents a node in the generated tree.
    /// </summary>
    abstract class Node : EventTarget, INode, IEquatable<INode>
    {
        #region Implementation of INode

        public NodeType NodeType { get; }
        public string NodeName { get; }
        public string BaseUri { get; }
        public Url BaseUrl { get; }
        public IDocument Owner { get; }
        public INode Parent { get; }
        public IElement ParentElement { get; }
        public bool HasChildNodes { get; }
        public INodeList ChildNodes { get; }
        public INode FirstChild { get; }
        public INode LastChild { get; }
        public INode PreviousSibling { get; }
        public INode NextSibling { get; }
        public string NodeValue { get; set; }
        public string TextContent { get; set; }
        public void Normalize()
        {
            throw new NotImplementedException();
        }

        public INode Clone(bool deep = true)
        {
            throw new NotImplementedException();
        }

        public bool Equals(INode otherNode)
        {
            throw new NotImplementedException();
        }

        public DocumentPositions CompareDocumentPosition(INode otherNode)
        {
            throw new NotImplementedException();
        }

        public bool Contains(INode otherNode)
        {
            throw new NotImplementedException();
        }

        public string LookupPrefix(string namespaceUri)
        {
            throw new NotImplementedException();
        }

        public string LookupNamespaceUri(string prefix)
        {
            throw new NotImplementedException();
        }

        public bool IsDefaultNamespace(string namespaceUri)
        {
            throw new NotImplementedException();
        }

        public INode InsertBefore(INode newElement, INode referenceElement)
        {
            throw new NotImplementedException();
        }

        public INode AppendChild(INode child)
        {
            throw new NotImplementedException();
        }

        public INode ReplaceChild(INode newChild, INode oldChild)
        {
            throw new NotImplementedException();
        }

        public INode RemoveChild(INode child)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IEquatable<INode>

        bool IEquatable<INode>.Equals(INode other)
        {
            return Equals(other);
        }

        #endregion
    }
}
