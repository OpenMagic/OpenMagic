using System;
using FluentAssertions;
using OpenMagic.Utilities;
using Xunit;

namespace OpenMagic.Tests.Utilities
{
    public class RetryTests
    {
        public class WhileExceptionIsThrown
        {
            [Fact]
            public void ShouldInvoke_action_OnceIfItDoesNotThrowAnException()
            {
                // Given
                var count = 0;

                Action action = () => count++;

                // When
                Retry.WhileExceptionIsThrown(action);

                // Then
                count.Should().Be(1);
            }

            [Fact]
            public void ShouldInvoke_action_MultipleTimesUntilItDoesNotThrowAnException()
            {
                // Given
                var count = 0;

                Action action = () =>
                {
                    count++;

                    if (count < 5)
                    {
                        throw new Exception("dummy exception");
                    }
                };

                // When
                Retry.WhileExceptionIsThrown(action);

                // Then
                count.Should().Be(5);
            }

            [Fact]
            public void ShouldThrowExceptionThrownBy_action_When_retryPeriod_IsExceeded()
            {
                // Given
                Exception exception = null;
                const string exceptionMessage = "dummy exception";

                var count = 0;

                Action action = () =>
                {
                    count++;
                    throw new InvalidOperationException(exceptionMessage);
                };

                // When
                try
                {
                    Retry.WhileExceptionIsThrown(action);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                // Then
                count.Should().BeGreaterThan(1);

                exception.Should().BeOfType<InvalidOperationException>();
                exception.Should().NotBeNull();
                exception.Message.Should().Be(exceptionMessage);
            }
        }
    }
}
