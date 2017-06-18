using System;
using System.Threading;
using System.Threading.Tasks;
using TagKit.Configuration;
using TagKit.Configuration.Foundation;
using TagKit.Configuration.Services;
using TagKit.Documents;
using TagKit.Documents.Html;
using TagKit.Documents.Nodes;
using TagKit.Documents.Nodes.Html;
using TagKit.Documents.Nodes.Html.Parser;
using TagKit.Foundation;
using TagKit.Foundation.Text;
using TagKit.Markup.Nodes.Browser;
using TagKit.Markup.Nodes.Mathml;
using TagKit.Markup.Nodes.Svg;

namespace TagKit.Markup.Nodes.Html
{
    /// <summary>
    /// Represents a document node that contains only HTML nodes.
    /// </summary>
    sealed class HtmlDocument : Document, IHtmlDocument
    {
        #region Fields

        private readonly IElementFactory<Document, HtmlElement> _htmlFactory;
        private readonly IElementFactory<Document, MathElement> _mathFactory;
        private readonly IElementFactory<Document, SvgElement> _svgFactory;

        #endregion

        #region ctor

        internal HtmlDocument(IBrowsingContext context, TextSource source)
            : base(context ?? BrowsingContext.New(), source)
        {
            ContentType = MimeTypeNames.Html;
            _htmlFactory = Context.GetFactory<IElementFactory<Document, HtmlElement>>();
            _mathFactory = Context.GetFactory<IElementFactory<Document, MathElement>>();
            _svgFactory = Context.GetFactory<IElementFactory<Document, SvgElement>>();
        }

        internal HtmlDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        #endregion

        #region Properties

        public override IElement DocumentElement
        {
            get { return this.FindChild<HtmlHtmlElement>(); }
        }

        public override IEntityProvider Entities
        {
            get { return Context.GetProvider<IEntityProvider>() ?? HtmlEntityProvider.Resolver; }
        }

        #endregion

        #region Methods

        public HtmlElement CreateHtmlElement(String name, String prefix = null)
        {
            return _htmlFactory.Create(this, name, prefix);
        }

        public MathElement CreateMathElement(String name, String prefix = null)
        {
            return _mathFactory.Create(this, name, prefix);
        }

        public SvgElement CreateSvgElement(String name, String prefix = null)
        {
            return _svgFactory.Create(this, name, prefix);
        }

        internal override Element CreateElementFrom(String name, String prefix)
        {
            return CreateHtmlElement(name, prefix);
        }
        public Task<IDocument> LoadHtmlAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var parser = context.GetService<IHtmlParser>();
            var document = new HtmlDocument(context, options.Source);
            document.Setup(options.Response, options.ContentType, options.ImportAncestor);
            context.NavigateTo(document);
            return parser.ParseDocumentAsync(document, cancellationToken);
        }
        public  async Task<IDocument> LoadTextAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var document = new HtmlDocument(context, options.Source);
            document.Setup(options.Response, options.ContentType, options.ImportAncestor);
            context.NavigateTo(document);
            var root = document.CreateElement(TagNames.Html);
            var head = document.CreateElement(TagNames.Head);
            var body = document.CreateElement(TagNames.Body);
            var pre = document.CreateElement(TagNames.Pre);
            document.AppendChild(root);
            root.AppendChild(head);
            root.AppendChild(body);
            body.AppendChild(pre);
            pre.SetAttribute(AttributeNames.Style, "word-wrap: break-word; white-space: pre-wrap;");
            await options.Source.PrefetchAllAsync(cancellationToken).ConfigureAwait(false);
            pre.TextContent = options.Source.Text;
            return document;
        }
        #endregion

        #region Helpers

        internal override Node Clone(Document owner, Boolean deep)
        {
            var source = new TextSource(Source.Text);
            var node = new HtmlDocument(Context, source);
            CloneDocument(node, deep);
            return node;
        }

        protected override String GetTitle()
        {
            var title = DocumentElement.FindDescendant<IHtmlTitleElement>();
            return title?.TextContent.CollapseAndStrip() ?? base.GetTitle();
        }

        protected override void SetTitle(String value)
        {
            var title = DocumentElement.FindDescendant<IHtmlTitleElement>();

            if (title == null)
            {
                var head = Head;

                if (head == null)
                {
                    return;
                }

                title = new HtmlTitleElement(this);
                head.AppendChild(title);
            }

            title.TextContent = value;
        }

        #endregion
    }
}
