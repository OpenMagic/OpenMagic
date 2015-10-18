using System;
using FluentAssertions;
using OpenMagic.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace OpenMagic.Specifications.Steps.ArgumentSteps
{
    [Binding]
    public class MustBeBetweenSteps
    {
        private readonly GivenData _given;
        private readonly ActualData _actual;

        public MustBeBetweenSteps(GivenData given, ActualData actual)
        {
            _given = given;
            _actual = actual;
        }

        [Given(@"value is (.*)")]
        public void GivenValueIs(int value)
        {
            _given.IntValue = value;
        }
        
        [Given(@"minimumValue is (.*)")]
        public void GivenMinimumValueIs(int minimumValue)
        {
            _given.MinimumInt = minimumValue;
        }
        
        [Given(@"maximumValue is (.*)")]
        public void GivenMaximumValueIs(int maximumValue)
        {
            _given.MaximumInt = maximumValue;
        }

        [When(@"I call value\.MustBetween\(minimumValue, maximumValue\)")]
        public void WhenICallValue_MustBetweenMinimumValueMaximumValue()
        {
            try
            {
                _actual.Result = _given.IntValue.MustBeBetween(_given.MinimumInt, _given.MaximumInt, "value");
            }
            catch (Exception exception)
            {
                _actual.Exception = exception;
            }
        }
        
        [Then(@"(.*) should be returned")]
        public void ThenShouldBeReturned(int expectedValue)
        {
            _actual.Exception.Should().BeNull();
            _actual.Result.Should().Be(expectedValue);
        }
        
        [Then(@"ArgumentOutOfRangeException should be thrown")]
        public void ThenArgumentOutOfRangeExceptionShouldBeThrown()
        {
            _actual.Exception.Should().BeOfType<ArgumentOutOfRangeException>();
        }
    }
}
