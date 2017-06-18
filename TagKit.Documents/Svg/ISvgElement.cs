using TagKit.Documents.Nodes;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Svg
{
    /// <summary>
    /// The SVGElement interface represents any SVG element. Some elements directly
    /// implement this interface, other implement it via an interface that inherit it.
    /// </summary>
    [DomName("SVGElement")]
    public interface ISvgElement : IElement
    {
    }
}
