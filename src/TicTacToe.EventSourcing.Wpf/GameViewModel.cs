using System.ComponentModel;
using System.Windows.Media;
using TicTacToe.EventSourcing.Wpf.Game.Contexts;
using TicTacToe.EventSourcing.Wpf.Game.Requests;
using TicTacToe.Wpf.Annotations;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace TicTacToe.EventSourcing.Wpf
{
    /// <summary>
    /// View of the game
    /// </summary>
    class GameViewModel : INotifyPropertyChanged
    {
        public string[][] GameLines => Program.Context.GetContext<BoardContext>().Board;
        public string Player => $"Player {Program.Context.GetContext<PlayersContext>().CurrentPlayer}";

        public bool Error { get; private set; }
        public string Message { get; private set; }

        public Color MessageColor => Error ? Colors.Red : Colors.Blue;


        public GameViewModel()
        {
            Message = "Start playing..";
        }

        public void OnClick(int row, int col)
        {
            //Message = _game.Set(row, col);
            OnPropertyChanged();
        }

        public void OnNewGame()
        {
            Program.Context.Notify(new StartNewGameRequested());
            Message = string.Empty;
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
