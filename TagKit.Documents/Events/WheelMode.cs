using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Events
{
    /// <summary>
    /// Enumeration with the various mouse wheel modes.
    /// </summary>
    [DomName("WheelEvent")]
    public enum WheelMode : byte
    {
        /// <summary>
        /// The unit of change is pixels.
        /// </summary>
        [DomName("DOM_DELTA_PIXEL")]
        Pixel = 0x0,
        /// <summary>
        /// The unit of change is lines.
        /// </summary>
        [DomName("DOM_DELTA_LINE")]
        Line = 0x1,
        /// <summary>
        /// The unit of change is pages.
        /// </summary>
        [DomName("DOM_DELTA_PAGE")]
        Page = 0x2
    }
}
