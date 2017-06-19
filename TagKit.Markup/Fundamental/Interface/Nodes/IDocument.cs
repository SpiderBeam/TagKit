﻿using System;
using TagKit.Foundation.Attributes;
using TagKit.Markup.Fundamental.Events;
using TagKit.Markup.Fundamental.Ranges;
using TagKit.Markup.Fundamental.Traversal;

namespace TagKit.Markup.Fundamental.Nodes
{
    /// <summary>
    /// The Document interface serves as an entry point to the web page's
    /// content.
    /// </summary>
    [DomName("Document")]
    public interface IDocument : INode, IParentNode, INonElementParentNode, IDisposable
    {
        /// <summary>
        /// Gets the DOM implementation associated with the current document.
        /// </summary>
        [DomName("implementation")]
        IImplementation Implementation { get; }

        /// <summary>
        /// Gets a string containing the URL of the current document.
        /// </summary>
        [DomName("URL")]
        String Url { get; }

        /// <summary>
        /// Gets the URI of the current document.
        /// </summary>
        [DomName("documentURI")]
        String DocumentUri { get; }

        /// <summary>
        /// Gets the Unicode serialization of document's origin.
        /// </summary>
        [DomName("origin")]
        String Origin { get; }

        /// <summary>
        /// Gets a value to indicate whether the document is rendered in Quirks
        /// mode (BackComp) or Strict mode (CSS1Compat).
        /// </summary>
        [DomName("compatMode")]
        String CompatMode { get; }

        /// <summary>
        /// Gets the character encoding of the current document.
        /// </summary>
        [DomName("characterSet")]
        String CharacterSet { get; }

        /// <summary>
        /// For legacy use, alias of .CharacterSet.
        /// </summary>
        [DomName("charset")]
        String Charset { get; }

        /// <summary>
        /// For legacy use, alias of .CharacterSet.
        /// </summary>
        [DomName("inputEncoding")]
        String InputEncoding { get; } 

        /// <summary>
        /// Gets the Content-Type from the MIME Header of the current document.
        /// </summary>
        [DomName("contentType")]
        String ContentType { get; }

        /// <summary>
        /// Gets the document type.
        /// </summary>
        [DomName("doctype")]
        IDocumentType Doctype { get; }

        /// <summary>
        /// Gets the root element of the document.
        /// </summary>
        [DomName("documentElement")]
        IElement DocumentElement { get; }

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The
        /// complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">
        /// A string representing the name of the elements. The special string
        /// "*" represents all elements.
        /// </param>
        /// <returns>
        /// A collection of elements in the order they appear in the tree.
        /// </returns>
        [DomName("getElementsByTagName")]
        IHtmlCollection<IElement> GetElementsByTagName(String tagName);

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the
        /// given namespace. The complete document is searched, including the
        /// root node.
        /// </summary>
        /// <param name="namespaceUri">
        /// The namespace URI of elements to look for.
        /// </param>
        /// <param name="tagName">
        /// Either the local name of elements to look for or the special value
        /// "*", which matches all elements.
        /// </param>
        /// <returns>
        /// A collection of elements in the order they appear in the tree.
        /// </returns>
        [DomName("getElementsByTagNameNS")]
        IHtmlCollection<IElement> GetElementsByTagName(String namespaceUri, String tagName);

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">
        /// A string representing the list of class names to match; class names
        /// are separated by whitespace.
        /// </param>
        /// <returns>A collection of elements.</returns>
        [DomName("getElementsByClassName")]
        IHtmlCollection<IElement> GetElementsByClassName(String classNames);

        /// <summary>
        /// Creates a new element with the given tag name.
        /// </summary>
        /// <param name="name">
        /// A string that specifies the type of element to be created.
        /// </param>
        /// <returns>The created element object.</returns>
        [DomName("createElement")]
        IElement CreateElement(String name);

        /// <summary>
        /// Creates a new element with the given tag name and namespace URI.
        /// </summary>
        /// <param name="namespaceUri">
        /// Specifies the namespace URI to associate with the element.
        /// </param>
        /// <param name="name">
        /// A string that specifies the type of element to be created.
        /// </param>
        /// <returns>The created element.</returns>
        [DomName("createElementNS")]
        IElement CreateElement(String namespaceUri, String name);

