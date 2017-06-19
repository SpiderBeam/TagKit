using System;
using System.Linq;
using TagKit.Markup.Fundamental.Events;
using TagKit.Markup.Fundamental.Ranges;
using TagKit.Markup.Fundamental.Traversal;

namespace TagKit.Markup.Fundamental.Nodes
{
    /// <summary>
    /// Represents a document node.
    /// </summary>
    abstract class Document : Node, IDocument
    {
        #region Fields
        private HtmlCollection<IElement> _children;

        private readonly IEventLoop _loop;

        private DomImplementation _implementation;

        #endregion
        #region Implementation of IParentNode

        public IHtmlCollection<IElement> Children
        {
            get { return _children ?? (_children = new HtmlCollection<IElement>(ChildNodes.OfType<Element>())); }
        }
        public IElement FirstElementChild
        {
            get
            {
                var children = ChildNodes;
                var n = children.Length;

                for (var i = 0; i < n; i++)
                {
                    var child = children[i] as IElement;

                    if (child != null)
                    {
                        return child;
                    }
                }

                return null;
            }
        }
        public IElement LastElementChild
        {
            get
            {
                var children = ChildNodes;

                for (var i = children.Length - 1; i >= 0; i--)
                {
                    var child = children[i] as IElement;

                    if (child != null)
                    {
                        return child;
                    }
                }

                return null;
            }
        }
        public Int32 ChildElementCount
        {
            get { return ChildNodes.OfType<Element>().Count(); }
        }
        public void Append(params INode[] nodes)
        {
            this.AppendNodes(nodes);
        }

        public void Prepend(params INode[] nodes)
        {
            this.PrependNodes(nodes);
        }

        public IElement QuerySelector(string selectors)
        {
            return ChildNodes.QuerySelector(selectors, DocumentElement);
        }

        public IHtmlCollection<IElement> QuerySelectorAll(string selectors)
        {
            return ChildNodes.QuerySelectorAll(selectors, DocumentElement);
        }

        #endregion

        #region Implementation of INonElementParentNode

        public IElement GetElementById(string elementId)
        {
            return ChildNodes.GetElementById(elementId);
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            //Important to fix #45
            ReplaceAll(null, true);
            _loop.CancelAll();
            _loadingScripts.Clear();
            _source.Dispose();
        }

        #endregion
        #region Internal Methods
        internal void ReplaceAll(Node node, Boolean suppressObservers)
        {
            var document = Owner;

            if (node != null)
            {
                document.AdoptNode(node);
            }

            var removedNodes = new NodeList();
            var addedNodes = new NodeList();

            removedNodes.AddRange(_children);

            if (node != null)
            {
                if (node.NodeType == NodeType.DocumentFragment)
                {
                    addedNodes.AddRange(node._children);
                }
                else
                {
                    addedNodes.Add(node);
                }
            }

            for (var i = 0; i < removedNodes.Length; i++)
            {
                RemoveChild(removedNodes[i], true);
            }

            for (var i = 0; i < addedNodes.Length; i++)
            {
                InsertBefore(addedNodes[i], null, true);
            }

            if (!suppressObservers)
            {
                document.QueueMutation(MutationRecord.ChildList(
                    target: this,
                    addedNodes: addedNodes,
                    removedNodes: removedNodes));
            }
        }
        #endregion
        #region Implementation of IDocument

        public IImplementation Implementation
        {
            get { return _implementation ?? (_implementation = new DomImplementation(this)); }
        }
        public string Url { get; }
        public string DocumentUri { get; }
        public string Origin { get; }
        public string CompatMode { get; }
        public string CharacterSet { get; }
        public string Charset { get; }
        public string InputEncoding { get; }
        public string ContentType { get; }
        public IDocumentType Doctype { get; }
        public IElement DocumentElement { get; }
        public IHtmlCollection<IElement> GetElementsByTagName(string tagName)
        {
            throw new NotImplementedException();
        }

        public IHtmlCollection<IElement> GetElementsByTagName(string namespaceUri, string tagName)
        {
            throw new NotImplementedException();
        }

        public IHtmlCollection<IElement> GetElementsByClassName(string classNames)
        {
            throw new NotImplementedException();
        }

        public IElement CreateElement(string name)
        {
            throw new NotImplementedException();
        }

        public IElement CreateElement(string namespaceUri, string name)
        {
            throw new NotImplementedException();
        }

        public IDocumentFragment CreateDocumentFragment()
        {
            throw new NotImplementedException();
        }

        public IText CreateTextNode(string data)
        {
            throw new NotImplementedException();
        }

        public IComment CreateComment(string data)
        {
            throw new NotImplementedException();
        }

        public IProcessingInstruction CreateProcessingInstruction(string target, string data)
        {
            throw new NotImplementedException();
        }

        public INode Import(INode externalNode, bool deep = true)
        {
            throw new NotImplementedException();
        }

        public INode Adopt(INode externalNode)
        {
            throw new NotImplementedException();
        }

        public Event CreateEvent(string type)
        {
            throw new NotImplementedException();
        }

        public IRange CreateRange()
        {
            throw new NotImplementedException();
        }

        public INodeIterator CreateNodeIterator(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public ITreeWalker CreateTreeWalker(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
