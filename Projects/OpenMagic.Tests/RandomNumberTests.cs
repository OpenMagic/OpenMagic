using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenMagic.Tests
{
    public class RandomNumberTests
    {
        [TestClass]
        public class NextInt
        {
            [TestMethod]
            public void ShouldGetRandomNumbers()
            {

                // Given
                var randomNumbers = new List<int>();

                // When
                for (int index = 1; index <= 1000; index++)
                {
                    randomNumbers.Add(RandomNumber.NextInt());
                }

                // Then
                var uniqueNumbers = from i in randomNumbers
                                    group i by i into Group
                                    select new { Number = Group };

                uniqueNumbers.Count().Should().BeGreaterThan(900, "because there should be more than 900 random int.");
            }
        }
    }
}
