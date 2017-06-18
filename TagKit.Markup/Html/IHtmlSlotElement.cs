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
    /// Represents a slot in the shadow tree.
    /// </summary>
    [DomName("HTMLSlotElement")]
    public interface IHtmlSlotElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the name attribute.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets the nodes from the distributed nodes of the context.
        /// </summary>
        /// <returns>The sequence of distributed nodes.</returns>
        [DomName("getDistributedNodes")]
        IEnumerable<INode> GetDistributedNodes();
    }
}
