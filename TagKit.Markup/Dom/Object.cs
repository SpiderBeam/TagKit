using System;
using TagKit.Markup.Events;

namespace TagKit.Markup
{
    /// <summary>
    /// Represents a node or an attribute in an DOM tree.
    /// </summary>
    public abstract class Object : EventTarget, ILineInfo
    {
        #region Implementation of ILineInfo

        public bool HasLineInfo()
        {
            throw new NotImplementedException();
        }

        public int LineNumber { get; }
        public int LinePosition { get; }

        #endregion
    }
}
