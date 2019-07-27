using System.Threading;
using System.Threading.Tasks;
using IctBaden.EventSourcing;
using TicTacToe.EventSourcing.Wpf.Game.Events;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace TicTacToe.EventSourcing.Wpf.Game.Contexts
{
    /// <summary>
    /// Players context.
    /// Remember the plural form
    /// because it represents all participating players.
    /// You can also model each player separately.
    /// </summary>
    public class PlayersContext : IHandler<NewGameStarted>
    {
        public string[] Players { get; private set; }

        private int _currentPlayer;
        public string CurrentPlayer => Players[_currentPlayer];

        public PlayersContext()
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