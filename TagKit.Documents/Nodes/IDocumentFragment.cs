﻿using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Nodes
{
    /// <summary>
    /// The DocumentFragment interface represents a minimal document object
    /// that has no parent.
    /// </summary>
    [DomName("DocumentFragment")]
    public interface IDocumentFragment : INode, IParentNode, INonElementParentNode
    {
    }
}
