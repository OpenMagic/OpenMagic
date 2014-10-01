using System;
using FluentAssertions;
using OpenMagic.Assertions;
using Xunit;

namespace OpenMagic.Tests.Assertions
{
    public class MustAssertionsTests
    {
        protected const string Message = "dummy exception message with param {0}";
        protected const int ParamValue = 1;
        protected const string ExpectedMessage = "dummy exception message with param 1";

        public class MustBe : MustAssertionsTests
        {
            [Fact]
            public void ShouldThrowAssertionExceptionWhenAssertionIsNotEqual()
            {
                // Given
                const int value = 1;

                // When
                Action action = () => value.MustBe(2, Message);

                // Then
                action.ShouldThrow<AssertionException>()
                    .WithMessage(Message);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenAssertionIsEqual()
            {
                // Given
                const int value = 1;

                // When
                Action action = () => value.MustBe(1, Message, ParamValue);

                // Then
                action.ShouldNotThrow<Exception>();
            }

            [Fact]
            public void ShouldThrowAssertionExceptionWhenAssertionIsNotEqualAnd_params_ParameterIsUsed()
            {
                // Given
                const int value = 1;

                // When
                Action action = () => value.MustBe(2, Message, ParamValue);

                // Then
                action.ShouldThrow<AssertionException>()
                    .WithMessage(ExpectedMessage);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenAssertionIsEqualAnd_params_ParameterIsUsed()
            {
                // Given
                const int value = 1;

                // When
                Action action = () => value.MustBe(1, Message, ParamValue);

                // Then
                action.ShouldNotThrow<Exception>();
            }
        }

        public class MustBeFalse : MustAssertionsTests
        {
            [Fact]
            public void ShouldThrowAssertionExceptionWhenValueIsTrue()
            {
                // Given
                const bool value = true;

                // When
                Action action = () => value.MustBeFalse(Message);

                // Then
                action.ShouldThrow<AssertionException>()
                    .WithMessage(Message);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsFalse()
            {
                // Given
                const bool value = false;

                // When
                Action action = () => value.MustBeFalse(Message);

                // Then
                action.ShouldNotThrow<Exception>();
            }

            [Fact]
            public void ShouldThrowAssertionExceptionWhenValueIsTrueAnd_params_ParameterIsUsed()
            {
                // Given
                const bool value = true;

                // When
                Action action = () => value.MustBeFalse(Message, ParamValue);

                // Then
                action.ShouldThrow<AssertionException>()
                    .WithMessage(ExpectedMessage);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsFalseAnd_params_ParameterIsUsed()
            {
                // Given
                const bool value = false;

                // When
                Action action = () => value.MustBeFalse(Message, ParamValue);

                // Then
                action.ShouldNotThrow<Exception>();
            }
        }

        public class MustBeTrue : MustAssertionsTests
        {
            [Fact]
            public void ShouldThrowAssertionExceptionWhenValueIsFalse()
            {
                // Given
                const bool value = false;

                // When
                Action action = () => value.MustBeTrue(Message, ParamValue);

                // Then
                action.ShouldThrow<AssertionException>()
                    .WithMessage(ExpectedMessage);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenValueIsTrue()
            {
                // Given
                const bool value = true;

                // When
                Action action = () => value.MustBeTrue(Message, ParamValue);

                // Then
                action.ShouldNotThrow<Exception>();
            }
        }
    }
}