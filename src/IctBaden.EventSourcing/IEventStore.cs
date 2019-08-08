using System;
using System.Collections.Generic;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace IctBaden.EventSourcing
{
    public interface IEventStore : IDisposable
    {
        bool Save(string eventStream, Event eventDto);
        bool Save(string eventStream, Event[] events);
        IEnumerable<Event> Replay(string eventStream);
        IEnumerable<Event> Replay(string eventStream, Type[] eventTypes);
    }

}
