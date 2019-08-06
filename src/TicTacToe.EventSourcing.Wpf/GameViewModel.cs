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
        private readonly BoardContext _boardContext;
        private readonly PlayersContext _playersContext;
        private readonly MessageContext _messageContext;

        public string[][] GameLines => _boardContext.Board;
        public string Player => $"Player {_playersContext.CurrentPlayer}";
        public string Message => _messageContext.Text;
        public Color MessageColor => _messageContext.TextColor;

        public GameViewModel()
        {
            _boardContext = Program.Context.GetContext<BoardContext>();
            _boardContext.BoardChanged += () => OnPropertyChanged();

            _playersContext = Program.Context.GetContext<PlayersContext>();

            _messageContext = Program.Context.GetContext<MessageContext>();
            _messageContext.MessageChanged += () => OnPropertyChanged();
        }

        public void OnClick(int row, int col)
        {
            Program.Context.Notify(new PlayerSetRequested(_playersContext.CurrentPlayer, row, col));
        }

        public void OnNewGame()
        {
            Program.Context.Notify(new StartNewGameRequested());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
