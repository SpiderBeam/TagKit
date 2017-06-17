﻿using System;
using TagKit.Documents.Html;
using TagKit.Documents.Nodes.Html;

namespace TagKit.Markup.Nodes.Html
{
    /// <summary>
    /// Represents the title element.
    /// </summary>
    sealed class HtmlTitleElement : HtmlElement, IHtmlTitleElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML title element.
        /// </summary>
        public HtmlTitleElement(Document owner, String prefix = null)
            : base(owner, TagNames.Title, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the title.
        /// </summary>
        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        #endregion
    }
}
