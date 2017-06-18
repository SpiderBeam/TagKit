using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.Attributes;

namespace TagKit.Markup.Nodes
{
    /// <summary>
    /// List of possible shadow root mode states.
    /// </summary>
    [DomName("ShadowRootMode")]
    [DomLiterals]
    public enum ShadowRootMode : byte
    {
        /// <summary>
        /// Specifies open encapsulation mode.
        /// </summary>
        [DomName("open")]
        Open = 0,
        /// <summary>
        /// Specifies closed encapsulation mode.
        /// </summary>
        [DomName("closed")]
        Closed = 1
    }
}
