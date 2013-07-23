using OpenMagic.DataAnnotations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenMagic.Tests.DataAnnotations
{
    [TestClass]
    public class UriAttributeTests
    {
        [TestClass]
        public class IsValid
        {
            [TestMethod]
            public void ShouldBeTrueWhenValueIsValidUri()
            {
                IsValid_For("http://example.com").Should().BeTrue();
            }

            [TestMethod]
            public void ShouldBeFalseWhenValueIsNotValidUri()
            {
                IsValid_For("an invalid url").Should().BeFalse();
                IsValid_For(2).Should().BeFalse();
            }

            [TestMethod]
            public void ShouldBeTrueWhenValueIsNull()
            {
                IsValid_For(null).Should().BeTrue();
            }

            [TestMethod]
            public void ShouldBeTrueWhenValueIsWhitespace()
            {
                IsValid_For("").Should().BeTrue();
            }

            private bool IsValid_For(object uri)
            {
                return (new UriAttribute()).IsValid(uri);
            }
        }
    }
}
