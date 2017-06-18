using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation;
using TagKit.Markup.Nodes;

namespace TagKit.Markup.Html
{
    /// <summary>
    /// Represents the HTML html element.
    /// </summary>
    sealed class HtmlHtmlElement : HtmlElement, IHtmlHtmlElement
    {
        #region ctor

        public HtmlHtmlElement(Document owner, String prefix = null)
            : base(owner, TagNames.Html, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
        {
        }

        #endregion

        #region Properties

        public String Manifest
        {
            get { return this.GetOwnAttribute(AttributeNames.Manifest); }
            set { this.SetOwnAttribute(AttributeNames.Manifest, value); }
        }

        #endregion
    }
}
