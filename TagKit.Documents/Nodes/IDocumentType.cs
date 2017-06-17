﻿using System;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Nodes
{
    /// <summary>
    /// The DocumentType interface represents a Node containing a doctype.
    /// </summary>
    [DomName("DocumentType")]
    public interface IDocumentType : INode, IChildNode
    {
        /// <summary>
        /// Gets or sets the name of the document type.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets or sets the public ID of the document type.
        /// </summary>
        [DomName("publicId")]
        String PublicIdentifier { get; }

        /// <summary>
        /// Gets or sets the system ID of the document type.
        /// </summary>
        [DomName("systemId")]
        String SystemIdentifier { get; }
    }
}
