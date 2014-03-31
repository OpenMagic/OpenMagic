using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace OpenMagic.Tests
{
    public class RandomNumberTests
    {
        public class NextInt
        {
            [Fact]
            public void ShouldGetRandomNumbers()
            {
                // Given
                var randomNumbers = new List<int>();

                // When
                for (var index = 1; index <= 1000; index++)
                {
                    randomNumbers.Add(RandomNumber.NextInt());
                }

                // Then
                var uniqueNumbers = from i in randomNumbers
                    group i by i
                    into Group
                    select new {Number = Group};

                uniqueNumbers.Count().Should().BeGreaterThan(900, "because there should be more than 900 random int.");
            }
        }
    }
}