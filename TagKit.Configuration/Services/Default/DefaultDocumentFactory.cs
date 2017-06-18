﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TagKit.Configuration.Foundation;
using TagKit.Documents;
using TagKit.Documents.Html;
using TagKit.Documents.Nodes;
using TagKit.Documents.Nodes.Xml;
using TagKit.Documents.Svg;
using TagKit.Foundation;
using TagKit.Markup.Nodes.Browser;

namespace TagKit.Configuration.Services.Default
{

    /// <summary>
    /// Provides the default content-type to document creation mapping.
    /// </summary>
    public class DefaultDocumentFactory : IDocumentFactory
    {
        private readonly Dictionary<String, Creator> _creators = new Dictionary<String, Creator>
        {
            { MimeTypeNames.Xml, LoadXmlAsync },
            { MimeTypeNames.ApplicationXml, LoadXmlAsync },
            { MimeTypeNames.Svg, LoadSvgAsync },
            { MimeTypeNames.Html, LoadHtmlAsync },
            { MimeTypeNames.ApplicationXHtml, LoadHtmlAsync },
            { MimeTypeNames.Plain, LoadTextAsync },
            { MimeTypeNames.ApplicationJson, LoadTextAsync },
            { MimeTypeNames.DefaultJavaScript, LoadTextAsync },
            { MimeTypeNames.Css, LoadTextAsync }
        };

        /// <summary>
        /// Represents a creator delegate for creating documents.
        /// </summary>
        /// <param name="context">The context of the new document.</param>
        /// <param name="options">The creation options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The task of creating the document.</returns>
        public delegate Task<IDocument> Creator(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken);

        /// <summary>
        /// Registers a new creator for the specified content-type.
        /// Throws an exception if another creator for the given
        /// content-type is already added.
        /// </summary>
        /// <param name="contentType">The content-type value.</param>
        /// <param name="creator">The creator to invoke.</param>
        public void Register(String contentType, Creator creator)
        {
            _creators.Add(contentType, creator);
        }

        /// <summary>
        /// Unregisters an existing creator for the given content-type.
        /// </summary>
        /// <param name="contentType">The content-type value.</param>
        /// <returns>The registered creator, if any.</returns>
        public Creator Unregister(String contentType)
        {
            var creator = default(Creator);

            if (_creators.TryGetValue(contentType, out creator))
            {
                _creators.Remove(contentType);
            }

            return creator;
        }

        /// <summary>
        /// Creates the default document for the given options.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="options">The options to consider.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The task creating the document from the response.</returns>
        protected virtual Task<IDocument> CreateDefaultAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            return LoadHtmlAsync(context, options, cancellationToken);
        }

        /// <summary>
        /// Creates a new document from the given arguments using the Content-Type
        /// of the provided options.
        /// </summary>
        /// <param name="context">The browsing context to use.</param>
        /// <param name="options">The options to consider.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The task creating the document from the response.</returns>
        public Task<IDocument> CreateAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var contentType = options.ContentType;

            foreach (var creator in _creators)
            {
                if (contentType.Represents(creator.Key))
                {
                    return creator.Value.Invoke(context, options, cancellationToken);
                }
            }

            return CreateDefaultAsync(context, options, cancellationToken);
        }

        private static Task<IDocument> LoadHtmlAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var document = context.GetProvider<IHtmlDocument>();
            var result=document.LoadHtmlAsync(context, options, cancellationToken);
            return result;
        }

        private static async Task<IDocument> LoadTextAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var document = context.GetProvider<IHtmlDocument>();
            var result = await document.LoadTextAsync(context, options, cancellationToken);
            return result;
        }

        private static Task<IDocument> LoadXmlAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var document = context.GetProvider<IXmlDocument>();
            var result =  document.LoadXmlAsync(context, options, cancellationToken);
            return result;
        }

        private static Task<IDocument> LoadSvgAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var document = context.GetProvider<ISvgDocument>();
            var result = document.LoadSvgAsync(context, options, cancellationToken);
            return result;
        }
    }
}