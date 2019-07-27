// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
namespace TicTacToe.EventSourcing.Wpf.Game.Requests
{
    /// <summary>
    /// A player's request to set to a given place on the board.
    /// </summary>
    public class PlayerSetRequested
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