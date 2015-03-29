using FluentAssertions;
using OpenMagic.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace OpenMagic.Specifications.Steps.Common
{
    [Binding]
    public class CommonThenSteps
    {
        private readonly ActualData _actual;

        public CommonThenSteps(ActualData actual)
        {
            _actual = actual;
        }

        [Then(@"(.*) should be returned")]
        public void ThenShouldBeReturned(string expectedResult)
        {
            _actual.Result.ToString().Should().Be(expectedResult);
        }
    }
}
