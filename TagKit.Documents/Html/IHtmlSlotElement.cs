using System;
using System.Collections.Generic;
using TagKit.Documents.Nodes;
using TagKit.Documents.Nodes.Html;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Html
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
