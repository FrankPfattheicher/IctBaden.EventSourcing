using IctBaden.EventSourcing;
using IctBaden.EventSourcing.EventStore;
using TicTacToe.EventSourcing.Wpf.Game;

namespace TicTacToe.EventSourcing.Wpf
{
    public static class Program
    {
        public static EventSession Session;

        public static void Main()
        {
            var publisher = new AppDomainEventPublisher();
            var store = new InMemoryEventStore(publisher);
            Session = new EventSession(store);
            store.Session = Session;
            publisher.Session = Session;

            Session.Notify(new NewGameRequest());


        }
    }
}
