using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OpenMagic.Exceptions;
using OpenMagic.Tests.TestHelpers;
using Xunit;

namespace OpenMagic.Tests
{
    public class ArgumentsTests
    {
        public class Must
        {
            [Fact]
            public void ShouldThrowNotArgumentExceptionWhenAssertionResultIsTrue()
            {
                // Given
                const int argument = 1;
                const string exceptionMessage = "Value must be 1.";
                const string paramName = "argument";

                // When
                Action action = () => argument.Must(argument == 1, exceptionMessage, paramName);

                // Then
                action.Should().NotThrow<ArgumentException>();
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhenAssertionResultIsFalse()
            {
                // Given
                const int argument = 0;
                const string exceptionMessage = "Value must be 1.";
                const string paramName = "argument";

                // When
                Action action = () => argument.Must(argument == 1, exceptionMessage, paramName);

                // Then
                action.Should().Throw<ArgumentException>().WithMessage("Value must be 1. (Parameter 'argument')");
            }
        }

        public class MustBeAnEmailAddress
        {
            [Fact]
            public void Should_Throw_ArgumentNullOrWhiteSpaceException_When_emailAddress_Is_Null()
            {
                // When
                // ReSharper disable once AssignNullToNotNullAttribute because this is a test for the exception
                Action action = () => ((string)null).MustBeAnEmailAddress("dummy");

                // Then
                action.Should().Throw<ArgumentNullOrWhiteSpaceException>().And.ParamName.Should().Be("emailAddress");
            }

            [Fact]
            public void Should_Throw_ArgumentNullOrWhiteSpaceException_When_emailAddress_Is_WhiteSpace()
            {
                // Given
                const string paramName = "dummy";

                // When
                Action action = () => "".MustBeAnEmailAddress(paramName);

                // Then
                var exception = action.Should().Throw<ArgumentException>().Subject.Single();
                exception.ParamName.Should().Be("emailAddress");
                exception.Message.Should().Be("emailAddress".ArgumentExceptionMessage("Value cannot be null or whitespace."));
            }

            [Fact]
            public void Should_Throw_ArgumentException_When_emailAddress_Is_Invalid()
            {
                // Given
                const string paramName = "dummy";

                // When
                Action action = () => "tim-26tp.com".MustBeAnEmailAddress(paramName);

                // Then
                var exception = action.Should().Throw<ArgumentException>().Subject.Single();

                exception.ParamName.Should().Be(paramName);
                exception.Message.Should().Be(paramName.ArgumentExceptionMessage("Value is not a valid email address."));
            }

            [Fact]
            public void Should_Return_emailAddress_When_emailAddress_Is_Valid()
            {
                // Given
                const string emailAddress = "tim@26tp.com";
                const string paramName = "dummy";

                // When
                var result = emailAddress.MustBeAnEmailAddress(paramName);

                // Then
                result.Should().Be(emailAddress);
            }
        }

        public class MustNotBeNull
        {
            [Fact]
            public void ShouldReturn_parameterValue_When_parameterValue_IsNotNull()
            {
                // Given
                const int parameterValue = 1;

                // When
                var value = parameterValue.MustNotBeNull("parameterValue");

                // Then
                value.Should().Be(parameterValue);
            }

            [Fact]
            public void ShouldThrowArgumentNullExceptionWithParameterNameWhenParameterValueIsNull()
            {
                // Given
                const string parameterName = "fakeParameterName";

                // When
                Action action = () => Argument.MustNotBeNull<Exception>(null, parameterName);

                // Then
                action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be(parameterName);
            }
        }

        public class MustNotBeNullOrEmpty
        {
            [Fact]
            public void ShouldReturn_parameterValue_When_parameterValue_IsNotNull()
            {
                // Given
                var parameterValue = new List<int>([1, 2]);

                // When
                var value = parameterValue.MustNotBeNullOrEmpty("fakeParamName");

                // Then
                value.Should().BeEquivalentTo(parameterValue);
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhen_param_IsEmpty()
            {
                // Given
                // ReSharper disable once CollectionNeverUpdated.Local because this is a test for the exception
                var parameterValue = new List<int>();

                // When
                Action action = () => parameterValue.MustNotBeNullOrEmpty("fakeParamName");

                // Then
                action
                    .Should().Throw<ArgumentException>()
                    .WithMessage("fakeParamName".ArgumentExceptionMessage("Value cannot be empty."));
            }
        }

        // ReSharper disable once InconsistentNaming
        public class MustNotBeNullOrEmpty_AsExtensionMethod
        {
            [Fact]
            public void ShouldReturn_param_When_param_IsNotNull()
            {
                // Given
                var param = new List<int>([1, 2]);

                // When
                var value = param.MustNotBeNullOrEmpty("fakeParamName");

                // Then
                value.Should().BeEquivalentTo(param);
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhen_param_IsEmpty()
            {
                // Given
                // ReSharper disable once CollectionNeverUpdated.Local because this is a test for the exception
                var param = new List<int>();

                // When
                Action action = () => param.MustNotBeNullOrEmpty("fakeParamName");

                // Then
                action
                    .Should().Throw<ArgumentException>()
                    .WithMessage("fakeParamName".ArgumentExceptionMessage("Value cannot be empty."));
            }
        }

        public class MustNotBeNullOrWhiteSpace
        {
            [Fact]
            public void ShouldReturn_param_When_param_IsNotWhitespace()
            {
                // Given
                const string param = "any string";

                // When
                var value = param.MustNotBeNullOrWhiteSpace("param");

                // Then
                value.Should().Be(param);
            }

