using System.Threading;
using System.Threading.Tasks;
using TagKit.Configuration.Foundation;
using TagKit.Documents.Nodes;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Svg
{
    /// <summary>
    /// Serves as an entry point to the content of an SVG document.
    /// </summary>
    [DomName("SVGDocument")]
    public interface ISvgDocument : IDocument
    {
        /// <summary>
        /// Gets the root svg element in the document hierachy.
        /// </summary>
        [DomName("rootElement")]
        ISvgSvgElement RootElement { get; }

        Task<IDocument> LoadSvgAsync(IBrowsingContext context, CreateDocumentOptions options,
            CancellationToken cancellationToken);
    }
}
