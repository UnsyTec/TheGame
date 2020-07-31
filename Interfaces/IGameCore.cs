namespace Interfaces
{
    using Models;

    public interface IGameCore
    {
        int Points { get; }

        int PreviousNumber { get; }

        int CurrentNumber { get; }

        UserScore UserScore { get; set; }

        int SeedNumber();

        bool NumberChecker(string higherOrLower);

        void SetUsername(string userName);

        void EndGame();

        void ResetGame();
    }
}
