﻿using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Nodes
{
    /// <summary>
    /// Implemented by elements that may expose stylesheets.
    /// </summary>
    [DomName("LinkStyle")]
    [DomNoInterfaceObject]
    public interface ILinkStyle
    {
        /// <summary>
        /// Gets the StyleSheet object associated with the given element, or
        /// null if there is none.
        /// </summary>
        [DomName("sheet")]
        IStyleSheet Sheet { get; }
    }
}
