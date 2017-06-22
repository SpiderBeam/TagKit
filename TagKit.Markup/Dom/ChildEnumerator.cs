using System;
using System.Collections;

namespace TagKit.Markup
{
    internal sealed class ChildEnumerator : IEnumerator
    {
        internal Node container;
        internal Node child;
        internal bool isFirst;

        internal ChildEnumerator(Node container)
        {
            this.container = container;
            this.child = container.FirstChild;
            this.isFirst = true;
        }

        bool IEnumerator.MoveNext()
        {
            return this.MoveNext();
        }

        internal bool MoveNext()
        {
            if (isFirst)
            {
                child = container.FirstChild;
                isFirst = false;
            }
            else if (child != null)
            {
                child = child.NextSibling;
            }

            return child != null;
        }

        void IEnumerator.Reset()
        {
            isFirst = true;
            child = container.FirstChild;
        }

        object IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        internal Node Current
        {
            get
            {
                if (isFirst || child == null)
                    throw new InvalidOperationException("SR.Xml_InvalidOperation");

                return child;
            }
        }
    }
}
