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
            public void ShouldReturn_param_When_param_IsNotNull()
            {
                // Given
                var param = 1;

                // When
                var value = Argument.MustNotBeNull(param, "param");

                // Then
                value.Should().Be(param);
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhen_param_IsNull()
            {
                // Given
                Exception param = null;

                // When
                Action action = () => Argument.MustNotBeNull(param, "fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentNullException>()
                    .WithMessage("Value cannot be null.\r\nParameter name: fakeParamName");
            }
        }

        [TestClass]
        public class MustNotBeNull_AsExtensionMethod
        {
            [TestMethod]
            public void ShouldReturn_param_When_param_IsNotNull()
            {
                // Given
                var param = 1;

                // When
                var value = param.MustNotBeNull("param");

                // Then
                value.Should().Be(param);
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhen_param_IsNull()
            {
                // Given
                Exception param = null;

                // When
                Action action = () => param.MustNotBeNull("fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentNullException>()
                    .WithMessage("Value cannot be null.\r\nParameter name: fakeParamName");
            }
        }

        [TestClass]
        public class MustNotBeNullOrEmpty
        {
            [TestMethod]
            public void ShouldReturn_param_When_param_IsNotNull()
            {
                // Given
                List<int> param = new List<int>(new int[] { 1, 2 });

                // When
                var value = Argument.MustNotBeNullOrEmpty(param, "fakeParamName");

                // Then
                value.Should().BeEquivalentTo(param);
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhen_param_IsNull()
            {
                // Given
                List<int> param = null;

                // When
                Action action = () => Argument.MustNotBeNullOrEmpty<int>(param, "fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentNullException>()
                    .WithMessage("Value cannot be null.\r\nParameter name: fakeParamName");
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhen_param_IsEmpty()
            {
                // Given
                List<int> param = new List<int>();

                // When
                Action action = () => Argument.MustNotBeNullOrEmpty<int>(param, "fakeParamName");

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
            public void ShouldThrowArgumentNullExceptionWhen_param_IsNull()
            {
                // Given
                List<int> param = null;

                // When
                Action action = () => param.MustNotBeNullOrEmpty<int>("fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentNullException>()
                    .WithMessage("Value cannot be null.\r\nParameter name: fakeParamName");
            }

            [TestMethod]
            public void ShouldThrowArgumentNullExceptionWhen_param_IsEmpty()
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
            public void ShouldThrowArgumentNullExceptionWhen_param_IsNull()
            {
                // Given
                string param = null;

                // When
                Action action = () => Argument.MustNotBeNullOrWhiteSpace(param, "fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentNullException>()
                    .WithMessage("Value cannot be null.\r\nParameter name: fakeParamName");
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
            public void ShouldThrowArgumentNullExceptionWhen_param_IsNull()
            {
                // Given
                string param = null;

                // When
                Action action = () => param.MustNotBeNullOrWhiteSpace("fakeParamName");

                // Then
                action
                    .ShouldThrow<ArgumentNullException>()
                    .WithMessage("Value cannot be null.\r\nParameter name: fakeParamName");
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
