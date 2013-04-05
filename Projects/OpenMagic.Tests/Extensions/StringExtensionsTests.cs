using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMagic.Extensions;

namespace OpenMagic.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestClass]
        public class IsNullOrWhiteSpace
        {
            [TestMethod]
            public void ReturnsTrueWhenValueIsNullOrWhiteSpace()
            {
                StringExtensions.IsNullOrWhiteSpace(null).Should().BeTrue();
                StringExtensions.IsNullOrWhiteSpace("").Should().BeTrue();
                StringExtensions.IsNullOrWhiteSpace(" ").Should().BeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenValueIsNotNullOrWhiteSpace()
            {
                StringExtensions.IsNullOrWhiteSpace("a").Should().BeFalse();
                StringExtensions.IsNullOrWhiteSpace(" a").Should().BeFalse();
                StringExtensions.IsNullOrWhiteSpace("a ").Should().BeFalse();
            }
        }
    }
}
