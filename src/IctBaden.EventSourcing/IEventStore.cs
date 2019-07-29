using System;
using System.Collections.Generic;

namespace IctBaden.EventSourcing
{
    public interface IEventStore : IDisposable
    {
        void Save(string eventStream, Event eventDto);
        void Save(string eventStream, Event[] events);
        IEnumerable<Event> Replay(string eventStream);
        IEnumerable<Event> Replay(string eventStream, Type[] eventTypes);
    }

}
