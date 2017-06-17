﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Events
{
    /// <summary>
    /// An enumeration over all possible keyboard locations.
    /// </summary>
    [DomName("KeyboardEvent")]
    public enum KeyboardLocation : byte
    {
        /// <summary>
        /// The standard location.
        /// </summary>
        [DomName("DOM_KEY_LOCATION_STANDARD")]
        Standard = 0,
        /// <summary>
        /// The left location.
        /// </summary>
        [DomName("DOM_KEY_LOCATION_LEFT")]
        Left = 1,
        /// <summary>
        /// The right location.
        /// </summary>
        [DomName("DOM_KEY_LOCATION_RIGHT")]
        Right = 2,
        /// <summary>
        /// The location of the numpad.
        /// </summary>
        [DomName("DOM_KEY_LOCATION_NUMPAD")]
        NumPad = 3
    }
}
