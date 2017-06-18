using TagKit.Foundation.Attributes;
using TagKit.Markup.Nodes;

namespace TagKit.Markup.Html
{
    /// <summary>
    /// Represents a collection of HTML form controls.
    /// </summary>
    [DomName("HTMLFormControlsCollection")]
    public interface IHtmlFormControlsCollection : IHtmlCollection<IHtmlElement>
    {
    }
}
