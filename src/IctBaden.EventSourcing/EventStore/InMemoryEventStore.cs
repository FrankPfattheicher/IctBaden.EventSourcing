using System;
using System.Collections.Generic;
using System.Linq;

namespace IctBaden.EventSourcing.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        public EventContext Context { get; set; }

        private readonly List<Event> _store = new List<Event>();
        private readonly IEventPublisher _publisher;

        public InMemoryEventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Dispose()
        {
        }

        public void Save(Event eventDto)
        {
            lock (_store)
            {
                _store.Add(eventDto);
                _publisher.Publish(eventDto).Wait();
            }
        }

        public void Save(Event[] events)
        {
            lock (_store)
            {
                foreach (var eventDto in events)
                {
                    Save(eventDto);
                }
            }
        }

        public IEnumerable<Event> Replay()
        {
            lock (_store)
            {
                return _store.ToList();
            }
        }

        public IEnumerable<Event> Replay(Type[] eventTypes)
        {
            lock (_store)
            {
                return _store
                    .Where(ev => eventTypes.Contains(ev.GetType()))
                    .ToList();
            }
        }

    }
}
