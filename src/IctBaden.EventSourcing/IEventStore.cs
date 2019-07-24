using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IctBaden.EventSourcing
{
    public interface IEventStore : IDisposable
    {
        void Save(Event eventDto);
        void Save(Event[] events);
        IEnumerable<Event> Replay();
        IEnumerable<Event> Replay(Type[] eventTypes);
    }

}
