using System.ComponentModel;
using IctBaden.EventSourcing;
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
        private readonly EventContext _context;
        private BoardContext BoardContext => _context.GetContextInstance<BoardContext>();
        private PlayersContext PlayersContext => _context.GetContextInstance<PlayersContext>();
        private MessageContext MessageContext => _context.GetContextInstance<MessageContext>();

        public string[][] GameLines => BoardContext.Board;
        public string Player => $"Player {PlayersContext.CurrentPlayer}";
        public string Info => MessageContext.Info;
        public string Error => MessageContext.Error;

        public GameViewModel()
        {
            _context = Program.Context;

            //_context.RegisterContextInstance(BoardContext);
            //_context.RegisterContextInstance(PlayersContext);
            //_context.RegisterContextInstance(MessageContext);
        }

        public void OnClick(int row, int col)
        {
            _context.Request(new PlayerSetRequested(PlayersContext.CurrentPlayer, row, col));
            OnPropertyChanged();
        }

        public void OnNewGame()
        {
            _context.Request(new StartNewGameRequested());
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
