using FluentAssertions;
using OpenMagic.Extensions;
using Reqnroll;

namespace OpenMagic.Specifications.Steps.Extensions.StringExtensions
{
    [Binding]
    [Scope(Feature = "TrimStart")]
    public class TrimStartSteps
    {
        private string _result;
        private string _trimString;
        private string _value;

        [When(@"I call TrimStart\((.*), (.*)\)")]
        public void WhenICallTrimStart(string value, string trimString)
        {
            _value = value;
            _trimString = trimString;
            _result = value.TrimStart(trimString);
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(string expectedResult)
        {
            _result.Should().Be(expectedResult, "because trimString '{0}' should have been removed from start of value '{1}'", _trimString, _value);
        }
    }
}