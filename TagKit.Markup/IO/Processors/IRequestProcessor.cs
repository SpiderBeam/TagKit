using System.Threading.Tasks;
using TagKit.Documents.Net;

namespace TagKit.Markup.IO.Processors
{
    /// <summary>
    /// Represents a request processor.
    /// </summary>
    public interface IRequestProcessor
    {
        /// <summary>
        /// Gets the current download, if any.
        /// </summary>
        IDownload Download { get; }

        /// <summary>
        /// Starts processing the given request by cancelling
        /// the current download if any.
        /// </summary>
        /// <param name="request">The new request.</param>
        /// <returns>The task handling the request.</returns>
        Task ProcessAsync(ResourceRequest request);
    }
}
