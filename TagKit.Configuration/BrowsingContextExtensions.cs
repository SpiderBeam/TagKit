using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TagKit.Configuration.Foundation;
using TagKit.Configuration.Services;
using TagKit.Documents;
using TagKit.Documents.Net;
using TagKit.Documents.Nodes;

namespace TagKit.Configuration
{

    /// <summary>
    /// A set of extensions for the browsing context.
    /// </summary>
    public static class BrowsingContextExtensions
    {
        /// <summary>
        /// Opens a new document without any content in the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="url">The optional base URL of the document.</param>
        /// <returns>The new, yet empty, document.</returns>
        public static Task<IDocument> OpenNewAsync(this IBrowsingContext context, String url = null)
        {
            return context.OpenAsync(m => m.Address(url));
        }
        /// <summary>
        /// Opens a new document loaded from a virtual response that can be 
        /// filled via the provided callback without any ability to cancel it.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">Callback with the response to setup.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, Action<VirtualResponse> request)
        {
            return context.OpenAsync(request, CancellationToken.None);
        }
        /// <summary>
        /// Opens a new document loaded from a virtual response that can be 
        /// filled via the provided callback.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="request">Callback with the response to setup.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static async Task<IDocument> OpenAsync(this IBrowsingContext context, Action<VirtualResponse> request, CancellationToken cancel)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using (var response = VirtualResponse.Create(request))
            {
                return await context.OpenAsync(response, cancel).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// Opens a new document created from the response asynchronously in
        /// the given context.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="response">The response to examine.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task that creates the document.</returns>
        public static Task<IDocument> OpenAsync(this IBrowsingContext context, IResponse response, CancellationToken cancel)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            if (context == null)
            {
                context = BrowsingContext.New();
            }

            var options = new CreateDocumentOptions(response, context.Configuration);
            var factory = context.Configuration.GetFactory<IDocumentFactory>();
            return factory.CreateAsync(context, options, cancel);
        }
    }
}
