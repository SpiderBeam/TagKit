using System;
using TagKit.Documents.Events;
using TagKit.Documents.Nodes;

namespace TagKit.Markup.Events
{
    /// <summary>
    /// The event that is published in case of starting XML parsing.
    /// </summary>
    public class XmlParseEvent : Event
    {
        /// <summary>
        /// Creates a new event for starting XML parsing.
        /// </summary>
        /// <param name="document">The document to be filled.</param>
        /// <param name="completed">Determines if parsing is done.</param>
        public XmlParseEvent(IDocument document, Boolean completed)
            : base(completed ? EventNames.Parsed : EventNames.Parsing)
        {
            Document = document;
        }

        /// <summary>
        /// Gets the document, which is to be filled.
        /// </summary>
        public IDocument Document
        {
            get;
            private set;
        }
    }
}
