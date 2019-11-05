using System.Linq;
using IctBaden.EventSourcing;
using TicTacToe.EventSourcing.Wpf.Game.Events;
// ReSharper disable MemberCanBePrivate.Global

namespace TicTacToe.EventSourcing.Wpf.Game.Contexts
{
    /// <summary>
    /// This is the game's board.
    /// It exposes all fields of the board with it's set states.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class BoardContext : 
        IEventHandler<NewGameStarted>, 
        IEventHandler<PlayerSet>
    {
        private readonly EventContext _context;
        public string[][] Board { get; private set; }

        public BoardContext(EventContext context)
        {
            _context = context;
            Apply(new NewGameStarted());
        }

        public void Apply(NewGameStarted eventDto)
        {
            Board = new string[3][];
            Board[0] = new[] { " ", " ", " " };
            Board[1] = new[] { " ", " ", " " };
            Board[2] = new[] { " ", " ", " " };
        }

        public void Apply(PlayerSet eventDto)
        {
            Board[eventDto.Row][eventDto.Column] = eventDto.Player;

            var winner = GetWinner();
            if (winner != null)
            {
                _context.Notify(new GameOver(winner));
            }
        }

        public string GetWinner()
        {
            // lines
            if (Board.Any(line => line.All(place => place == "X")))
                return "X is the winner.";
            if (Board.Any(line => line.All(place => place == "O")))
                return "O is the winner.";

            //columns
            if (Enumerable.Range(0, Board.Length).Any(ix => Board.All(line => line[ix] == "X")))
                return "X is the winner.";
            if (Enumerable.Range(0, Board.Length).Any(ix => Board.All(line => line[ix] == "O")))
                return "O is the winner.";

            //diagonals
            var diagonals = new[]
            {
                new [] { Board[0][0], Board[1][1], Board[2][2] },
                new [] { Board[0][2], Board[1][1], Board[2][0] }
            };
            if (diagonals.Any(line => line.All(place => place == "X")))
                return "X is the winner.";
            if (diagonals.Any(line => line.All(place => place == "O")))
                return "O is the winner.";

            // empty places?
            if (!Board.Any(line => line.Any(place => place == " ")))
                return "Nobody wins.";

            return null;
        }

        public override string ToString()
        {
            var text = $"{Board[0][0]}.{Board[0][1]}.{Board[0][2]}|"
                   + $"{Board[1][0]}.{Board[1][1]}.{Board[1][2]}|"
                   + $"{Board[2][0]}.{Board[2][1]}.{Board[2][2]}";

            return text.Replace(" ", "_");
        }
    }

}

