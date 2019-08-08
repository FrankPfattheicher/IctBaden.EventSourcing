using System;
using IctBaden.EventSourcing;
using IctBaden.EventSourcing.EventStore;

namespace TicTacToe.EventSourcing.Wpf
{
    public static class Program
    {
        public static EventContext Context;

        public static void Main()
        {
            var publisher = new AppDomainEventPublisher();
            //var store = new FileEventStore(AppDomain.CurrentDomain.BaseDirectory);
            var store = new InMemoryEventStore();
            Context = new EventContext(Guid.Empty.ToString("N"), store, publisher);
        }
    }
}
