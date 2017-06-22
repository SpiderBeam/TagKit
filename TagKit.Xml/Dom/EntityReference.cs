using TagKit.Markup;

namespace TagKit.Xml
{
    public class EntityReference : LinkedNode
    {
        public EntityReference(Document doc) : base(doc)
        {
        }

        #region Overrides of Node

        public override NodeType NodeType { get; }

        #endregion
    }
}
