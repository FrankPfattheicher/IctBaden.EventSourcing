using IctBaden.EventSourcing;
using TicTacToe.EventSourcing.Wpf.Game.Commands;
using TicTacToe.EventSourcing.Wpf.Game.Events;

namespace TicTacToe.EventSourcing.Wpf.Game.Contexts
{
    /// <summary>
    /// The entire game context.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GameContext :
        IHandler<NewGameStarted>, 
        IHandler<GameOver>,
        IHandler<PlayerSet>
    {
        private readonly EventContext _context;

        public bool IsOver { get; private set; }


        public GameContext(EventContext context)
        {
            _context = context;
        }

        public void Handle(NewGameStarted eventDto)
        {
            IsOver = false;
        }

        public void Handle(GameOver eventDto)
        {
            IsOver = true;
        }

        public void Handle(PlayerSet eventDto)
        {
            _context.ExecuteCommand(new SelectNextPlayerCommand());
        }

    }
}
