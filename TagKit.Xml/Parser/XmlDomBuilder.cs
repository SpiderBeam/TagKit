using System;
using System.Collections.Generic;
using TagKit.Markup;

namespace TagKit.Xml.Parser
{
    /// <summary>
    /// Represents the Tree construction as specified in the official W3C
    /// specification for XML:
    /// http://www.w3.org/TR/REC-xml/
    /// </summary>
    sealed class XmlDomBuilder
    {
        #region Fields

        private readonly XmlTokenizer _tokenizer;
        private readonly Document _document;
        private readonly List<Element> _openElements;

        private XmlParserOptions _options;
        private XmlTreeMode _currentMode;
        private Boolean _standalone;

        #endregion
        #region ctor

        /// <summary>
        /// Creates a new instance of the XML parser.
        /// </summary>
        /// <param name="document">The document instance to be filled.</param>
        internal XmlDomBuilder(Document document)
        {
            _tokenizer = new XmlTokenizer(document.Source, document.Entities);
            _document = document;
            _standalone = false;
            _openElements = new List<Element>();
            _currentMode = XmlTreeMode.Initial;
        }

        #endregion
    }
}
