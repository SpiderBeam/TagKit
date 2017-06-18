﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Foundation.Attributes;

namespace TagKit.Markup.Nodes
{
    /// <summary>
    /// Enumeration with possible values for the adjacent position insertation.
    /// </summary>
    public enum AdjacentPosition : byte
    {
        /// <summary>
        /// Before the element itself.
        /// </summary>
        [DomName("beforebegin")]
        BeforeBegin,
        /// <summary>
        /// Just inside the element, before its first child.
        /// </summary>
        [DomName("afterbegin")]
        AfterBegin,
        /// <summary>
        /// Just inside the element, after its last child.
        /// </summary>
        [DomName("beforebegin")]
        BeforeEnd,
        /// <summary>
        /// After the element itself.
        /// </summary>
        [DomName("afterend")]
        AfterEnd
    }
}