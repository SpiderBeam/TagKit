using System;
using System.Collections;
using System.Collections.Generic;
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

        /// <summary>
        /// Gets the name of the node.
        /// </summary>
        public abstract string Name
        {
            get;
        }

        public virtual void RemoveAll()
        {
            Node child = FirstChild;
            Node sibling = null;

            while (child != null)
            {
                sibling = child.NextSibling;
                RemoveChild(child);
                child = sibling;
            }
        }
        public virtual Node RemoveChild(Node oldChild)
        {
            //if (!IsContainer)
            //    throw new InvalidOperationException(Res.GetString(Resources.Xdom_Node_Remove_Contain));

            //if (oldChild.ParentNode != this)
            //    throw new ArgumentException(Res.GetString(Resources.Xdom_Node_Remove_Child));

            //LinkedNode oldNode = (LinkedNode)oldChild;

            //string oldNodeValue = oldNode.Value;
            //XmlNodeChangedEventArgs args = GetEventArgs(oldNode, this, null, oldNodeValue, oldNodeValue, XmlNodeChangedAction.Remove);

            //if (args != null)
            //    BeforeEvent(args);

            //LinkedNode lastNode = LastNode;

            //if (oldNode == FirstChild)
            //{
            //    if (oldNode == lastNode)
            //    {
            //        LastNode = null;
            //        oldNode.next = null;
            //        oldNode.SetParent(null);
            //    }
            //    else
            //    {
            //        LinkedNode nextNode = oldNode.next;

            //        if (nextNode.IsText)
            //        {
            //            if (oldNode.IsText)
            //            {
            //                UnnestTextNodes(oldNode, nextNode);
            //            }
            //        }

            //        lastNode.next = nextNode;
            //        oldNode.next = null;
            //        oldNode.SetParent(null);
            //    }
            //}
            //else
            //{
            //    if (oldNode == lastNode)
            //    {
            //        LinkedNode prevNode = (LinkedNode)oldNode.PreviousSibling;
            //        prevNode.next = oldNode.next;
            //        LastNode = prevNode;
            //        oldNode.next = null;
            //        oldNode.SetParent(null);
            //    }
            //    else
            //    {
            //        LinkedNode prevNode = (LinkedNode)oldNode.PreviousSibling;
            //        LinkedNode nextNode = oldNode.next;

            //        if (nextNode.IsText)
            //        {
            //            if (prevNode.IsText)
            //            {
            //                NestTextNodes(prevNode, nextNode);
            //            }
            //            else
            //            {
            //                if (oldNode.IsText)
            //                {
            //                    UnnestTextNodes(oldNode, nextNode);
            //                }
            //            }
            //        }

            //        prevNode.next = nextNode;
            //        oldNode.next = null;
            //        oldNode.SetParent(null);
            //    }
            //}

            //if (args != null)
            //    AfterEvent(args);

            //return oldChild;
            return null;
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
