using System.ComponentModel;
using System.Windows.Media;
using TicTacToe.Wpf.Annotations;

namespace TicTacToe.Wpf
{
    class GameViewModel : INotifyPropertyChanged
    {
        private readonly Game.TicTacToe _game;

        public string[][] GameLines => _game.Board;
        public string Player => $"Player {_game.CurrentPlayer}";

        public string Message { get; private set; }

        public Color MessageColor => _game.Error ? Colors.Red : Colors.Blue;


        public GameViewModel()
        {
            _game = new Game.TicTacToe();
            OnNewGame();
        }

        public void OnClick(int row, int col)
        {
            Message = _game.Set(row, col);
            OnPropertyChanged();
        }

        public void OnNewGame()
        {
            _game.NewGame();
            Message = "Start playing..";
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
