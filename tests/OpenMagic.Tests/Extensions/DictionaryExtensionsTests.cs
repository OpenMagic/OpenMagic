using System.Collections.Generic;
using FluentAssertions;
using OpenMagic.Extensions;
using Xunit;

namespace OpenMagic.Tests.Extensions
{
    public class DictionaryExtensionsTests
    {
        public class FindValue
        {
            [Fact]
            public void ShouldReturnValueIfKeyIsInDictionary()
            {
                // Given
                var dictionary = new Dictionary<int, string> {{1, "a"}, {2, "b"}};

                // When / Then
                dictionary.FindValue(1).Should().Be("a");
                dictionary.FindValue(2).Should().Be("b");
            }

            [Fact]
            public void ShouldReturnNullIfKeyIsNotDictionary()
            {
                // Given
                var dictionary = new Dictionary<int, string> {{1, "a"}};

                // When / Then
                dictionary.FindValue(2).Should().BeNull();
            }
        }
    }
}