            [Fact]
            public void ShouldThrowArgumentNullOrWhiteSpaceExceptionWhen_param_IsWhiteSpace()
            {
                // Given
                const string param = " ";

                // When
                Action action = () => param.MustNotBeNullOrWhiteSpace("fakeParamName");

                // Then
                action
                    .Should().Throw<ArgumentNullOrWhiteSpaceException>()
                    .WithMessage("fakeParamName".ArgumentExceptionMessage("Value cannot be null or whitespace."));
            }
        }

        // ReSharper disable once InconsistentNaming
        public class MustNotBeNullOrWhiteSpace_AsExtensionMethod
        {
            [Fact]
            public void ShouldReturn_param_When_param_IsNotWhitespace()
            {
                // Given
                const string param = "any string";

                // When
                var value = param.MustNotBeNullOrWhiteSpace("param");

                // Then
                value.Should().Be(param);
            }

            [Fact]
            public void ShouldThrowArgumentNullOrWhiteSpaceExceptionWhen_param_IsWhiteSpace()
            {
                // Given
                const string param = " ";

                // When
                Action action = () => param.MustNotBeNullOrWhiteSpace("fakeParamName");

                // Then
                action
                    .Should().Throw<ArgumentNullOrWhiteSpaceException>()
                    .WithMessage("fakeParamName".ArgumentExceptionMessage("Value cannot be null or whitespace."));
            }
        }

        // ReSharper disable once InconsistentNaming
        public class MustNotBeNull_AsExtensionMethod
        {
            [Fact]
            public void ShouldReturn_parameterValue_When_parameterValue_IsNotNull()
            {
                // Given
                const int parameterValue = 1;

                // When
                var value = parameterValue.MustNotBeNull("parameterValue");

                // Then
                value.Should().Be(parameterValue);
            }
        }

        public class MustBeGreaterThan
        {
            [Fact]
            public void ShouldReturn_param_When_param_IsGreaterThan_greaterThan()
            {
                // Given
                const int param = 1;
                const int greaterThan = 0;

                // When
                var result = param.MustBeGreaterThan(greaterThan, "fakeParamName");

                // Then
                result.Should().Be(param);
            }

            [Fact]
            public void ShouldThrow_ArgumentOutOfRangeException_When_param_IsEqualTo_greaterThan()
            {
                // Given
                const int param = 1;
                const int greaterThan = 1;

                // When
                Action action = () => param.MustBeGreaterThan(greaterThan, "fakeParamName");

                // Then
                action.Should().Throw<ArgumentOutOfRangeException>();
            }

            [Fact]
            public void ShouldThrow_ArgumentOutOfRangeException_When_param_IsLessThan_greaterThan()
            {
                // Given
                const int param = 0;
                const int greaterThan = 1;

                // When
                Action action = () => param.MustBeGreaterThan(greaterThan, "fakeParamName");

                // Then
                action.Should().Throw<ArgumentOutOfRangeException>();
            }
        }

        public class MustBeGreaterThanOrEqualTo
        {
            [Fact]
            public void ShouldReturn_param_When_param_IsGreaterThan_greaterThanOrEqualTo()
            {
                // Given
                const int highValue = 1;
                const int lowValue = 0;

                // When
                var result = highValue.MustBeGreaterThanOrEqualTo(lowValue, "fakeParamName");

                // Then
                result.Should().Be(highValue);
            }

            [Fact]
            public void ShouldReturn_param_When_param_IsEqualTo_greaterThanOrEqualTo()
            {
                // Given
                const int equalToA = 1;
                const int equalToB = 1;

                // When
                var result = equalToA.MustBeGreaterThanOrEqualTo(equalToB, "fakeParamName");

                // Then
                result.Should().Be(equalToA);
            }

            [Fact]
            public void ShouldThrow_ArgumentOutOfRangeException_When_param_IsLessThan_greaterThan()
            {
                // Given
                const int lowValue = 0;
                const int highValue = 1;

                // When
                Action action = () => lowValue.MustBeGreaterThanOrEqualTo(highValue, "fakeParamName");

                // Then
                action.Should().Throw<ArgumentOutOfRangeException>();
            }

            [Fact]
            public void ShouldReturn_param_When_param_IsGreaterThan_greaterThanOrEqualTo_UsingDates()
            {
                // Given
                var highValue = DateTime.Today;
                var lowValue = highValue.AddDays(-1);

                // When
                var result = highValue.MustBeGreaterThanOrEqualTo(lowValue, "fakeParamName");

                // Then
                result.Should().Be(highValue);
            }

            [Fact]
            public void ShouldReturn_param_When_param_IsEqualTo_greaterThanOrEqualTo_UsingDates()
            {
                // Given
                var equalToA = DateTime.Today;
                // ReSharper disable once InlineTemporaryVariable
                var equalToB = equalToA;

                // When
                var result = equalToA.MustBeGreaterThanOrEqualTo(equalToB, "fakeParamName");

                // Then
                result.Should().Be(equalToA);
            }

            [Fact]
            public void ShouldThrow_ArgumentOutOfRangeException_When_param_IsLessThan_greaterThan_UsingDates()
            {
                // Given
                var lowValue = DateTime.Today;
                var highValue = lowValue.AddDays(1);

                // When
                Action action = () => lowValue.MustBeGreaterThanOrEqualTo(highValue, "fakeParamName");

                // Then
                action.Should().Throw<ArgumentOutOfRangeException>();
            }
        }
    }
}