using System;
using TagKit.Documents.Nodes.Html;
using TagKit.Foundation;
using TagKit.Foundation.Attributes;
using TagKit.Foundation.Text;

namespace TagKit.Markup.Nodes.Html
{
    /// <summary>
    /// Represents the HTML frameset element.
    /// Obsolete since HTML 4.01.
    /// </summary>
    [DomHistorical]
    sealed class HtmlFrameSetElement : HtmlElement
    {
        #region ctor

        public HtmlFrameSetElement(Document owner, String prefix = null)
            : base(owner, TagNames.Frameset, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        public Int32 Columns
        {
            get { return this.GetOwnAttribute(AttributeNames.Cols).ToInteger(1); }
            set { this.SetOwnAttribute(AttributeNames.Cols, value.ToString()); }
        }

        public Int32 Rows
        {
            get { return this.GetOwnAttribute(AttributeNames.Rows).ToInteger(1); }
            set { this.SetOwnAttribute(AttributeNames.Rows, value.ToString()); }
        }

        #endregion
    }
}
