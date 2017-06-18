using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.Attributes;

namespace TagKit.Markup.Html.IO
{
    /// <summary>
    /// The interface implemented by elements that may load resources.
    /// </summary>
    [DomNoInterfaceObject]
    public interface ILoadableElement
    {
        /// <summary>
        /// Gets the current download or resource, if any.
        /// </summary>
        IDownload CurrentDownload { get; }
    }
}
