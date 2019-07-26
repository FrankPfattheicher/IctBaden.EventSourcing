namespace TicTacToe.EventSourcing.Wpf.Game
{
    public class MoveRequest : PlayerMoved
    {
        public MoveRequest(string player, int row, int column) : base(player, row, column)
        {
        }
    }
}