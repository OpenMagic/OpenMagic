using System;
using FluentAssertions;
using OpenMagic.Specifications.Helpers;
using Reqnroll;

namespace OpenMagic.Specifications.Steps.ArgumentSteps
{
    [Binding]
    [Scope(Feature = "MustNotBeEmpty")]
    public class MustNotBeEmptySteps
    {
        private readonly ActualData _actual;
        private readonly GivenData _given;

        public MustNotBeEmptySteps(GivenData given, ActualData actual)
        {
            _given = given;
            _actual = actual;
        }

        [Given(@"param is an array with elements")]
        public void GivenParamIsAnArrayWithElements()
        {
            _given.ParameterValue = new[] { 1, 2, 3 };
        }

        [Given(@"param is an array with zero elements")]
        public void GivenParamIsAnArrayWithZeroElements()
        {
            _given.ParameterValue = new int[] { };
        }

        [Given(@"param is a guid with a value")]
        public void GivenParamIsAGuidWithAValue()
        {
            _given.ParameterValue = Guid.NewGuid();
        }

        [Given(@"param is a guid with an empty value")]
        public void GivenParamIsAGuidWithAnEmptyValue()
        {
            _given.ParameterValue = Guid.Empty;
        }

        [When(@"I call Argument\.MustNotBeEmpty\(int\[] (.*), (.*)\)")]
        public void WhenICallArgument_MustNotBeEmpty(string param0, string paramName1)
        {
            _actual.GetResult(() => ((int[])_given.ParameterValue).MustNotBeEmpty("dummy"));
        }

        [When(@"I call Argument\.MustNotBeEmpty\(Guid (.*), (.*)\)")]
        public void WhenICallArgument_MustNotBeEmptyGuid(string param0, string paramName1)
        {
            _actual.GetResult(() => ((Guid)_given.ParameterValue).MustNotBeEmpty("dummy"));
        }

        [Then(@"the param should be returned")]
        public void ThenShouldBeReturned()
        {
            _actual.Exception.Should().BeNull();
            _actual.Result.Should().Be(_given.ParameterValue);
        }
    }
}