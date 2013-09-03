using System;
using System.Collections.Generic;
using System.IO;
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
            public void Argument_delimiter_CannotBeEmpty()
            {
                ShouldThrow_ArgumentException_For_delimiter_IsWhitespace(string.Empty);
            }

            [TestMethod]
            public void Argument_delimiter_CannotBeWhitespace()
            {
                ShouldThrow_ArgumentException_For_delimiter_IsWhitespace(" ");
            }

            [TestMethod]
            public void Argument_delimiter_CannotLongerThanOneCharacter()
            {
                "fake value".Invoking(x => x.GetValuesBetween("12"))
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("Value cannot be longer than 1 character.\r\nParameter name: delimiter");
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
                string value = "'the' 'quick' brown 'fox' jumped 'over the' lazy dog";
                string delimiter = "'";

                // When
                IEnumerable<string> values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().Equal(new string[] { "the", "quick", "fox", "over the" });
            }

            [TestMethod]
            public void ReturnsValuesWhenStringDoesNotStartsDelimiter()
            {
                // Given
                string value = "the 'quick' brown 'fox' jumped 'over the' lazy dog";
                string delimiter = "'";

                // When
                IEnumerable<string> values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().Equal(new string[] { "quick", "fox", "over the" });
            }

            [TestMethod]
            public void ReturnsZeroValuesWhen_value_DoesNotHaveAnyValues()
            {
                // Given
                string value = "the quick brown fox";
                string delimiter = "'";

                // When
                IEnumerable<string> values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().BeEmpty();
            }

            [TestMethod]
            public void ReturnsZeroValuesWhen_value_IsNull()
            {
                ReturnsZeroValuesWhenValueIs(null);
            }

            [TestMethod]
            public void ReturnsZeroValuesWhen_value_IsEmpty()
            {
                ReturnsZeroValuesWhenValueIs(string.Empty);
            }

            [TestMethod]
            public void ReturnsZeroValuesWhen_value_CanBeWhitespace()
            {
                ReturnsZeroValuesWhenValueIs(" ");
            }

            private void ReturnsZeroValuesWhenValueIs(string value)
            {
                // Given
                string delimiter = "'";

                // When
                IEnumerable<string> values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().BeEmpty();
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

        [TestClass]
        public class WriteLines
        {
            [TestMethod]
            public void WritesUntrimmedLinesTo_textWriter_When_trimLines_IsFalse()
            {
                // Given
                var value = "line 1\r\n line 2 \r\nline 3";
                var textWriter = new StringWriter();
                var trimLines = false;

                // When
                value.WriteLines(textWriter, trimLines);

                // Then
                textWriter.ToString().Should().Be(value+"\r\n");
            }

            [TestMethod]
            public void WritesTrimmedLinesTo_textWriter_When_trimLines_IsTrue()
            {
                // Given
                var value = "line 1\r\n line 2 \r\nline 3";
                var textWriter = new StringWriter();
                var trimLines = true;

                // When
                value.WriteLines(textWriter, trimLines);

                // Then
                textWriter.ToString().Should().Be("line 1\r\nline 2\r\nline 3\r\n");
            }

            [TestMethod]
            public void DoesNotThrowWhen_value_IsNull()
            {
                // Given
                string value = null;
                var textWriter = new StringWriter();
                var trimLines = true;

                // When
                value.WriteLines(textWriter, trimLines);

                // Then
                textWriter.ToString().Should().Be("");
            }
        }
    }
}
