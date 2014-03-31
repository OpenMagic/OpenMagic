using System.Collections.Generic;
using FluentAssertions;
using OpenMagic.Extensions.Collections.Generic;
using Xunit;

namespace OpenMagic.Tests.Extensions.Collections.Generic
{
    public class IEnumerableExtensions
    {
        public class IsNullOrEmpty
        {
            [Fact]
            public void ReturnsTrueWhenValueIsNull()
            {
                IEnumerable<string> nullString = null;
                IEnumerable<int> nullInt = null;

                nullString.IsNullOrEmpty().Should().BeTrue();
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
                var enumerableString = new List<string> {"a", "b"};
                var enumerableInt = new List<int> {1, 2};

                enumerableString.IsNullOrEmpty().Should().BeFalse();
                enumerableInt.IsNullOrEmpty().Should().BeFalse();
            }
        }
    }
}