// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
namespace TicTacToe.EventSourcing.Wpf.Game.Events
{
    // A player has set to a given place on the board.
    public class PlayerSet
    {
        public string Player { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        public PlayerSet(string player, int row, int column)
        {
            Player = player;
            Row = row;
            Column = column;
        }
    }
}