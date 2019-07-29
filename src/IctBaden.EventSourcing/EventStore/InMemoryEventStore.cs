using System;
using System.Collections.Generic;
using System.Linq;

namespace IctBaden.EventSourcing.EventStore
{
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<string, List<Event>> _store = new Dictionary<string, List<Event>>();    // one list per stream
        private readonly IEventPublisher _publisher;

        public InMemoryEventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Dispose()
        {
        }

        public void Save(string eventStream, Event eventDto)
        {
            lock (_store)
            {
                if (_store.ContainsKey(eventStream))
                {
                    _store[eventStream].Add(eventDto);
                }
                else
                {
                    _store[eventStream] = new List<Event> { eventDto };
                }
                    
                _publisher.Publish(eventStream, eventDto).Wait();
            }
        }

        public void Save(string eventStream, Event[] events)
        {
            lock (_store)
            {
                foreach (var eventDto in events)
                {
                    Save(eventStream, eventDto);
                }
            }
        }

        public IEnumerable<Event> Replay(string eventStream)
        {
            lock (_store)
            {
                var events = new List<Event>();
                if (_store.ContainsKey(eventStream))
                {
                    events = _store[eventStream].ToList();
                }
                return events;
            }
        }

        public IEnumerable<Event> Replay(string eventStream, Type[] eventTypes)
        {
            lock (_store)
            {
                return Replay(eventStream)
                    .Where(ev => eventTypes.Contains(ev.GetType()))
                    .ToList();
            }
        }

    }
}
