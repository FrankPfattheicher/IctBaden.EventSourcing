using IctBaden.EventSourcing;
using IctBaden.EventSourcing.EventStore;
using TicTacToe.EventSourcing.Wpf.Game.Requests;

namespace TicTacToe.EventSourcing.Wpf
{
    public static class Program
    {
        public static EventContext Context;

        public static void Main()
        {
            var publisher = new AppDomainEventPublisher();
            var store = new InMemoryEventStore(publisher);
            Context = new EventContext(store);
            store.Context = Context;
            publisher.Context = Context;

            Context.Notify(new StartNewGameRequested());


        }
    }
}
