using FluentAssertions;
using TechTalk.SpecFlow;
using OpenMagic.Extensions;

namespace OpenMagic.Specifications.Steps.Extensions.StringExtensions
{
    [Binding]
    [Scope(Feature = "InsertStringBeforeEachUpperCaseCharacter")]
    public class InsertStringBeforeEachUpperCaseCharacterSteps
    {
        private string _value;
        private string _insert;
        private object _result;

        [When(@"I call InsertStringBeforeEachUpperCaseCharacter\((.*), (.*)\)")]
        public void WhenICallInsertStringBeforeEachUpperCaseCharacter(string value, string insert)
        {
            _value = value;
            _insert = insert;

            _result = value.InsertStringBeforeEachUpperCaseCharacter(insert);
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(string expectedResult)
        {
            _result.Should().Be(expectedResult, "because '{0}' should have been inserted before each upper case character in '{1}'", _insert, _value);
        }
    }
}
