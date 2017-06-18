using TagKit.Foundation.Attributes;

namespace TagKit.Markup.Nodes
{
    /// <summary>
    /// An HTMLAllCollection is always rooted at document and matching all
    /// elements. It represents the tree of elements in a one-dimensional
    /// fashion.
    /// </summary>
    [DomName("HTMLAllCollection")]
    public interface IHtmlAllCollection : IHtmlCollection<IElement>
    {
    }
}
