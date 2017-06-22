using TagKit.Markup.Parser;

namespace TagKit.Markup
{
    /// <summary>
    /// Represents an DOM document.
    /// </summary>
    public class Document : Container
    {
        #region Fields

        private readonly TextSource _source;

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>

        public Document(TextSource source)
        {
            _source = source;
        }
        #endregion
        #region Properties

        public TextSource Source
        {
            get { return _source; }
        }

        public  IEntityProvider Entities
        {
            get;
        }

        #region Overrides of Node

        /// <summary>
        /// Gets the node type for this node.
        /// </summary>
        /// <remarks>
        /// This property will always return NodeType.Document.
        /// </remarks>
        public override NodeType NodeType
        {
            get
            {
                return NodeType.Document;
            }
        }

        #endregion

        #endregion

    }
}
