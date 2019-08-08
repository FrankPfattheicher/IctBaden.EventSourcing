using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable UnusedMember.Global

namespace IctBaden.EventSourcing.EventStore
{
    /// <summary>
    /// This event store is mainly for test uses.
    /// It stores all events in memory.
    /// </summary>
    public class InMemoryEventStore : IEventStore
    {
        private readonly Dictionary<string, List<Event>> _store = new Dictionary<string, List<Event>>();    // one list per stream

        public void Dispose()
        {
        }

        public bool Save(string eventStream, Event eventDto)
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
                return true;
            }
        }

        public bool Save(string eventStream, Event[] events)
        {
            lock (_store)
            {
                foreach (var eventDto in events)
                {
                    if (!Save(eventStream, eventDto)) return false;
                }
                return true;
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
