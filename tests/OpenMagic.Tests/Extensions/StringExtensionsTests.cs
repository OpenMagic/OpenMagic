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
                    .Should().Throw<ArgumentException>()
                    .WithMessage("Value cannot be longer than 1 character.\r\nParameter name: delimiter");
            }

            private void ShouldThrow_ArgumentException_For_delimiter_IsWhitespace(string delimiter)
            {
                "fake value".Invoking(x => x.GetValuesBetween(delimiter))
                    .Should().Throw<ArgumentException>()
                    .WithMessage("Value cannot be whitespace.\r\nParameter name: delimiter");
            }

            [Fact]
            public void ReturnsValuesWhenStringStartsWithDelimiter()
            {
                // Given
                const string value = "'the' 'quick' brown 'fox' jumped 'over the' lazy dog";
                const string delimiter = "'";

                // When
                var values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().Equal(new[] { "the", "quick", "fox", "over the" });
            }

            [Fact]
            public void ReturnsValuesWhenStringDoesNotStartsDelimiter()
            {
                // Given
                const string value = "the 'quick' brown 'fox' jumped 'over the' lazy dog";
                const string delimiter = "'";

                // When
                var values = value.GetValuesBetween(delimiter);

                // Then
                values.Should().Equal(new[] { "quick", "fox", "over the" });
            }

            [Fact]
            public void ReturnsZeroValuesWhen_value_DoesNotHaveAnyValues()
            {
                // Given
                const string value = "the quick brown fox";
                const string delimiter = "'";

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
                const string delimiter = "'";

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
                var lines = sb.ToString().ToLines().ToArray();

                // Then
                lines.Length.Should().Be(5);
                lines[0].Should().Be("line 1");
                lines[1].Should().Be("line 2 with trailing space ");
                lines[2].Should().Be("");
                lines[3].Should().Be(" line 4 with leading space");
                lines[4].Should().Be("");
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
                var lines = sb.ToString().ToLines(true).ToArray();

                // Then
                lines.Length.Should().Be(5);
                lines[0].Should().Be("line 1");
                lines[1].Should().Be("line 2 with trailing space trimmed");
                lines[2].Should().Be("");
                lines[3].Should().Be("line 4 with leading space trimmed");
                lines[4].Should().Be("");
            }

            [Fact]
            public void ReturnsZeroLinesWhenValueIsEmptyString()
            {
                // Given
                const string value = "";

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
                // ReSharper disable once ExpressionIsAlwaysNull
                var lines = value.ToLines();

                // Then
                lines.Count().Should().Be(0);
            }
        }

        public class ToNameValueCollection
        {
            [Fact]
            public void ReturnsCollectionOfNameAndValueItems()
            {
                // Given
                const string convert = "a=1;b=2";
                
                // When
                var result = convert.ToNameValueCollection();

                // Then
                result.Count.Should().Be(2);
                result["a"].Should().Be("1");
                result["b"].Should().Be("2");
            }
        }
        public class WriteLines
        {
            [Fact]
            public void WritesUntrimmedLinesTo_textWriter_When_trimLines_IsFalse()
            {
                // Given
                const string value = "line 1\r\n line 2 \r\nline 3";
                var textWriter = new StringWriter();
                const bool trimLines = false;

                // When
                // ReSharper disable once RedundantArgumentDefaultValue
                value.WriteLines(textWriter, trimLines);

                // Then
                textWriter.ToString().Should().Be(value + "\r\n");
            }

            [Fact]
            public void WritesTrimmedLinesTo_textWriter_When_trimLines_IsTrue()
            {
                // Given
                const string value = "line 1\r\n line 2 \r\nline 3";
                var textWriter = new StringWriter();
                const bool trimLines = true;

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
                const bool trimLines = true;

                // When
                // ReSharper disable once ExpressionIsAlwaysNull
                value.WriteLines(textWriter, trimLines);

                // Then
                textWriter.ToString().Should().Be("");
            }
        }
    }
}