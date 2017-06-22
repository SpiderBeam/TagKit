
using System;
using System.Diagnostics;

namespace TagKit.Markup
{
    /// <summary>
    /// Represents nodes (elements, comments, document type, processing instruction,
    /// and text nodes) in the DOM tree.
    /// </summary>
    public abstract class Node : Object
    {
        #region Fields

        private Node _parentNode;//this pointer is reused to save the userdata information, need to prevent internal user access the pointer directly.

        #endregion
        internal Node()
        {
        }

        internal Node(Document doc)
        {
            if (doc == null)
                throw new ArgumentException("Xdom_Node_Null_Doc");
            this._parentNode = doc;
        }
        public abstract NodeType NodeType
        {
            get;
        }
        /// <summary>
        /// Gets the parent of this node (for nodes that can have parents).
        /// </summary>
        public virtual Node ParentNode
        {
            get
            {
                Debug.Assert(_parentNode != null);

                if (_parentNode.NodeType != NodeType.Document)
                {
                    return _parentNode;
                }

                // Linear lookup through the children of the document
                LinkedNode firstChild = _parentNode.FirstChild as LinkedNode;
                if (firstChild != null)
                {
                    LinkedNode node = firstChild;
                    do
                    {
                        if (node == this)
                        {
                            return _parentNode;
                        }
                        node = node.next;
                    }
                    while (node != null
                           && node != firstChild);
                }
                return null;
            }
        }
        public virtual Node FirstChild
        {
            get
            {
                LinkedNode linkedNode = LastNode;
                if (linkedNode != null)
                    return linkedNode.next;

                return null;
            }
        }
        /// <summary>
        /// Gets the last child of this node.
        /// </summary>
        public virtual Node LastChild
        {
            get { return LastNode; }
        }
        internal virtual LinkedNode LastNode
        {
            get { return null; }
            set { }
        }
        /// <summary>
        /// Gets the node immediately preceding this node.
        /// </summary>
        public virtual Node PreviousSibling
        {
            get { return null; }
        }
        /// <summary>
        /// Gets the node immediately following this node.
        /// </summary>
        public virtual Node NextSibling
        {
            get { return null; }
        }
    }
}
