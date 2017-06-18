using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.Attributes;
using TagKit.Markup.Nodes;

namespace TagKit.Markup.Html
{
    /// <summary>
    /// Represents the datalist HTML element.
    /// </summary>
    [DomName("HTMLDataListElement")]
    public interface IHtmlDataListElement : IHtmlElement
    {
        /// <summary>
        /// Gets the associated options.
        /// </summary>
        [DomName("options")]
        IHtmlCollection<IHtmlOptionElement> Options { get; }
    }
}
