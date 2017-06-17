using System;
using TagKit.Foundation.Attributes;

namespace TagKit.Documents.Nodes.Browser.Navigator
{
    /// <summary>
    /// Connectivity information regarding the navigator.
    /// </summary>
    [DomName("NavigatorOnLine")]
    [DomNoInterfaceObject]
    public interface INavigatorOnline
    {
        /// <summary>
        /// Gets if the connection is established.
        /// </summary>
        [DomName("onLine")]
        Boolean IsOnline { get; }
    }
}
