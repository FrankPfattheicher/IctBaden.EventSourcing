using System.Threading;
using System.Threading.Tasks;
using IctBaden.EventSourcing;
using TicTacToe.EventSourcing.Wpf.Game.Events;

namespace TicTacToe.EventSourcing.Wpf.Game.Contexts
{
    /// <summary>
    /// This is the game's board.
    /// It exposes all fields of the board with it's set states.
    /// </summary>
    public class BoardContext : IHandler<NewGameStarted>, IHandler<PlayerSet>
    {
        public string[][] Board { get; private set; }

        public Task<bool> Handle(NewGameStarted eventDto, CancellationToken token = default)
        {
            Board = new string[3][];
            Board[0] = new[] { "X", " ", " " };
            Board[1] = new[] { " ", "O", " " };
            Board[2] = new[] { " ", " ", "X" };

            return Task.FromResult(true);
        }

        public Task<bool> Handle(PlayerSet eventDto, CancellationToken token = default)
        {
            return Task.FromResult(true);
        }
    }

}
