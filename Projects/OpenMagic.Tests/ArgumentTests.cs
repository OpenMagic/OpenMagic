using System;
using System.Collections.Generic;
using FluentAssertions;
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
                action.ShouldNotThrow<ArgumentException>();
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
                action.ShouldThrow<ArgumentException>().WithMessage("Value must be 1.\r\nParameter name: argument");
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
                action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be(parameterName);
            }
        }

        public class MustNotBeNullOrEmpty
        {
            [Fact]
            public void ShouldReturn_parameterValue_When_parameterValue_IsNotNull()
            {
                // Given
                var parameterValue = new List<int>(new[] { 1, 2 });

                // When
                var value = parameterValue.MustNotBeNullOrEmpty("fakeParamName");

                // Then
                value.Should().BeEquivalentTo(parameterValue);
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhen_param_IsEmpty()
            {
                // Given
                var parameterValue = new List<int>();

                // When
                Action action = () => parameterValue.MustNotBeNullOrEmpty("fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("Value cannot be empty.\r\nParameter name: fakeParamName");
            }
        }

        // ReSharper disable once InconsistentNaming
        public class MustNotBeNullOrEmpty_AsExtensionMethod
        {
            [Fact]
            public void ShouldReturn_param_When_param_IsNotNull()
            {
                // Given
                var param = new List<int>(new[] { 1, 2 });

                // When
                var value = param.MustNotBeNullOrEmpty("fakeParamName");

                // Then
                value.Should().BeEquivalentTo(param);
            }

            [Fact]
            public void ShouldThrowArgumentExceptionWhen_param_IsEmpty()
            {
                // Given
                var param = new List<int>();

                // When
                Action action = () => param.MustNotBeNullOrEmpty("fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("Value cannot be empty.\r\nParameter name: fakeParamName");
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
            public void ShouldThrowArgumentExceptionWhen_param_IsWhiteSpace()
            {
                // Given
                const string param = "";

                // When
                Action action = () => param.MustNotBeNullOrWhiteSpace("fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("Value cannot be whitespace.\r\nParameter name: fakeParamName");
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
            public void ShouldThrowArgumentExceptionWhen_param_IsWhiteSpace()
            {
                // Given
                const string param = "";

                // When
                Action action = () => param.MustNotBeNullOrWhiteSpace("fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("Value cannot be whitespace.\r\nParameter name: fakeParamName");
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
                action.ShouldThrow<ArgumentOutOfRangeException>();
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
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }
    }
}