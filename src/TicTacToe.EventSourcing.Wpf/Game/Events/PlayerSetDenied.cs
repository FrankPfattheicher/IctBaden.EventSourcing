// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

using IctBaden.EventSourcing;

namespace TicTacToe.EventSourcing.Wpf.Game.Events
{
    public class PlayerSetDenied : Event
    {
        public string Player { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public string Message { get; private set; }

        public PlayerSetDenied(string player, int row, int column, string message)
        {
            Player = player;
            Row = row;
            Column = column;
            Message = message;
        }
    }
}