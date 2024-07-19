using System;
using FluentAssertions;
using OpenMagic.Exceptions;
using OpenMagic.Extensions;
using OpenMagic.Specifications.Helpers;
using Reqnroll;

namespace OpenMagic.Specifications.Steps.Common
{
    [Binding]
    public class CommonThenSteps(GivenData given, ActualData actual)
    {
        private readonly GivenData _given = given;

        [Then(@"True should be returned")]
        public void ThenTrueShouldBeReturned()
        {
            actual.Result.Should().Be(true);
        }

        [Then(@"False should be returned")]
        public void ThenFalseShouldBeReturned()
        {
            actual.Result.Should().Be(false);
        }

        [Then(@"ArgumentException should be thrown")]
        public void ThenArgumentExceptionShouldBeThrown()
        {
            actual.Exception.Should().BeOfType<ArgumentException>();
        }

        [Then(@"ArgumentEmptyException should be thrown")]
        public void ThenArgumentEmptyExceptionShouldBeThrown()
        {
            actual.Exception.Should().BeOfType<ArgumentEmptyException>();
        }

        [Then(@"ArgumentWhitespaceException should be thrown")]
        public void ThenArgumentWhitespaceExceptionShouldBeThrown()
        {
            actual.Exception.Should().BeOfType<ArgumentWhiteSpaceException>();
        }

        [Then(@"the exception message should be:")]
        public void ThenTheExceptionMessageShouldBe(string expectedMessage)
        {
            actual.Exception.Message.Should().Be(expectedMessage.NormalizeLineEndings());
        }
    }
}