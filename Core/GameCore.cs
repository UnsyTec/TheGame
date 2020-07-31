namespace Core
{
    using System;
    using System.Collections.Generic;

    using Interfaces;

    using Models;

    public class GameCore : IGameCore
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        private readonly List<int> _previousNumbers = new List<int>();

        private readonly ILeaderBoard _leaderBoard;

        public GameCore(IRandomNumberGenerator randomNumberGenerator, ILeaderBoard leaderBoard)
        {
            _randomNumberGenerator =
                randomNumberGenerator ?? throw new ArgumentNullException(nameof(randomNumberGenerator));

            _leaderBoard = leaderBoard ?? throw new ArgumentNullException(nameof(leaderBoard));
        }

        public int Points { get; private set; }

        public int PreviousNumber { get; set; }

        public int CurrentNumber { get; set; }

        public UserScore UserScore { get; set; }

        public int SeedNumber()
        {
            CurrentNumber = _randomNumberGenerator.GetRandomNumber();
            _previousNumbers.Add(CurrentNumber);
            return CurrentNumber;
        }

        public bool NumberChecker(string higherOrLower)
        {
            var newNumber = _randomNumberGenerator.GetRandomNumber();
            while (_previousNumbers.Contains(newNumber))
            {
                newNumber = _randomNumberGenerator.GetRandomNumber();
            }

            PreviousNumber = CurrentNumber;
            CurrentNumber = newNumber;
            _previousNumbers.Add(newNumber);

            var userCorrect = higherOrLower.Equals("h") ? (CurrentNumber > PreviousNumber) : (CurrentNumber < PreviousNumber);

            CurrentNumber = newNumber;

            if (userCorrect)
            {
                Points++;
            }

            return userCorrect;
        }

        public void SetUsername(string userName)
        {
            UserScore = new UserScore { Username = userName, StartTime = DateTime.Now };
        }

        public void EndGame()
        {
            UserScore.EndTime = DateTime.Now;
            _leaderBoard.AddUserScoreToLeaderBoard(UserScore);
        }

        public void ResetGame()
        {
            Points = 0;
            _previousNumbers.Clear();
            PreviousNumber = 0;
            CurrentNumber = 0;

            UserScore.StartTime = DateTime.Now;
        }
    }
}
