﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Foundation.Common
{
    /// <summary>
    /// Implemented by OM classes that may change internal state reflected with
    /// a changed string representation.
    /// </summary>
    public interface IBindable
    {
        /// <summary>
        /// Triggered when the internal state changed.
        /// </summary>
        event Action<String> Changed;

        /// <summary>
        /// Update the string representation without calling Changed.
        /// </summary>
        /// <param name="value">The representation's new value.</param>
        void Update(String value);
    }
}