        /// <summary>
        /// Creates an empty DocumentFragment object.
        /// </summary>
        /// <returns>The new document fragment.</returns>
        [DomName("createDocumentFragment")]
        IDocumentFragment CreateDocumentFragment();

        /// <summary>
        /// Creates a new text node and returns it.
        /// </summary>
        /// <param name="data">
        /// A string containing the data to be put in the text node.
        /// </param>
        /// <returns>The created text node.</returns>
        [DomName("createTextNode")]
        IText CreateTextNode(String data);

        /// <summary>
        /// Creates a new comment node, and returns it.
        /// </summary>
        /// <param name="data">
        /// A string containing the data to be added to the Comment.
        /// </param>
        /// <returns>The new comment.</returns>
        [DomName("createComment")]
        IComment CreateComment(String data);

        /// <summary>
        /// Creates a ProcessingInstruction node given the specified name and
        /// data strings.
        /// </summary>
        /// <param name="target">
        /// The target part of the processing instruction.
        /// </param>
        /// <param name="data">The data for the node.</param>
        /// <returns>The new processing instruction.</returns>
        [DomName("createProcessingInstruction")]
        IProcessingInstruction CreateProcessingInstruction(String target, String data);

        /// <summary>
        /// Creates a copy of a node from an external document that can be
        /// inserted into the current document.
        /// </summary>
        /// <param name="externalNode">
        /// The node from another document to be imported.
        /// </param>
        /// <param name="deep">
        /// Optional argument, indicating whether the descendants of the
        /// imported node need to be imported.
        /// </param>
        /// <returns>
        /// The new node that is imported into the document. The new node's
        /// parentNode is null, since it has not yet been inserted into the
        /// document tree.
        /// </returns>
        [DomName("importNode")]
        INode Import(INode externalNode, Boolean deep = true);

        /// <summary>
        /// Adopts a node from an external document. The node and its subtree
        /// is removed from the document it's in (if any), and its
        /// ownerDocument is changed to the current document. The node can then
        /// be inserted into the current document. The new node's parentNode is
        /// null, since it has not yet been inserted into the document tree.
        /// </summary>
        /// <param name="externalNode">
        /// The node from another document to be adopted.
        /// </param>
        /// <returns>
        /// The adopted node that can be used in the current document.
        /// </returns>
        [DomName("adoptNode")]
        INode Adopt(INode externalNode);

        /// <summary>
        /// Creates an event of the type specified. 
        /// </summary>
        /// <param name="type">
        /// Represents the type of event (e.g., uievent, event, customevent,
        /// ...) to be created.
        /// </param>
        /// <returns>The event.</returns>
        [DomName("createEvent")]
        Event CreateEvent(String type);

        /// <summary>
        /// Creates a new Range object.
        /// </summary>
        /// <returns>The range.</returns>
        [DomName("createRange")]
        IRange CreateRange();

        /// <summary>
        /// Creates a new NodeIterator object.
        /// </summary>
        /// <param name="root">
        /// The root node at which to begin the NodeIterator's traversal.
        /// </param>
        /// <param name="settings">
        /// Indicates which nodes to iterate over.
        /// </param>
        /// <param name="filter">
        /// An optional callback function for filtering.
        /// </param>
        /// <returns>The created node NodeIterator.</returns>
        [DomName("createNodeIterator")]
        INodeIterator CreateNodeIterator(INode root, FilterSettings settings = FilterSettings.All,
            NodeFilter filter = null);

        /// <summary>
        /// Creates a new TreeWalker object.
        /// </summary>
        /// <param name="root">
        /// Is the root Node of this TreeWalker traversal.
        /// </param>
        /// <param name="settings">
        /// Indicates which nodes to iterate over.
        /// </param>
        /// <param name="filter">
        /// An optional callback function for filtering.
        /// </param>
        /// <returns>The created node TreeWalker.</returns>
        [DomName("createTreeWalker")]
        ITreeWalker CreateTreeWalker(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null);

    }
}
