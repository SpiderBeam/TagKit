using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.Attributes;
using TagKit.Markup.Html.IO;

namespace TagKit.Markup.Html
{
    /// <summary>
    /// The embed HTML element.
    /// </summary>
    [DomName("HTMLEmbedElement")]
    public interface IHtmlEmbedElement : IHtmlElement, ILoadableElement
    {
        /// <summary>
        /// Gets or sets the source of the object to embed.
        /// </summary>
        [DomName("src")]
        String Source { get; set; }

        /// <summary>
        /// Gets or sets the type of the embedded object.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }

        /// <summary>
        /// Gets or sets the display width of the object.
        /// </summary>
        [DomName("width")]
        String DisplayWidth { get; set; }

        /// <summary>
        /// Gets or sets the display height of the object.
        /// </summary>
        [DomName("height")]
        String DisplayHeight { get; set; }
    }
}
