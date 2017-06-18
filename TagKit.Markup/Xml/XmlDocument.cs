using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.IO;
using TagKit.Foundation.Text;
using TagKit.Markup.Nodes;

namespace TagKit.Markup.Xml
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
