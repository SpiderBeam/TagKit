using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup.Nodes
{
    /// <summary>
    /// Defines the callback signature for a mutation event.
    /// </summary>
    /// <param name="mutations">The sequence of mutations.</param>
    /// <param name="observer">The observer.</param>
    public delegate void MutationCallback(IMutationRecord[] mutations, MutationObserver observer);
}
