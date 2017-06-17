using System.Collections.Generic;
using TagKit.Foundation.Text;

namespace TagKit.Documents.Nodes.Css
{
    /// <summary>
    /// Represents a node in the CSSOM.
    /// </summary>
    public interface ICssNode : IStyleFormattable
    {
        /// <summary>
        /// Gets the children of this node.
        /// </summary>
        IEnumerable<ICssNode> Children { get; }

        /// <summary>
        /// Gets the original source code, if any.
        /// </summary>
        TextView SourceCode { get; }
    }
}