using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Documents.Events;

namespace TagKit.Configuration.Services
{
    /// <summary>
    /// Represents a factory to create event data.
    /// </summary>
    public interface IEventFactory
    {
        /// <summary>
        /// Creates a new event data object for the given event.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <returns>The event data for the given event.</returns>
        Event Create(String name);
    }
}
