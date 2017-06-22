using System;

namespace TagKit.Markup
{
    /// <summary>
    /// Defines how a DOMException should look like.
    /// </summary>
    public interface IDomException
    {
        /// <summary>
        /// Gets the error code for this exception.
        /// </summary>
        Int32 Code { get; }
    }
}