using FluentAssertions;
using OpenMagic.DataAnnotations;
using Xunit;

namespace OpenMagic.Tests.DataAnnotations
{
    public class UriAttributeTests
    {
        public class IsValid
        {
            [Fact]
            public void ShouldBeTrueWhenValueIsValidUri()
            {
                IsValid_For("http://example.com").Should().BeTrue();
            }

            [Fact]
            public void ShouldBeFalseWhenValueIsNotValidUri()
            {
                IsValid_For("an invalid url").Should().BeFalse();
                IsValid_For(2).Should().BeFalse();
            }

            [Fact]
            public void ShouldBeTrueWhenValueIsNull()
            {
                IsValid_For(null).Should().BeTrue();
            }

            [Fact]
            public void ShouldBeTrueWhenValueIsWhitespace()
            {
                IsValid_For("").Should().BeTrue();
            }

            private static bool IsValid_For(object uri) => new UriAttribute().IsValid(uri);
        }
    }
}