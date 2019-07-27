using System.Threading;
using System.Threading.Tasks;
using IctBaden.EventSourcing;
using TicTacToe.EventSourcing.Wpf.Game.Events;
using TicTacToe.EventSourcing.Wpf.Game.Requests;

namespace TicTacToe.EventSourcing.Wpf.Game.Contexts
{
    /// <summary>
    /// Requests targeting the entire game.
    /// </summary>
    public class GameContext : IHandler<StartNewGameRequested>, 
        IHandler<NewGameStarted>, IHandler<GameOver>
    {
        private readonly EventContext _context;

        public bool IsRunning { get; private set; }

        public GameContext(EventContext context)
        {
            _context = context;
        }

        public Task<bool> Handle(StartNewGameRequested eventDto, CancellationToken token = default)
        {
            // This is allowed at every time - no checks required.
            _context.Notify(new NewGameStarted(), token);
            return Task.FromResult(true);
        }

        public Task<bool> Handle(NewGameStarted eventDto, CancellationToken token = default)
        {
            IsRunning = true;
            return Task.FromResult(true);
        }

        public Task<bool> Handle(GameOver eventDto, CancellationToken token = default)
        {
            IsRunning = false;
            return Task.FromResult(true);
        }

    }
}