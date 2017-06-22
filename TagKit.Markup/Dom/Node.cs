using System;
using System.Collections;
using System.Diagnostics;
using TagKit.Markup.Properties;

namespace TagKit.Markup
{
    /// <summary>
    /// Represents a single node in the document.
    /// </summary>
    [DebuggerDisplay("{debuggerDisplayProxy}")]
    public abstract class Node : IEnumerable
    {
        internal Node parentNode; //this pointer is reused to save the userdata information, need to prevent internal user access the pointer directly.

        internal Node()
        {
        }

        internal Node(Document doc)
        {
            if (doc == null)
                throw new ArgumentException(Resources.Xdom_Node_Null_Doc);
            this.parentNode = doc;
        }
        /// <summary>
        /// Gets the type of the current node.
        /// </summary>
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
                Debug.Assert(parentNode != null);

                if (parentNode.NodeType != NodeType.Document)
                {
                    return parentNode;
                }

                // Linear lookup through the children of the document
                LinkedNode firstChild = parentNode.FirstChild as LinkedNode;
                if (firstChild != null)
                {
                    LinkedNode node = firstChild;
                    do
                    {
                        if (node == this)
                        {
                            return parentNode;
                        }
                        node = node.next;
                    }
                    while (node != null
                           && node != firstChild);
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the first child of this node.
        /// </summary>
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
        internal virtual LinkedNode LastNode
        {
            get { return null; }
            set { }
        }
        /// <summary>
        /// Gets the node immediately following this node.
        /// </summary>
        public virtual Node NextSibling
        {
            get { return null; }
        }
        /// <summary>
        /// Gets all children of this node.
        /// </summary>
        public virtual NodeList ChildNodes
        {
            get { return new ChildNodes(this); }
        }
        /// <summary>
        /// Gets the node immediately preceding this node.
        /// </summary>
        public virtual Node PreviousSibling
        {
            get { return null; }
        }
        #region Implementation of IEnumerable

        /// <summary>
        // Provides a simple ForEach-style iteration over the
        // collection of nodes in this NamedNodeMap.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ChildEnumerator(this);
        }

        public IEnumerator GetEnumerator()
        {
            return new ChildEnumerator(this);
        }
        #endregion

    }
}
