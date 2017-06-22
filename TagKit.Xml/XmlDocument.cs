using System;
using TagKit.Markup;
using TagKit.Markup.Parser;

namespace TagKit.Xml
{
    /// <summary>
    /// Represents a document node that contains only XML nodes.
    /// </summary>
    public sealed class XmlDocument : Document
    {
        private TextSource _source;

        public XmlDocument(TextSource source):base(source)
        {
            _source = source;
        }
    }
}
