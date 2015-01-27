using FluentAssertions;
using TechTalk.SpecFlow;
using OpenMagic.Extensions;

namespace OpenMagic.Specifications.Steps.Extensions.StringExtensions
{
    [Binding]
    public class TrimEndSteps
    {
        private string _result;
        private string _trimString;
        private string _value;

        [When(@"I call TrimEnd\((.*), (.*)\)")]
        public void WhenICallTrimEnd(string value, string trimString)
        {
            _value = value;
            _trimString = trimString;
            _result = value.TrimEnd(trimString);
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(string expectedResult)
        {
            _result.Should().Be(expectedResult, "because trimString '{0}' should have been removed from end of value '{1}'", _trimString, _value);
        }
    }
}