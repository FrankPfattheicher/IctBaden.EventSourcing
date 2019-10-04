using IctBaden.EventSourcing;
using TicTacToe.EventSourcing.Wpf.Game.Events;

namespace TicTacToe.EventSourcing.Wpf.Game.Contexts
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MessageContext :
        IHandler<NewGameStarted>,
        IHandler<GameOver>,
        IHandler<PlayerSet>,
        IHandler<PlayerSetDenied>
    {
        public string Info { get; private set; }
        public string Error { get; private set; }

        public void Handle(NewGameStarted eventDto)
        {
            Info = "Start playing..";
            Error = "";
        }

        public void Handle(GameOver eventDto)
        {
            Info = eventDto.Winner;
            Error = "";
        }

        public void Handle(PlayerSet eventDto)
        {
            Info = "";
            Error = "";
        }

        public void Handle(PlayerSetDenied eventDto)
        {
            Error = eventDto.Message;
        }

    }
}