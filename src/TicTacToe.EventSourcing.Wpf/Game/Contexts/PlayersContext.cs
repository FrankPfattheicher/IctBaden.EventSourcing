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
    public class PlayersContext : 
        IHandler<NewGameStarted>,
        IHandler<NextPlayerSelected>
    {
        public string[] Players { get; private set; }

        private int _currentPlayer;
        public string CurrentPlayer => Players[_currentPlayer];

        public PlayersContext()
        {
            Players = new[] { "X", "O" };
        }

        public void Handle(NewGameStarted eventDto)
        {
            _currentPlayer = 0;
        }

        public void Handle(NextPlayerSelected eventDto)
        {
            _currentPlayer = (_currentPlayer + 1) % Players.Length;
        }
    }
}
