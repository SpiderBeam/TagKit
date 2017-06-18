using System;

namespace TagKit.Markup.Events
{
    /// <summary>
    /// Defines the callback signature for an event.
    /// </summary>
    /// <param name="sender">The callback this argument.</param>
    /// <param name="ev">The event arguments.</param>
    public delegate void DomEventHandler(Object sender, Event ev);
}
