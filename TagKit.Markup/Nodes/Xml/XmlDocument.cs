using System;
using System.Threading;
using System.Threading.Tasks;
using TagKit.Configuration;
using TagKit.Configuration.Foundation;
using TagKit.Documents;
using TagKit.Documents.Nodes;
using TagKit.Documents.Nodes.Xml;
using TagKit.Foundation;
using TagKit.Foundation.Text;
using TagKit.Markup.Nodes.Browser;
using TagKit.Services;
using TagKit.Xml.Parser;

namespace TagKit.Markup.Nodes.Xml
{
    /// <summary>
    /// Represents a document node that contains only XML nodes.
    /// </summary>
    sealed class XmlDocument : Document, IXmlDocument
    {
        #region ctor

        internal XmlDocument(IBrowsingContext context, TextSource source)
            : base(context ?? BrowsingContext.New(), source)
        {
            ContentType = MimeTypeNames.Xml;
        }

        internal XmlDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        #endregion

        #region Properties

        public override IElement DocumentElement
        {
            get { return this.FindChild<IElement>(); }
        }

        public override IEntityProvider Entities
        {
            get { return Context.GetProvider<IEntityProvider>() ?? XmlEntityProvider.Resolver; }
        }

        #endregion

        #region Methods

        internal override Element CreateElementFrom(String name, String prefix)
        {
            return new XmlElement(this, name, prefix);
        }
        public Task<IDocument> LoadXmlAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var parser = context.GetService<IXmlParser>();
            var document = new XmlDocument(context, options.Source);
            document.Setup(options.Response, options.ContentType, options.ImportAncestor);
            context.NavigateTo(document);
            return parser.ParseDocumentAsync(document, cancellationToken);
        }
        #endregion

        #region Helpers

        internal override Node Clone(Document owner, Boolean deep)
        {
            var node = new XmlDocument(Context, new TextSource(Source.Text));
            CloneDocument(node, deep);
            return node;
        }

        protected override void SetTitle(String value)
        {
        }

        #endregion
    }
}
