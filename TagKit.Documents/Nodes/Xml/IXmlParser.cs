using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TagKit.Documents.Nodes;
using TagKit.Documents.Nodes.Browser;
using TagKit.Documents.Nodes.Xml;

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
        IXmlDocument ParseDocument(String source);

        /// <summary>
        /// Parses the stream and returns the result.
        /// </summary>
        IXmlDocument ParseDocument(Stream source);

        /// <summary>
        /// Parses the string asynchronously with option to cancel.
        /// </summary>
        Task<IXmlDocument> ParseDocumentAsync(String source, CancellationToken cancel);

        /// <summary>
        /// Parses the stream asynchronously with option to cancel.
        /// </summary>
        Task<IXmlDocument> ParseDocumentAsync(Stream source, CancellationToken cancel);

        /// <summary>
        /// Populates the given document asynchronously.
        /// </summary>
        Task<IDocument> ParseDocumentAsync(IDocument document, CancellationToken cancel);
    }
}
