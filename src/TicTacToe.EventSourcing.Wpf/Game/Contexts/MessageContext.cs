using IctBaden.EventSourcing;
using TicTacToe.EventSourcing.Wpf.Game.Events;

namespace TicTacToe.EventSourcing.Wpf.Game.Contexts
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MessageContext :
        IEventHandler<NewGameStarted>,
        IEventHandler<GameOver>,
        IEventHandler<PlayerSet>,
        IEventHandler<PlayerSetDenied>
    {
        public string Info { get; private set; }
        public string Error { get; private set; }

        public void Apply(NewGameStarted eventDto)
        {
            Info = "Start playing..";
            Error = "";
        }

        public void Apply(GameOver eventDto)
        {
            Info = eventDto.Winner;
            Error = "";
        }

        public void Apply(PlayerSet eventDto)
        {
            Info = "";
            Error = "";
        }

        public void Apply(PlayerSetDenied eventDto)
        {
            Error = eventDto.Message;
        }

    }
}