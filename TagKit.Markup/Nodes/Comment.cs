using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup.Nodes
{
    /// <summary>
    /// Represents a node that contains a comment.
    /// </summary>
    sealed class Comment : CharacterData, IComment
    {
        #region ctor

        internal Comment(Document owner)
            : this(owner, String.Empty)
        {
        }

        internal Comment(Document owner, String data)
            : base(owner, "#comment", NodeType.Comment, data)
        {
        }

        #endregion

        #region Methods

        public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
        {
            writer.Write(formatter.Comment(this));
        }

        #endregion

        #region Helpers

        internal override Node Clone(Document owner, Boolean deep)
        {
            var node = new Comment(owner, Data);
            CloneNode(node, owner, deep);
            return node;
        }

        #endregion
    }
}
