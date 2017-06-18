using System.Threading;
using System.Threading.Tasks;
using TagKit.Configuration.Foundation;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Nodes.Xml
{

    /// <summary>
    /// The interface represent an XML document.
    /// </summary>
    [DomName("XMLDocument")]
    public interface IXmlDocument : IDocument
    {
        Task<IDocument> LoadXmlAsync(IBrowsingContext context, CreateDocumentOptions options,
            CancellationToken cancellationToken);
    }
}
