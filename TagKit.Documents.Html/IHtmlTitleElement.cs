using System;
using TagKit.Documents.Nodes.Html;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Html
{
    /// <summary>
    /// Represents the title HTML element.
    /// </summary>
    [DomName("HTMLTitleElement")]
    public interface IHtmlTitleElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the text of the title.
        /// </summary>
        [DomName("text")]
        String Text { get; set; }
    }
}
