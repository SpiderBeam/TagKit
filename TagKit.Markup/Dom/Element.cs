using TagKit.Markup.Serialization;

namespace TagKit.Markup
{
    /// <summary>
    /// Represents an DOM element.
    /// </summary>
    public class Element : Container, IMarkupSerializable
    {
        #region ctor



        #endregion
        #region Overrides of Node

        /// <summary>
        /// Gets the node type for this node.
        /// </summary>
        /// <remarks>
        /// This property will always return NodeType.Text.
        /// </remarks>
        public override NodeType NodeType
        {
            get
            {
                return NodeType.Element;
            }
        }
        #endregion
    }
}
