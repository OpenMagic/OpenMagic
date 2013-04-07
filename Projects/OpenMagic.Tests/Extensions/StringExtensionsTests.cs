using System;
using System.Collections.Generic;
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
        public class GetValuesBetween
        {
            [TestMethod]
            public void Argument_delimiter_CannotBeNull()
            {
                ShouldThrow_ArgumentNullException_For_delimiter(null);
            }

            [TestMethod]
            public void Argument_delimiter_CannotBeEmpty()
            {
                ShouldThrow_ArgumentException_For_delimiter_IsWhitespace(string.Empty);
            }

            [TestMethod]
            public void Argument_delimiter_CannotBeWhitespace()
            {
                ShouldThrow_ArgumentException_For_delimiter_IsWhitespace(" ");
            }

            private void ShouldThrow_ArgumentNullException_For_delimiter(string delimiter)
            {
                "fake value".Invoking(x => x.GetValuesBetween(delimiter))
                    .ShouldThrow<ArgumentNullException>()
                    .WithMessage("Value cannot be null.\r\nParameter name: delimiter");
            }

            private void ShouldThrow_ArgumentException_For_delimiter_IsWhitespace(string delimiter)
            {
                "fake value".Invoking(x => x.GetValuesBetween(delimiter))
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("Value cannot be whitespace.\r\nParameter name: delimiter");
            }

            [TestMethod]
            public void ReturnsValuesWhenStringStartsWithDelimiter()
            {
                // Given

                // When

                // Then
                Assert.Inconclusive("todo");
            }

            [TestMethod]
            public void ReturnsValuesWhenStringDoesNotStartsDelimiter()
            {
                // Given
                string value = "the 'quick' brown 'fox'";
                string delimiter = "'";

                delimiter = string.Empty;

                // When
                IEnumerable<string> values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().Equal(new string[] { "quick", "fox" });
            }

            [TestMethod]
            public void ReturnsZeroValuesWhen_value_DoesNotHaveAnyValues()
            {
                // Given

                // When

                // Then
                Assert.Inconclusive("todo");
            }

            [TestMethod]
            public void ReturnsZeroValuesWhen_value_IsNull()
            {
                // Given

                // When

                // Then
                Assert.Inconclusive("todo");
            }

            [TestMethod]
            public void ReturnsZeroValuesWhen_value_CanBeEmpty()
            {
                // Given

                // When

                // Then
                Assert.Inconclusive("todo");
            }

            [TestMethod]
            public void ReturnsZeroValuesWhen_value_CanBeWhitespace()
            {
                // Given

                // When

                // Then
                Assert.Inconclusive("todo");
            }

        }

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
