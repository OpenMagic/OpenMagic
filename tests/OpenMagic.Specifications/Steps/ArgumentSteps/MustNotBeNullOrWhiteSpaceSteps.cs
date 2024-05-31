using System;
using FluentAssertions;
using OpenMagic.Exceptions;
using Reqnroll;

namespace OpenMagic.Specifications.Steps.ArgumentSteps
{
    [Binding]
    public class MustNotBeNullOrWhiteSpaceSteps
    {
        private string _stringArgument;
        private Exception _exception;
        private string _returned;

        [Given("a string argument {string}")]
        public void GivenAStringArgument(string value)
        {
            _stringArgument = value;
        }

        [When("I check if the string is not null or whitespace")]
        public void WhenICheckIfTheStringIsNotNullOrWhitespace()
        {
            try
            {
                _returned = _stringArgument.MustNotBeNullOrWhiteSpace(nameof(_stringArgument));
            }
            catch (Exception exception)
            {
                _exception = exception;
            }
        }

        [Then("the string argument should be returned")]
        public void ThenTheStringArgumentShouldBeReturned()
        {
            _returned.Should().Be(_stringArgument);
        }

        [Given("a null string argument")]
        public void GivenANullStringArgument()
        {
            _stringArgument = null;
        }

        [Then("ArgumentNullOrWhiteSpaceException should be thrown")]
        public void ThenArgumentNullOrWhiteSpaceExceptionShouldBeThrown()
        {
            _exception.Should().BeOfType<ArgumentNullOrWhiteSpaceException>();
        }

        [Given("an empty string argument")]
        public void GivenAnEmptyStringArgument()
        {
            _stringArgument = "";
        }

        [Given("a whitespace string argument")]
        public void GivenAWhitespaceStringArgument()
        {
            _stringArgument = " ";
        }
    }
}
