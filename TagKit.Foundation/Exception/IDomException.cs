﻿using System;
using TagKit.Foundation.Attributes;

namespace TagKit.Fundamental.Exception
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
