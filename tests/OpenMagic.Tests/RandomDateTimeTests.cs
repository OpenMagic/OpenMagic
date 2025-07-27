using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace OpenMagic.Tests
{
    public class RandomDateTimeTests
    {
        public class NextDateTime
        {
            [Fact]
            public void ShouldGetRandomValues()
            {
                // When
                var randomValues = RandomDateTime.Enumerable(1000);

                // Then
                var uniqueValues =
                    from i in randomValues
                    group i by i
                    into g
                    select new { DateTime = g };

                uniqueValues.Count()
                    .Should()
                    .BeGreaterThan(900, "because there should be more than 900 DateTime values");
            }
        }

        // ReSharper disable once InconsistentNaming

        public class NextDateTime_minValue_maxValue
        {
            [Fact]
            public void ShouldGetRandomValues()
            {
                // Given
                var minValue = new DateTime(2015, 1, 1);
                var maxValue = new DateTime(2015, 6, 30);
                const int requestNumberOfRandomDates = 1000;

                // When
                var randomNumbers = RandomDateTime.Enumerable(requestNumberOfRandomDates, minValue, maxValue);

                // Then
                const int expectedUniqueValues = (int)(requestNumberOfRandomDates * 0.9);
                var uniqueValues = randomNumbers.GroupBy(i => i).Select(g => new { Number = g });

                var actualCount = uniqueValues.Distinct().Count();

                actualCount.Should().BeGreaterThan(
                    expectedUniqueValues, 
                    "because there should be more than {0} random numbers", expectedUniqueValues
                );
            }

            [Fact]
            public void ShouldGetRandomValuesWithinDefinedRange()
            {
                // Given
                var minValue = new DateTime(2015, 1, 1);
                var maxValue = new DateTime(2015, 6, 30);

                // When
                var randomNumbers = RandomDateTime.Enumerable(1000, minValue, maxValue);

                // Then
                randomNumbers.Count(n => n < minValue || n >= maxValue).Should().Be(0);
            }
        }
    }
}