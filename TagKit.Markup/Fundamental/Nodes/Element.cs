using System;
using TagKit.Markup.Fundamental.Sets;

namespace TagKit.Markup.Fundamental.Nodes
{
    /// <summary>
    /// Represents an element node.
    /// </summary>
    class Element : Node, IElement
    {
        private Document owner;
        private string name;
        private string prefix;
        private object p;

        public Element(Document owner, string name, string prefix, object p)
        {
            this.owner = owner;
            this.name = name;
            this.prefix = prefix;
            this.p = p;
        }
        #region Implementation of IParentNode

        public IHtmlCollection<IElement> Children { get; }
        public IElement FirstElementChild { get; }
        public IElement LastElementChild { get; }
        public int ChildElementCount { get; }
        public void Append(params INode[] nodes)
        {
            throw new NotImplementedException();
        }

        public void Prepend(params INode[] nodes)
        {
            throw new NotImplementedException();
        }

        public IElement QuerySelector(string selectors)
        {
            throw new NotImplementedException();
        }

        public IHtmlCollection<IElement> QuerySelectorAll(string selectors)
        {
            throw new NotImplementedException();
        }

        #endregion

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

        #region Implementation of INonDocumentTypeChildNode

        public IElement NextElementSibling { get; }
        public IElement PreviousElementSibling { get; }

        #endregion

        #region Implementation of IElement

        public string NamespaceUri { get; }
        public string Prefix { get; }
        public string LocalName { get; }
        public string TagName { get; }
        public string Id { get; set; }
        public string ClassName { get; set; }
        public ITokenList ClassList { get; }
        public INamedNodeMap Attributes { get; }
        public string GetAttribute(string namespaceUri, string localName)
        {
            throw new NotImplementedException();
        }

        public string GetAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public void SetAttribute(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void SetAttribute(string namespaceUri, string name, string value)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAttribute(string namespaceUri, string localName)
        {
            throw new NotImplementedException();
        }

        public bool HasAttribute(string name)
        {
            throw new NotImplementedException();
        }

        public bool HasAttribute(string namespaceUri, string localName)
        {
            throw new NotImplementedException();
        }

        public IHtmlCollection<IElement> GetElementsByTagName(string tagName)
        {
            throw new NotImplementedException();
        }

        public IHtmlCollection<IElement> GetElementsByTagNameNS(string namespaceUri, string tagName)
        {
            throw new NotImplementedException();
        }

        public IHtmlCollection<IElement> GetElementsByClassName(string classNames)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
