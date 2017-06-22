﻿using System;
using System.Runtime.Remoting.Contexts;
using TagKit.Foundation.IO;
using TagKit.Foundation.Text;
using TagKit.Markup.Fundamental;
using TagKit.Markup.Fundamental.Nodes;

namespace TagKit.Markup.Html
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
