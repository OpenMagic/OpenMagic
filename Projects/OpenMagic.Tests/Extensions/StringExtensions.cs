using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenMagic.Tests.Extensions
{
    [TestClass]
    public class StringExtensions
    {
        [TestClass]
        public class IsNullOrWhiteSpace
        {

            [TestMethod]
            public void ReturnsTrueWhenValueIsNullOrWhiteSpace()
            {
                OpenMagic.Extensions.StringExtensions.IsNullOrWhiteSpace(null).Should().BeTrue();
                OpenMagic.Extensions.StringExtensions.IsNullOrWhiteSpace("").Should().BeTrue();
                OpenMagic.Extensions.StringExtensions.IsNullOrWhiteSpace(" ").Should().BeTrue();
            }

            [TestMethod]
            public void ReturnsFalseWhenValueIsNotNullOrWhiteSpace()
            {
                OpenMagic.Extensions.StringExtensions.IsNullOrWhiteSpace("a").Should().BeFalse();
                OpenMagic.Extensions.StringExtensions.IsNullOrWhiteSpace(" a").Should().BeFalse();
                OpenMagic.Extensions.StringExtensions.IsNullOrWhiteSpace("a ").Should().BeFalse();
            }
        }
    }
}
