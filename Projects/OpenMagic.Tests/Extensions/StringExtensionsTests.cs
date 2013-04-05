using System.Linq;
using System.Text;
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

        [TestClass]
        public class ToLines
        {
            [TestMethod]
            public void ReturnsLinesWhenValueHasLines()
            {
                // Given
                var sb = new StringBuilder();

                sb.AppendLine("line 1");
                sb.AppendLine("line 2 with trailing space ");
                sb.AppendLine();
                sb.AppendLine(" line 4 with leading space");
                sb.AppendLine();

                // When
                var lines = sb.ToString().ToLines();

                // Then
                lines.Count().Should().Be(5);
                lines.ElementAt(0).Should().Be("line 1");
                lines.ElementAt(1).Should().Be("line 2 with trailing space ");
                lines.ElementAt(2).Should().Be("");
                lines.ElementAt(3).Should().Be(" line 4 with leading space");
                lines.ElementAt(4).Should().Be("");
            }

            [TestMethod]
            public void ReturnsTrimmedLinesWhenValueHasLinesAndTrimLinesIsTrue()
            {
                // Given
                var sb = new StringBuilder();

                sb.AppendLine("line 1");
                sb.AppendLine("line 2 with trailing space trimmed ");
                sb.AppendLine();
                sb.AppendLine(" line 4 with leading space trimmed");
                sb.AppendLine();

                // When
                var lines = sb.ToString().ToLines(true);

                // Then
                lines.Count().Should().Be(5);
                lines.ElementAt(0).Should().Be("line 1");
                lines.ElementAt(1).Should().Be("line 2 with trailing space trimmed");
                lines.ElementAt(2).Should().Be("");
                lines.ElementAt(3).Should().Be("line 4 with leading space trimmed");
                lines.ElementAt(4).Should().Be("");
            }

            [TestMethod]
            public void ReturnsZeroLinesWhenValueIsEmptyString()
            {
                // Given
                string value = "";

                // When
                var lines = value.ToLines();

                // Then
                lines.Count().Should().Be(0);
            }

            [TestMethod]
            public void ReturnsZeroLinesWhenValueIsNull()
            {
                // Given
                string value = null;

                // When
                var lines = value.ToLines();

                // Then
                lines.Count().Should().Be(0);
            }
        }
    }
}
