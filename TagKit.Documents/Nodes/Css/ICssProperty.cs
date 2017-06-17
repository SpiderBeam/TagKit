using System;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Nodes.Css
{
    /// <summary>
    /// Common interface of all CSS properties.
    /// </summary>
    [DomName("CSSProperty")]
    [DomNoInterfaceObject]
    public interface ICssProperty : ICssNode
    {
        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets the value of the property.
        /// </summary>
        [DomName("value")]
        String Value { get; }

        /// <summary>
        /// Gets if the !important flag has been set.
        /// </summary>
        [DomName("important")]
        Boolean IsImportant { get; }
    }
}