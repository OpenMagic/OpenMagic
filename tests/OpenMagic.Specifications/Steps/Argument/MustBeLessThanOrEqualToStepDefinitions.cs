using System;
using FluentAssertions;
using OpenMagic.Extensions;
using OpenMagic.Specifications.Helpers;
using Reqnroll;

namespace OpenMagic.Specifications.Steps.Argument
{
    [Binding]
    [Scope(Feature = "MustBeLessThanOrEqualTo")]
    public class MustBeLessThanOrEqualToSteps(GivenData given, ActualData actual)
    {
        [Given(@"value is today")]
        public void GivenValueIs()
        {
            given.ParameterValue = DateTime.UtcNow.Date;
        }

        [Given(@"maximumValue is (.*)")]
        public void GivenMaximumValueIs(string maximumValue)
        {
            given.MaximumDateTime = maximumValue.AsRelativeDate();
        }

        [When(@"I call Argument.MustBeLessThanOrEqualTo\(value, maximumValue\)")]
        public void WhenICallArgument_MustBeLessThanOrEqualToValueMaximumValue()
        {
            actual.GetResult(() => ((DateTime)given.ParameterValue).MustBeLessThanOrEqualTo(given.MaximumDateTime, "value"));
        }

        [Then("today should be returned")]
        public void ThenTodayShouldBeReturned()
        {
            actual.Result.Should().Be(DateTime.UtcNow.Date);
        }

        [Then(@"the exception message should be:")]
        public void ThenTheExceptionMessageShouldBe(string expectedMessage)
        {
            expectedMessage = expectedMessage
                .NormalizeLineEndings()
                .Replace("maximumValue", given.MaximumDateTime.ToString("dd MMM yyyy"));

            actual.Exception.Message.Should().Be(expectedMessage);
        }
    }
}