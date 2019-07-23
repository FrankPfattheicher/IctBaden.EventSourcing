using System.Linq;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace TicTacToe.Wpf.Game
{
    public class TicTacToe
    {
        public string[] Players { get; private set; }

        public string[][] Board { get; private set; }

        private int _currentPlayer;
        public string CurrentPlayer => Players[_currentPlayer];

        public bool Error { get; private set; }
        public bool GameOver { get; private set; }

        public TicTacToe()
        {
            Players = new[] {"X", "O"};
            _currentPlayer = 1;

            Board = new string[3][];
            Board[0] = new[] { "X", " ", " " };
            Board[1] = new[] { " ", "O", " " };
            Board[2] = new[] { " ", " ", "X" };
        }

        public void NewGame()
        {
            _currentPlayer = 0;

            Board[0] = new[] { " ", " ", " " };
            Board[1] = new[] { " ", " ", " " };
            Board[2] = new[] { " ", " ", " " };

            GameOver = false;
            Error = false;
        }

        public string Set(int row, int col)
        {
            Error = false;

            if (GameOver)
            {
                Error = true;
                return "Game is already over: " + Winner();
            }

            if (Board[row][col] != " ")
            {
                Error = true;
                return "Field not empty.";
            }

            Board[row][col] = CurrentPlayer;
            _currentPlayer = (_currentPlayer + 1) % Players.Length;
            var winner = Winner();
            GameOver = !string.IsNullOrEmpty(winner);
            return winner;
        }

        public string Winner()
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

            return string.Empty;
        }

    }
}