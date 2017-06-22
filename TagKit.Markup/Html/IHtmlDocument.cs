using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.Attributes;
using TagKit.Markup.Fundamental.Nodes;

namespace TagKit.Markup.Html
{
    /// <summary>
    /// Serves as an entry point to the content of an HTML document.
    /// </summary>
    [DomName("HTMLDocument")]
    public interface IHtmlDocument : IDocument
    {
    }
}