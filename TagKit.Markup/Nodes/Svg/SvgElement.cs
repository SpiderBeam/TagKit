using System;
using TagKit.Documents.Svg;
using TagKit.Services.Configuration;

namespace TagKit.Markup.Nodes.Svg
{

    /// <summary>
    /// Represents an element of the SVG DOM.
    /// </summary>
    class SvgElement : Element, ISvgElement
    {
        #region ctor

        public SvgElement(Document owner, String name, String prefix = null, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, NamespaceNames.SvgUri, flags | NodeFlags.SvgMember)
        {
        }

        #endregion

        #region Helpers

        internal override Node Clone(Document owner, Boolean deep)
        {
            var factory = Context.GetFactory<IElementFactory<Document, SvgElement>>();
            var node = factory.Create(owner, LocalName, Prefix);
            CloneElement(node, owner, deep);
            return node;
        }

        #endregion
    }
}
