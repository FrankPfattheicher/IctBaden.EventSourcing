using IctBaden.EventSourcing;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace TicTacToe.EventSourcing.Wpf.Game.Events
{
    /// <summary>
    /// The game is over.
    /// </summary>
    public class GameOver : Event
    {
        public string Winner { get; private set; }

        public GameOver(string winner)
        {
            Winner = winner;
        }
    }
}
