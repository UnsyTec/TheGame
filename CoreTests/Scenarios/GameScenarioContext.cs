namespace CoreTests.Scenarios
{
    using System.Collections.Generic;

    using Core;

    using Interfaces;

    using Moq;

    public class GameScenarioContext
    {
        private readonly Mock<IRandomNumberGenerator> _mockRandomNumberGenerator = new Mock<IRandomNumberGenerator>();

        private readonly Mock<ILeaderBoard> _mockLeaderBoard = new Mock<ILeaderBoard>(); 
        
        private IRandomNumberGenerator _randomNumberGenerator;

        private IGameCore _game;

        public bool NumberCheckerResult { get; set; }

        public int RandomNumberResult { get; set; }

        public List<int> RandomNumberResultList { get; set; }

        public int MockRandomNumberGeneratorInvocations => _mockRandomNumberGenerator.Invocations.Count;

        public void InitialiseRandomNumberGenerator()
        {
            _randomNumberGenerator = new RandomNumberGenerator();
        }

        public void CallGetRandomNumber()
        {
            RandomNumberResult = _randomNumberGenerator.GetRandomNumber();
        }

        public void CallGetRandomNumberInLoop(int numberOfIterations)
        {
            RandomNumberResultList = new List<int>();
            for (var counter = 0; counter < numberOfIterations; counter++)
            {
                RandomNumberResultList.Add(_randomNumberGenerator.GetRandomNumber());
            }
        }

        public void SetRandomNumbersReturned(int firstNumber, int secondNumber, int thirdNumber)
        {
            _mockRandomNumberGenerator.Setup(x => x.GetRandomNumber()).Returns(
                () =>
                    {
                        if (_mockRandomNumberGenerator.Invocations.Count.Equals(1))
                        {
                            return firstNumber;
                        }

                        if (_mockRandomNumberGenerator.Invocations.Count.Equals(2))
                        {
                            return secondNumber;
                        }

                        if (_mockRandomNumberGenerator.Invocations.Count.Equals(3))
                        {
                            return thirdNumber;
                        }

                        return 1;
                    });
        }

        public void SetUpGameObject()
        {
            _game = new GameCore(_mockRandomNumberGenerator.Object, _mockLeaderBoard.Object);
        }

        public void CallNumberChecker()
        {
            NumberCheckerResult = _game.NumberChecker("h");
        }
    }
}
