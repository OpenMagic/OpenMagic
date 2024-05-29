using System;
using FluentAssertions;
using OpenMagic.Exceptions;
using OpenMagic.Extensions;
using OpenMagic.Specifications.Helpers;
using Reqnroll;

namespace OpenMagic.Specifications.Steps.Common
{
    [Binding]
    public class CommonThenSteps
    {
        private readonly ActualData _actual;
        private readonly GivenData _given;

        public CommonThenSteps(GivenData given, ActualData actual)
        {
            _given = given;
            _actual = actual;
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
        public void ThenArgumentExceptionShouldBeThrown()
        {
            _actual.Exception.Should().BeOfType<ArgumentException>();
        }

        [Then(@"ArgumentEmptyException should be thrown")]
        public void ThenArgumentEmptyExceptionShouldBeThrown()
        {
            _actual.Exception.Should().BeOfType<ArgumentEmptyException>();
        }

        [Then(@"ArgumentWhitespaceException should be thrown")]
        public void ThenArgumentWhitespaceExceptionShouldBeThrown()
        {
            _actual.Exception.Should().BeOfType<ArgumentWhitespaceException>();
        }

        [Then(@"the exception message should be:")]
        public void ThenTheExceptionMessageShouldBe(string expectedMessage)
        {
            _actual.Exception.Message.Should().Be(expectedMessage.NormalizeLineEndings());
        }
    }
}