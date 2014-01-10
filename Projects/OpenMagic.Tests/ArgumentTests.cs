using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestMagic;

namespace OpenMagic.Tests
{
    public class ArgumentsTests
    {
        [TestClass]
        public class Must
        {
            [TestMethod]
            public void ShouldThrowNotArgumentExceptionWhenAssertionResultIsTrue()
            {
                // Given
                var argument = 1;
                var exceptionMessage = "Value must be 1.";
                var paramName = "argument";
                                
                // When
                Action action = () => argument.Must(argument == 1, exceptionMessage, paramName);

                // Then
                action.ShouldNotThrow<ArgumentException>();
            }

            [TestMethod]
            public void ShouldThrowArgumentExceptionWhenAssertionResultIsFalse()
            {
                // Given
                var argument = 0;
                var exceptionMessage = "Value must be 1.";
                var paramName = "argument";

                // When
                Action action = () => argument.Must(argument == 1, exceptionMessage, paramName);

                // Then
                action.ShouldThrow<ArgumentException>().WithMessage("Value must be 1.\r\nParameter name: argument");
            }
        }

        [TestClass]
        public class MustNotBeNull
        {
            [TestMethod]
            public void ShouldReturn_parameterValue_When_parameterValue_IsNotNull()
            {
                // Given
                var parameterValue = 1;

                // When
                var value = Argument.MustNotBeNull(parameterValue, "parameterValue");

                // Then
                value.Should().Be(parameterValue);
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWithParameterNameWhenParameterValueIsNull()
            {
                // Given
                const string parameterName = "fakeParameterName";

                // When
                Action action = () => Argument.MustNotBeNull<Exception>(param: null, paramName: parameterName);

                // Then
                action.ShouldThrow<ArgumentNullException>().And.ParamName.Should().Be(parameterName);
            }
        }

        [TestClass]
        public class MustNotBeNull_AsExtensionMethod
        {
            [TestMethod]
            public void ShouldReturn_parameterValue_When_parameterValue_IsNotNull()
            {
                // Given
                var parameterValue = 1;

                // When
                var value = parameterValue.MustNotBeNull("parameterValue");

                // Then
                value.Should().Be(parameterValue);
            }
        }

        [TestClass]
        public class MustNotBeNullOrEmpty
        {
            [TestMethod]
            public void ShouldReturn_parameterValue_When_parameterValue_IsNotNull()
            {
                // Given
                List<int> parameterValue = new List<int>(new int[] { 1, 2 });

                // When
                var value = Argument.MustNotBeNullOrEmpty(parameterValue, "fakeParamName");

                // Then
                value.Should().BeEquivalentTo(parameterValue);
            }

            [TestMethod]
            public void ShouldThrowArgumentExceptionWhen_param_IsEmpty()
            {
                // Given
                List<int> parameterValue = new List<int>();

                // When
                Action action = () => Argument.MustNotBeNullOrEmpty<int>(parameterValue, "fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("Value cannot be empty.\r\nParameter name: fakeParamName");
            }
        }

        [TestClass]
        public class MustNotBeNullOrEmpty_AsExtensionMethod
        {
            [TestMethod]
            public void ShouldReturn_param_When_param_IsNotNull()
            {
                // Given
                List<int> param = new List<int>(new int[] { 1, 2 });

                // When
                var value = param.MustNotBeNullOrEmpty("fakeParamName");

                // Then
                value.Should().BeEquivalentTo(param);
            }

            [TestMethod]
            public void ShouldThrowArgumentExceptionWhen_param_IsEmpty()
            {
                // Given
                List<int> param = new List<int>();

                // When
                Action action = () => param.MustNotBeNullOrEmpty<int>("fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("Value cannot be empty.\r\nParameter name: fakeParamName");
            }
        }

        [TestClass]
        public class MustNotBeNullOrWhiteSpace
        {
            [TestMethod]
            public void ShouldReturn_param_When_param_IsNotWhitespace()
            {
                // Given
                var param = "any string";

                // When
                var value = Argument.MustNotBeNullOrWhiteSpace(param, "param");

                // Then
                value.Should().Be(param);
            }

            [TestMethod]
            public void ShouldThrowArgumentExceptionWhen_param_IsWhiteSpace()
            {
                // Given
                string param = "";

                // When
                Action action = () => Argument.MustNotBeNullOrWhiteSpace(param, "fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("Value cannot be whitespace.\r\nParameter name: fakeParamName");
            }
        }

        [TestClass]
        public class MustNotBeNullOrWhiteSpace_AsExtensionMethod
        {
            [TestMethod]
            public void ShouldReturn_param_When_param_IsNotWhitespace()
            {
                // Given
                var param = "any string";

                // When
                var value = param.MustNotBeNullOrWhiteSpace("param");

                // Then
                value.Should().Be(param);
            }

            [TestMethod]
            public void ShouldThrowArgumentExceptionWhen_param_IsWhiteSpace()
            {
                // Given
                string param = "";

                // When
                Action action = () => param.MustNotBeNullOrWhiteSpace("fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentException>()
                    .WithMessage("Value cannot be whitespace.\r\nParameter name: fakeParamName");
            }
        }
    }
}
