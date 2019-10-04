using IctBaden.EventSourcing;
using TicTacToe.EventSourcing.Wpf.Game.Contexts;
using TicTacToe.EventSourcing.Wpf.Game.Events;

namespace TicTacToe.EventSourcing.Wpf.Game.Commands
{
    // ReSharper disable once UnusedMember.Global
    public class CommandsHandler :
        IHandler<StartNewGameCommand>, 
        IHandler<SelectNextPlayerCommand>,
        IHandler<PlayerSetCommand>
    {
        private readonly EventContext _context;

        public CommandsHandler(EventContext context)
        {
            _context = context;
        }
        

        public void Handle(StartNewGameCommand eventDto)
        {
            // This is allowed at every time - no checks required.
            _context.Notify(new NewGameStarted());
        }

        public void Handle(SelectNextPlayerCommand eventDto)
        {
            var game = _context.GetContextInstance<GameContext>();
            
            if (game.IsOver) return;
            _context.Notify(new NextPlayerSelected());
        }

        public void Handle(PlayerSetCommand eventDto)
        {
            var game = _context.GetContextInstance<GameContext>();
            if(game.IsOver)
            {
                _context.Notify(new PlayerSetDenied(eventDto.Player, eventDto.Row, eventDto.Column, "Game is over."));
                return;
            }

            var board = _context.GetContextInstance<BoardContext>();
            if (board.Board[eventDto.Row][eventDto.Column] != " ")
            {
                _context.Notify(new PlayerSetDenied(eventDto.Player, eventDto.Row, eventDto.Column, "Field not empty."));
            }
            else
            {
                _context.Notify(new PlayerSet(eventDto.Player, eventDto.Row, eventDto.Column));
            }
        }

        
    }
}