namespace TagKit.Markup
{
    /// <summary>
    /// Represents an element.
    /// </summary>
    public class Element : LinkedNode
    {
        private TagName _name;


        #region Overrides of Node

        // Gets the type of the current node.
        public override NodeType NodeType
        {
            get { return NodeType.Element; }
        }

        #region Overrides of Node
        /// <summary>
        /// Gets the name of the node.
        /// </summary>
        public override string Name
        {
            get { return _name.Name; }
        }
        #endregion

        #endregion
    }
}
