using System;
using TagKit.Documents.Nodes.Html;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Html
{
    /// <summary>
    /// Represents the html HTML element.
    /// </summary>
    [DomName("HTMLHtmlElement")]
    public interface IHtmlHtmlElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the value of the manifest attribute.
        /// </summary>
        [DomName("manifest")]
        String Manifest { get; set; }
    }
}
