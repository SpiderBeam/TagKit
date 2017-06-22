namespace TagKit.Markup
{
    public class Element : LinkedNode
    {

        #region Overrides of Node

        // Gets the type of the current node.
        public override NodeType NodeType
        {
            get { return NodeType.Element; }
        }
        #endregion
    }
}
