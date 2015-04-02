using System;
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

        [Then(@"ArgumentException should be thrown")]
        public void ThenArgumentExectionShouldBeThrown()
        {
            _actual.Exception.Should().BeOfType<ArgumentException>();
        }

        [Then(@"the exception message should be:")]
        public void ThenTheExceptionMessageShouldBe(string expectedMessage)
        {
            _actual.Exception.Message.Should().Be(expectedMessage.Replace("\n", "\r\n"));
        }
    }
}
