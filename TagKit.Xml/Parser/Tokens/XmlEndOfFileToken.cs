using TagKit.Markup.Parser;

namespace TagKit.Xml.Parser.Tokens
{
    /// <summary>
    /// Represents the final token to mark the EOF.
    /// </summary>
    sealed class XmlEndOfFileToken : XmlToken
    {
        /// <summary>
        /// Creates a new EOF token.
        /// </summary>
        public XmlEndOfFileToken(TextPosition position)
            : base(XmlTokenType.EndOfFile, position)
        {
        }
    }
}
