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
    /// Represents the template HTML element.
    /// </summary>
    [DomName("HTMLTemplateElement")]
    public interface IHtmlTemplateElement : IHtmlElement
    {
        /// <summary>
        /// Gets the template's content for cloning.
        /// </summary>
        [DomName("content")]
        IDocumentFragment Content { get; }
    }
}
