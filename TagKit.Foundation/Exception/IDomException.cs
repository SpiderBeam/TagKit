using System;
using TagKit.Foundation.Attributes;

namespace TagKit.Foundation.Exception
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
