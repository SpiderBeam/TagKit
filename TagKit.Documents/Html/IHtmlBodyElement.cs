using TagKit.Documents.Events;
using TagKit.Documents.Nodes.Html;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Html
{
    /// <summary>
    /// Represents the body HTML element.
    /// </summary>
    [DomName("HTMLBodyElement")]
    public interface IHtmlBodyElement : IHtmlElement, IWindowEventHandlers
    {
    }
}
