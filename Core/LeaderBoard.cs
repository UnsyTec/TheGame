namespace Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Interfaces;

    using Models;

    using Newtonsoft.Json;

    public class LeaderBoard : ILeaderBoard
    {
        private const string LeaderBoardFileName = "leaderboard.txt";

        public List<UserScore> GetLeaderBoard()
        {
            var leaderBoardFile = new FileInfo(LeaderBoardFileName);

            if (!leaderBoardFile.Exists)
            {
                return new List<UserScore>();
            }

            using (var streamReader = new StreamReader(LeaderBoardFileName))
            {
                var leaderBoardText = streamReader.ReadToEnd();

                var returnObject = JsonConvert.DeserializeObject<List<UserScore>>(leaderBoardText);

                return returnObject.OrderBy(x => x.Seconds).ToList();
            }
        }

        public void AddUserScoreToLeaderBoard(UserScore userScore)
        {
            var currentLeaderBoard = GetLeaderBoard();
            currentLeaderBoard.Add(userScore);

            var leaderBoardText = JsonConvert.SerializeObject(currentLeaderBoard.OrderBy(x => x.Seconds).Take(3).ToList());

            using (var streamWriter = new StreamWriter(LeaderBoardFileName))
            {
                streamWriter.WriteLine(leaderBoardText);
                streamWriter.Flush();
            }
        }
    }
}
