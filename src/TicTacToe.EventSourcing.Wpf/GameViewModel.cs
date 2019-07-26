using System.ComponentModel;
using System.Windows.Media;
using TicTacToe.EventSourcing.Wpf.Game;
using TicTacToe.Wpf.Annotations;

namespace TicTacToe.EventSourcing.Wpf
{
    class GameViewModel : INotifyPropertyChanged
    {
        public string[][] GameLines => Program.Session.GetContext<BoardContext>().Board;
        public string Player => $"Player {Program.Session.GetContext<PlayerContext>().CurrentPlayer}";

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
            Program.Session.Notify(new NewGameRequest());
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
