using System;
using System.Collections.Generic;

namespace IctBaden.EventSourcing
{
    public interface IEventStore : IDisposable
    {
        EventContext Context { get; set; }

        void Save(Event eventDto);
        void Save(Event[] events);
        IEnumerable<Event> Replay();
        IEnumerable<Event> Replay(Type[] eventTypes);
    }

}
