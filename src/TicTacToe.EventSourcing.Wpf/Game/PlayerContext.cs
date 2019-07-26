using System.Threading;
using System.Threading.Tasks;
using IctBaden.EventSourcing;

namespace TicTacToe.EventSourcing.Wpf.Game
{
    public class PlayerContext : IHandler<NewGameStarted>
    {
        public string[] Players { get; private set; }

        private int _currentPlayer;
        public string CurrentPlayer => Players[_currentPlayer];

        public PlayerContext()
        {
            Players = new[] { "X", "O" };
        }

        public Task<bool> Handle(NewGameStarted eventDto, CancellationToken token = default)
        {
            _currentPlayer = 0;
            return Task.FromResult(true);
        }
    }
}