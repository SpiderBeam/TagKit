using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagKit.Markup
{
    /// <summary>
    /// Specifies how whitespace is handled in TagTextReader.
    /// </summary>
#if SILVERLIGHT
    internal enum WhitespaceHandling
#else
    public enum WhitespaceHandling
#endif
    {
        // Return all Whitespace and SignificantWhitespace nodes. This is the default.
        All = 0,

        // Return just SignificantWhitespace, i.e. whitespace nodes that are in scope of xml:space="preserve"
        Significant = 1,

        // Do not return any Whitespace or SignificantWhitespace nodes.
        None = 2
    }
}
