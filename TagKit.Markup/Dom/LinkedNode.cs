using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    // Gets the node immediately preceding or following this node.
    public abstract class LinkedNode : Node
    {
        internal LinkedNode next;

        internal LinkedNode() : base()
        {
            next = null;
        }
        internal LinkedNode(Document doc) : base(doc)
        {
            next = null;
        }

        // Gets the node immediately preceding this node.
        public override Node PreviousSibling
        {
            get
            {
                Node parent = ParentNode;
                if (parent != null)
                {
                    Node node = parent.FirstChild;
                    while (node != null)
                    {
                        Node nextSibling = node.NextSibling;
                        if (nextSibling == this)
                        {
                            break;
                        }
                        node = nextSibling;
                    }
                    return node;
                }
                return null;
            }
        }

        // Gets the node immediately following this node.
        public override Node NextSibling
        {
            get
            {
                Node parent = ParentNode;
                if (parent != null)
                {
                    if (next != parent.FirstChild)
                        return next;
                }
                return null;
            }
        }
    }
}
