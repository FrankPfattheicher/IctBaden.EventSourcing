using System;
using IctBaden.EventSourcing;
using TicTacToe.EventSourcing.Wpf.Game.Events;
using TicTacToe.EventSourcing.Wpf.Game.Requests;

namespace TicTacToe.EventSourcing.Wpf.Game.Contexts
{
    /// <summary>
    /// Requests targeting the entire game.
    /// </summary>
    public class GameContext : 
        IHandler<StartNewGameRequested>, 
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

        public void Handle(StartNewGameRequested eventDto)
        {
            // This is allowed at every time - no checks required.
            _context.Notify(new NewGameStarted());
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
            _context.Notify(new NextPlayerSelected());
        }

    }
}
