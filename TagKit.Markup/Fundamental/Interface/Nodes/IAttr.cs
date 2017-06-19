using System;
using TagKit.Foundation.Attributes;

namespace TagKit.Markup.Fundamental.Nodes
{
    /// <summary>
    /// This type represents a DOM element's attribute as an object. 
    /// </summary>
    [DomName("Attr")]
    public interface IAttr : IEquatable<IAttr>
    {
        /// <summary>
        /// Gets the namespace URL of the attribute.
        /// </summary>
        [DomName("namespaceURI")]
        String NamespaceUri { get; }

        /// <summary>
        /// Gets the prefix used by the namespace.
        /// </summary>
        [DomName("prefix")]
        String Prefix { get; }
        /// <summary>
        /// Gets the local name of the attribute.
        /// </summary>
        [DomName("localName")]
        String LocalName { get; }

        /// <summary>
        /// Gets the attribute's name.
        /// </summary>
        [DomName("name")]
        String Name { get; }
        /// <summary>
        /// For legacy use, alias of .Name.
        /// </summary>
        [DomName("name")]
        String NodeName { get; }

        /// <summary>
        /// Gets the attribute's value.
        /// </summary>
        [DomName("value")]
        String Value { get; set; }

        /// <summary>
        /// Useless; always returns true.
        /// </summary>
        [DomName("specified")]
        Boolean Specified { get;}

    }
}
