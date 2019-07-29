using System;
using System.Windows.Media;
using IctBaden.EventSourcing;
using TicTacToe.EventSourcing.Wpf.Game.Events;

namespace TicTacToe.EventSourcing.Wpf.Game.Contexts
{
    public class MessageContext :
        IHandler<NewGameStarted>,
        IHandler<GameOver>,
        IHandler<PlayerSet>,
        IHandler<PlayerSetDenied>
    {
        public bool Error { get; private set; }
        public string Text { get; private set; }
        public Color TextColor => Error ? Colors.Red : Colors.Blue;

        public event Action MessageChanged;

        public void Handle(NewGameStarted eventDto)
        {
            Error = false;
            Text = "Start playing..";
            MessageChanged?.Invoke();
        }

        public void Handle(GameOver eventDto)
        {
            Error = false;
            Text = eventDto.Winner;
            MessageChanged?.Invoke();
        }

        public void Handle(PlayerSet eventDto)
        {
            Error = false;
            Text = "";
            MessageChanged?.Invoke();
        }

        public void Handle(PlayerSetDenied eventDto)
        {
            Error = true;
            Text = eventDto.Message;
            MessageChanged?.Invoke();
        }

    }
}