using TagKit.Documents.Nodes.Css;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Nodes
{
    /// <summary>
    /// The PseudoElement interface is used for representing CSS
    /// pseudo-elements.
    /// </summary>
    [DomName("PseudoElement")]
    [DomNoInterfaceObject]
    public interface IPseudoElement : IStyleUtilities
    {
    }
}
