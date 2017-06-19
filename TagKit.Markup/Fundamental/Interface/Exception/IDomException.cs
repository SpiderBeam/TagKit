using System;
using TagKit.Markup.Attributes;

namespace TagKit.Markup.Fundamental.Exception
{

    /// <summary>
    /// Defines how a DOMException should look like.
    /// </summary>
    [DomName("DOMException")]
    public interface IDomException
    {
        /// <summary>
        /// Gets the error code for this exception.
        /// </summary>
        [DomName("code")]
        Int32 Code { get; }
    }
}
