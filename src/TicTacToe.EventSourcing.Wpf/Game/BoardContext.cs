using System.Threading;
using System.Threading.Tasks;
using IctBaden.EventSourcing;

namespace TicTacToe.EventSourcing.Wpf.Game
{
    public class BoardContext : IHandler<NewGameStarted>, IHandler<PlayerMoved>
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

        public Task<bool> Handle(PlayerMoved eventDto, CancellationToken token = default)
        {
            return Task.FromResult(true);
        }
    }

}
