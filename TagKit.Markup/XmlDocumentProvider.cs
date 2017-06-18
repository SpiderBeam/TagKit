using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TagKit.Configuration.Foundation;
using TagKit.Documents;
using TagKit.Documents.Nodes;
using TagKit.Documents.Nodes.Xml;
using TagKit.Markup.Nodes.Browser;
using TagKit.Markup.Nodes.Xml;
using TagKit.Xml.Parser;

namespace TagKit.Markup
{
    public class XmlDocumentProvider: IXmlDocumentProvider
    {
        #region Implementation of IXmlDocumentProvider

        public IXmlDocument CreateXmlDocument(IBrowsingContext context)
        {
            return new XmlDocument(context);
        }

        #region Implementation of IXmlDocumentProvider

        public Task<IDocument> LoadXmlAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var parser = context.GetService<IXmlParser>();
            var document = new XmlDocument(context, options.Source);
            document.Setup(options.Response, options.ContentType, options.ImportAncestor);
            context.NavigateTo(document);
            return parser.ParseDocumentAsync(document, cancellationToken);
        }

        #endregion

        #endregion
    }
}
