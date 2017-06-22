using System;
using System.Collections;
using TagKit.Markup.Properties;

namespace TagKit.Markup
{
    internal sealed class EmptyEnumerator : IEnumerator
    {
        bool IEnumerator.MoveNext()
        {
            return false;
        }

        void IEnumerator.Reset()
        {
        }

        object IEnumerator.Current
        {
            get
            {
                throw new InvalidOperationException(Resources.Xml_InvalidOperation);
            }
        }
    }
}