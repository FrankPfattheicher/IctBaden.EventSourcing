﻿// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

using IctBaden.EventSourcing;

namespace TicTacToe.EventSourcing.Wpf.Game.Events
{
    // A player has set to a given place on the board.
    public class PlayerSet : Event
    {
        public string Player { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        public PlayerSet(string player, int row, int column)
        {
            Player = player;
            Row = row;
            Column = column;
        }
    }
}