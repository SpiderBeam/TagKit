using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagKit.Documents.Events;

namespace TagKit.Configuration.Services.Default
{
    /// <summary>
    /// Provides string to Event instance creation mappings.
    /// </summary>
    sealed class EventFactory : IEventFactory
    {
        delegate Event Creator();

        readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { "event", () => new Event() },
            { "uievent", () => new UiEvent() },
            { "focusevent", () => new FocusEvent() },
            { "keyboardevent", () => new KeyboardEvent() },
            { "mouseevent", () => new MouseEvent() },
            { "wheelevent", () => new WheelEvent() },
            { "customevent", () => new CustomEvent() }
        };

        public EventFactory()
        {
            //Alias
            AddEventAlias("events", "event");
            AddEventAlias("htmlevents", "event");
            AddEventAlias("uievents", "uievent");
            AddEventAlias("keyevents", "keyboardevent");
            AddEventAlias("mouseevents", "mouseevent");
        }

        void AddEventAlias(String aliasName, String aliasFor)
        {
            creators.Add(aliasName, creators[aliasFor]);
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <returns>The created event.</returns>
        public Event Create(String name)
        {
            var creator = default(Creator);

            if (name != null && creators.TryGetValue(name, out creator))
            {
                return creator.Invoke();
            }

            return default(Event);
        }
    }
}
