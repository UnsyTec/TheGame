namespace Core
{
    using System;

    using Interfaces;

    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        public int GetRandomNumber()
        {
            var random = new Random();

            return random.Next(1, 100);
        }
    }
}
