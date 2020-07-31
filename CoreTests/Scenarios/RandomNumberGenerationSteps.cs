namespace CoreTests.Scenarios
{
    using TechTalk.SpecFlow;

    using Xunit;

    [Binding]
    public class RandomNumberGenerationSteps
    {
        private readonly GameScenarioContext _context;

        public RandomNumberGenerationSteps(GameScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I have a random number generator")]
        public void GivenIHaveARandomNumberGenerator()
        {
            _context.InitialiseRandomNumberGenerator();
        }

        [When(@"I request a random number")]
        public void WhenIRequestARandomNumber()
        {
            _context.CallGetRandomNumber();
        }

        [Then(@"I get a random number returned between (.*) and (.*)")]
        public void ThenIGetARandomNumberReturnedBetweenAnd(int lowerNumber, int higherNumber)
        {
            Assert.True(lowerNumber <= _context.RandomNumberResult);
            Assert.True(higherNumber >= _context.RandomNumberResult);
        }

        [When(@"I request a random number '(.*)' times")]
        public void WhenIRequestARandomNumberTimes(int numberOfIterations)
        {
            _context.CallGetRandomNumberInLoop(numberOfIterations);
        }

        [Then(@"I get only random numbers between (.*) and (.*)")]
        public void ThenIGetOnlyRandomNumbersBetweenAnd(int lowerNumber, int higherNumber)
        {
            foreach (var number in _context.RandomNumberResultList)
            {
                Assert.True(lowerNumber <= number);
                Assert.True(higherNumber >= number);
            }
        }

        [Given(@"I have a random number generator set to return '(.*)' first, then '(.*)' second, then '(.*)' on the third time")]
        public void GivenIHaveARandomNumberGeneratorSetToReturnFirstThenSecondThenOnTheThirdTime(int firstNumber, int secondNumber, int thirdNumber)
        {
            _context.SetRandomNumbersReturned(firstNumber, secondNumber, thirdNumber);
        }

        [Given(@"I have a Game object")]
        public void GivenIHaveAGameObject()
        {
            _context.SetUpGameObject();
        }

        [When(@"I request a random number from the random number checker from the Game object")]
        public void WhenIRequestARandomNumberFromTheRandomNumberCheckerFromTheGameObject()
        {
            _context.CallNumberChecker();
        }

        [Then(@"I retry until I get a number that I have not yet had and the random number generator random number request will have been invoked '(.*)' times")]
        public void ThenIRetryUntilIGetANumberThatIHaveNotYetHadAndTheRandomNumberGeneratorRandomNumberRequestWillHaveBeenInvokedTimes(int expectedTimesRandomNumberGeneratorWasInvoked)
        {
            Assert.Equal(expectedTimesRandomNumberGeneratorWasInvoked, _context.MockRandomNumberGeneratorInvocations);
        }
    }
}
