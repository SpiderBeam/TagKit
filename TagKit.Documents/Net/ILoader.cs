using System.Collections.Generic;

namespace TagKit.Documents.Net
{
    /// <summary>
    /// Represents the basic interface for all loaders.
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Gets the currently active downloads.
        /// </summary>
        /// <returns>The downloads in progress.</returns>
        IEnumerable<IDownload> GetDownloads();
    }
}