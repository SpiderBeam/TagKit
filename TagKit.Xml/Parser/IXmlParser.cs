
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TagKit.Markup;
using TagKit.Markup.Parser;

namespace TagKit.Xml.Parser
{
    /// <summary>
    /// Represents the interface of an XML parser.
    /// </summary>
    public interface IXmlParser : IParser
    {
        /// <summary>
        /// Parses the string and returns the result.
        /// </summary>
        XmlDocument ParseDocument(String source);

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        XmlDocument ParseDocument(Stream source);

        /// <summary>
        /// Parses the string asynchronously with option to cancel.
        /// </summary>
        Task<XmlDocument> ParseDocumentAsync(String source, CancellationToken cancel);

        /// <summary>
        /// Parses the stream asynchronously with option to cancel.
        /// </summary>
        Task<XmlDocument> ParseDocumentAsync(Stream source, CancellationToken cancel);

        /// <summary>
        /// Populates the given document asynchronously.
        /// </summary>
        Task<Document> ParseDocumentAsync(Document document, CancellationToken cancel);
    }
}
