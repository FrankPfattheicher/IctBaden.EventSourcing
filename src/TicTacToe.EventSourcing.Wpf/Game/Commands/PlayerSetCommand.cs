// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

using IctBaden.EventSourcing;

namespace TicTacToe.EventSourcing.Wpf.Game.Commands
{
    /// <summary>
    /// A player's command to set to a given place on the board.
    /// </summary>
    public class PlayerSetCommand : Command
    {
        public string Player { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        public PlayerSetCommand(string player, int row, int column)
        {
            Player = player;
            Row = row;
            Column = column;
        }
    }
}