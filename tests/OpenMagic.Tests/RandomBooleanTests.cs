using System.Linq;
using FluentAssertions;
using Xunit;

namespace OpenMagic.Tests;

public class RandomBooleanTests
{
    public class Next
    {
        [Fact]
        public void ShouldGetRandomValues()
        {
            // When
            var randomBooleans = RandomBoolean.Enumerable(1000);

            // Then
            randomBooleans.Count(b => b).Should().BeInRange(450, 550, "because approximately 50% of values should be true");
        }
    }
}