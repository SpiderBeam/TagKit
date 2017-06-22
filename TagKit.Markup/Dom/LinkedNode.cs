namespace TagKit.Markup
{
    public abstract class LinkedNode : Node
    {
        internal LinkedNode next;

        internal LinkedNode(Document doc) : base(doc)
        {
            next = null;
        }

        /// <summary>
        /// Gets the node immediately preceding this node.
        /// </summary>
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

        /// <summary>
        /// Gets the node immediately following this node.
        /// </summary>
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
