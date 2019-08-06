// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

using IctBaden.EventSourcing;

namespace TicTacToe.EventSourcing.Wpf.Game.Requests
{
    /// <summary>
    /// A player's request to set to a given place on the board.
    /// </summary>
    public class PlayerSetRequested : Request
    {
        public string Player { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        public PlayerSetRequested(string player, int row, int column)
        {
            Player = player;
            Row = row;
            Column = column;
        }
    }
}