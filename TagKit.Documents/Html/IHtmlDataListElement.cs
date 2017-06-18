using TagKit.Documents.Nodes;
using TagKit.Documents.Nodes.Html;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Html
{
    /// <summary>
    /// Represents the datalist HTML element.
    /// </summary>
    [DomName("HTMLDataListElement")]
    public interface IHtmlDataListElement : IHtmlElement
    {
        /// <summary>
        /// Gets the associated options.
        /// </summary>
        [DomName("options")]
        IHtmlCollection<IHtmlOptionElement> Options { get; }
    }
}
