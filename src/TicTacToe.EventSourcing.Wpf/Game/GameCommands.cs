using System.Threading;
using System.Threading.Tasks;
using IctBaden.EventSourcing;

namespace TicTacToe.EventSourcing.Wpf.Game
{
    public class GameCommands : IHandler<NewGameRequest>
    {
        private readonly EventSession _session;

        public GameCommands(EventSession session)
        {
            _session = session;
        }

        public Task<bool> Handle(NewGameRequest eventDto, CancellationToken token = default)
        {
            _session.Notify(new NewGameStarted(), token);
            return Task.FromResult(true);
        }

    }
}