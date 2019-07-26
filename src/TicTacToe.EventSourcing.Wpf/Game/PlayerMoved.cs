namespace TicTacToe.EventSourcing.Wpf.Game
{
    public class PlayerMoved
    {
        public string Player { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        public PlayerMoved(string player, int row, int column)
        {
            Player = player;
            Row = row;
            Column = column;
        }
    }
}