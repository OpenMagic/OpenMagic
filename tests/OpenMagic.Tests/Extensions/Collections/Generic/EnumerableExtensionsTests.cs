using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OpenMagic.Extensions.Collections.Generic;
using Xunit;

namespace OpenMagic.Tests.Extensions.Collections.Generic
{
    public class EnumerableExtensionsTests
    {
        public class ForEach
        {
            [Fact]
            public void ShouldInvoke_action_OnEachItemIn_collection()
            {
                // Given
                var list = Enumerable.Range(1, 3).ToArray();
                var result = new List<int>();

                // When
                list.ForEach(result.Add);

                // Then
                result.Should().Equal(list);
            }
        }
        public class IsNullOrEmpty
        {
            [Fact]
            public void ReturnsTrueWhenValueIsNull()
            {
                IEnumerable<string> nullString = null;
                IEnumerable<int> nullInt = null;

                // ReSharper disable once ExpressionIsAlwaysNull
                nullString.IsNullOrEmpty().Should().BeTrue();
                // ReSharper disable once ExpressionIsAlwaysNull
                nullInt.IsNullOrEmpty().Should().BeTrue();
            }

            [Fact]
            public void ReturnsTrueWhenValueIsHasZeroElements()
            {
                var emptyString = new List<string>();
                var emptyInt = new List<int>();

                emptyString.IsNullOrEmpty().Should().BeTrue();
                emptyInt.IsNullOrEmpty().Should().BeTrue();
            }

            [Fact]
            public void ReturnsFalseWhenValueHasElements()
            {
                var enumerableString = new List<string> { "a", "b" };
                var enumerableInt = new List<int> { 1, 2 };

                enumerableString.IsNullOrEmpty().Should().BeFalse();
                enumerableInt.IsNullOrEmpty().Should().BeFalse();
            }
        }
    }
}