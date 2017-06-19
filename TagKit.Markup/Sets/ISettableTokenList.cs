using System;
using TagKit.Markup.Attributes;

namespace TagKit.Markup.Sets
{
    /// <summary>
    /// The same as TokenList, except that it allows the underlying string to
    /// be directly changed.
    /// </summary>
    [DomName("DOMSettableTokenList")]
    public interface ISettableTokenList : ITokenList
    {
        /// <summary>
        /// Gets or sets the underlying string.
        /// </summary>
        [DomName("value")]
        String Value { get; set; }
    }
}
