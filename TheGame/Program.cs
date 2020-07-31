namespace TheGame
{
    using System;
    using System.Configuration;

    using Core;

    using Interfaces;

    class Program
    {
        private static IGameCore _game;

        private static ILeaderBoard _leaderBoard = new LeaderBoard();

        private static string _userName;

        private static int _pointsToWin;

        static void Main(string[] args)
        {
            int.TryParse(ConfigurationManager.AppSettings["PointsToWin"], out _pointsToWin);
            _game = new GameCore(new RandomNumberGenerator(), _leaderBoard);

            Console.WriteLine("Please enter your username:");

            _userName = Console.ReadLine();

            if (string.IsNullOrEmpty(_userName))
            {
                Console.WriteLine("Your username is invalid. Quitting");
                return;
            }

            _game.SetUsername(_userName);

            while (true)
            {
                NewGame();
                Console.WriteLine("Play again? y/n");
                if (Console.ReadLine().ToLower().Equals("n"))
                {
                    break;
                }

                _game.ResetGame();
            }
        }

        private static void NewGame()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to The Game {_userName}");

            WriteLeaderBoard();
            Console.WriteLine("=====================");

            var seedNumber = _game.SeedNumber();

            Console.WriteLine($"Your starting point number is '{seedNumber}");
            Console.WriteLine("type 'h' for higher, 'l' for lower");
            var selection = Console.ReadLine();

            var result = _game.NumberChecker(selection);
            var congratsOrNot = result ? "Congratulations. You guessed correctly" : "Commiserations. You guessed incorrectly";

            if (result)
            {
                selection = selection == "h" ? "higher" : "lower";
                Console.WriteLine($"{congratsOrNot} that {_game.CurrentNumber} was going to be {selection} than {_game.PreviousNumber}");
            }

            while (result)
            {
                Console.WriteLine($"So, now your new number is {_game.CurrentNumber}");
                Console.WriteLine("type 'h' for higher, 'l' for lower");
                selection = Console.ReadLine();
                Console.WriteLine();

                result = _game.NumberChecker(selection);

                selection = selection == "h" ? "higher" : "lower";

                congratsOrNot = result ? "Congratulations. You guessed correctly" : "Commiserations. You guessed incorrectly";

                Console.WriteLine($"{congratsOrNot} that {_game.CurrentNumber} was going to be {selection} than {_game.PreviousNumber}");

                if (_game.Points.Equals(_pointsToWin))
                {
                    _game.EndGame();
                    Console.WriteLine($"Congratulations. You have won!!! You did it in {_game.UserScore.Seconds}");

                    WriteLeaderBoard();

                    return;
                }
            }
        }

        private static void WriteLeaderBoard()
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            var leaderBoardEntries = _leaderBoard.GetLeaderBoard();

            foreach (var entry in leaderBoardEntries)
            {
                Console.WriteLine($"User: {entry.Username.PadRight(20)} Time: {entry.Seconds} seconds");
            }
        }
    }
}
