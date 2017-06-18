using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup.Html
{
    /// <summary>
    /// An enumeration with all contenteditable modes.
    /// </summary>
    enum ContentEditableMode : byte
    {
        /// <summary>
        /// Not contenteditable.
        /// </summary>
        False,
        /// <summary>
        /// The element is contenteditable.
        /// </summary>
        True,
        /// <summary>
        /// Inherited from the parent element.
        /// </summary>
        Inherited
    }
}
