using TagKit.Foundation.Attributes;

namespace TagKit.Markup.Browser
{
    /// <summary>
    /// Defines a set of methods for working with IO.
    /// </summary>
    [DomName("NavigatorStorageUtils")]
    [DomNoInterfaceObject]
    public interface INavigatorStorageUtilities
    {
        /// <summary>
        /// Blocks the current operation until storage operations have completed.
        /// </summary>
        [DomName("yieldForStorageUpdates")]
        void WaitForStorageUpdates();
    }
}
