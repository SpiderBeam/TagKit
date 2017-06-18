using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.Attributes;

namespace TagKit.Markup.Html
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
