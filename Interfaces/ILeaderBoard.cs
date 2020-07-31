namespace Interfaces
{
    using System.Collections.Generic;

    using Models;

    public interface ILeaderBoard
    {
        List<UserScore> GetLeaderBoard();

        void AddUserScoreToLeaderBoard(UserScore userScore);
    }
}
