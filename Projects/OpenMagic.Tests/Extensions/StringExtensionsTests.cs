using System;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using OpenMagic.Extensions;
using Xunit;

namespace OpenMagic.Tests.Extensions
{
    public class StringExtensionsTests
    {
        public class GetValuesBetween
        {
            [Fact]
            public void Argument_delimiter_CannotBeEmpty()
            {
                ShouldThrow_ArgumentException_For_delimiter_IsWhitespace(string.Empty);
            }

            [Fact]
            public void Argument_delimiter_CannotBeWhitespace()
            {
                ShouldThrow_ArgumentException_For_delimiter_IsWhitespace(" ");
            }

            [Fact]
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

            [Fact]
            public void ReturnsValuesWhenStringStartsWithDelimiter()
            {
                // Given
                var value = "'the' 'quick' brown 'fox' jumped 'over the' lazy dog";
                var delimiter = "'";

                // When
                var values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().Equal(new[] {"the", "quick", "fox", "over the"});
            }

            [Fact]
            public void ReturnsValuesWhenStringDoesNotStartsDelimiter()
            {
                // Given
                var value = "the 'quick' brown 'fox' jumped 'over the' lazy dog";
                var delimiter = "'";

                // When
                var values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().Equal(new[] {"quick", "fox", "over the"});
            }

            [Fact]
            public void ReturnsZeroValuesWhen_value_DoesNotHaveAnyValues()
            {
                // Given
                var value = "the quick brown fox";
                var delimiter = "'";

                // When
                var values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().BeEmpty();
            }

            [Fact]
            public void ReturnsZeroValuesWhen_value_IsNull()
            {
                ReturnsZeroValuesWhenValueIs(null);
            }

            [Fact]
            public void ReturnsZeroValuesWhen_value_IsEmpty()
            {
                ReturnsZeroValuesWhenValueIs(string.Empty);
            }

            [Fact]
            public void ReturnsZeroValuesWhen_value_CanBeWhitespace()
            {
                ReturnsZeroValuesWhenValueIs(" ");
            }

            private void ReturnsZeroValuesWhenValueIs(string value)
            {
                // Given
                var delimiter = "'";

                // When
                var values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().BeEmpty();
            }
        }

        public class IsNullOrWhiteSpace
        {
            [Fact]
            public void ReturnsTrueWhenValueIsNullOrWhiteSpace()
            {
                StringExtensions.IsNullOrWhiteSpace(null).Should().BeTrue();
                "".IsNullOrWhiteSpace().Should().BeTrue();
                " ".IsNullOrWhiteSpace().Should().BeTrue();
            }

            [Fact]
            public void ReturnsFalseWhenValueIsNotNullOrWhiteSpace()
            {
                "a".IsNullOrWhiteSpace().Should().BeFalse();
                " a".IsNullOrWhiteSpace().Should().BeFalse();
                "a ".IsNullOrWhiteSpace().Should().BeFalse();
            }
        }

        public class ToLines
        {
            [Fact]
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

            [Fact]
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

            [Fact]
            public void ReturnsZeroLinesWhenValueIsEmptyString()
            {
                // Given
                var value = "";

                // When
                var lines = value.ToLines();

                // Then
                lines.Count().Should().Be(0);
            }

            [Fact]
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

        public class WriteLines
        {
            [Fact]
            public void WritesUntrimmedLinesTo_textWriter_When_trimLines_IsFalse()
            {
                // Given
                var value = "line 1\r\n line 2 \r\nline 3";
                var textWriter = new StringWriter();
                var trimLines = false;

                // When
                value.WriteLines(textWriter, trimLines);

                // Then
                textWriter.ToString().Should().Be(value + "\r\n");
            }

            [Fact]
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

            [Fact]
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