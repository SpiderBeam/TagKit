using System.Collections;

namespace TagKit.Markup
{
    internal class ChildNodes : NodeList
    {
        private Node _container;

        public ChildNodes(Node container)
        {
            _container = container;
        }

        public override Node Item(int i)
        {
            // Out of range indexes return a null XmlNode
            if (i < 0)
                return null;
            for (Node n = _container.FirstChild; n != null; n = n.NextSibling, i--)
            {
                if (i == 0)
                    return n;
            }
            return null;
        }

        public override int Count
        {
            get
            {
                int c = 0;
                for (Node n = _container.FirstChild; n != null; n = n.NextSibling)
                {
                    c++;
                }
                return c;
            }
        }

        public override IEnumerator GetEnumerator()
        {
            if (_container.FirstChild == null)
            {
                return Document.EmptyEnumerator;
            }
            else
            {
                return new ChildEnumerator(_container);
            }
        }
    }
}
