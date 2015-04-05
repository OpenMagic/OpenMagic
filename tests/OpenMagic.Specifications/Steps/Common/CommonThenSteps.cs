using System;
using FluentAssertions;
using OpenMagic.Extensions;
using OpenMagic.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace OpenMagic.Specifications.Steps.Common
{
    [Binding]
    public class CommonThenSteps
    {
        private readonly GivenData _given;
        private readonly ActualData _actual;

        public CommonThenSteps(GivenData given, ActualData actual)
        {
            _given = given;
            _actual = actual;
        }

        [Then(@"<param> should be returned")]
        public void ThenParamShouldBeReturned()
        {
            _actual.Result.Should().Be(_given.Param);
        }

        [Then(@"True should be returned")]
        public void ThenTrueShouldBeReturned()
        {
            _actual.Result.Should().Be(true);
        }

        [Then(@"False should be returned")]
        public void ThenFalseShouldBeReturned()
        {
            _actual.Result.Should().Be(false);
        }

        [Then(@"ArgumentException should be thrown")]
        public void ThenArgumentExectionShouldBeThrown()
        {
            _actual.Exception.Should().BeOfType<ArgumentException>();
        }

        [Then(@"the exception message should be:")]
        public void ThenTheExceptionMessageShouldBe(string expectedMessage)
        {
            _actual.Exception.Message.Should().Be(expectedMessage.NormalizeLineEndings());
        }
    }
}
