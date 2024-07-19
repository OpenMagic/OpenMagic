using System;
using FluentAssertions;
using OpenMagic.Specifications.Helpers;
using Reqnroll;

namespace OpenMagic.Specifications.Steps.Argument
{
    [Binding]
    public class MustBeBetweenSteps(GivenData given, ActualData actual)
    {
        [Given(@"value is (.*)")]
        public void GivenValueIs(string value)
        {
            given.ParameterValue = value.Equals("today", StringComparison.OrdinalIgnoreCase)
                ? DateTime.UtcNow
                : int.Parse(value);
        }

        [Given(@"minimumValue is (.*)")]
        public void GivenMinimumValueIs(int minimumValue)
        {
            given.MinimumInt = minimumValue;
        }

        [Given(@"maximumValue is (.*)")]
        public void GivenMaximumValueIs(int maximumValue)
        {
            given.MaximumInt = maximumValue;
        }

        [When(@"I call Argument.MustBetween\(value, minimumValue, maximumValue\)")]
        public void WhenICall_Argument_MustBetween_Value_MinimumValue_MaximumValue()
        {
            try
            {
                // ReSharper disable once InvokeAsExtensionMethod
                actual.Result = OpenMagic.Argument.MustBeBetween((int)given.ParameterValue, given.MinimumInt, given.MaximumInt, "value");
            }
            catch (Exception exception)
            {
                actual.Exception = exception;
            }
        }

        [Then(@"number (.*) should be returned")]
        public void ThenShouldBeReturned(int expectedValue)
        {
            actual.Exception.Should().BeNull();
            actual.Result.Should().Be(expectedValue);
        }

        [Then(@"ArgumentOutOfRangeException should be thrown")]
        public void ThenArgumentOutOfRangeExceptionShouldBeThrown()
        {
            actual.Exception.Should().BeOfType<ArgumentOutOfRangeException>();
        }
    }
}