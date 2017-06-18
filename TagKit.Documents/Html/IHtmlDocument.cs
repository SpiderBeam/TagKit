﻿using System.Threading;
using System.Threading.Tasks;
using TagKit.Configuration.Foundation;
using TagKit.Documents.Nodes;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Html
{
    /// <summary>
    /// Serves as an entry point to the content of an HTML document.
    /// </summary>
    [DomName("HTMLDocument")]
    public interface IHtmlDocument : IDocument
    {
        Task<IDocument> LoadHtmlAsync(IBrowsingContext context, CreateDocumentOptions options,
            CancellationToken cancellationToken);

        Task<IDocument> LoadTextAsync(IBrowsingContext context, CreateDocumentOptions options,
            CancellationToken cancellationToken);
    }
}
