using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace OpenMagic.Specifications.Steps.Arguments
{
    [Binding]
    public class FileMustExistSteps
    {
        private FileInfo _givenFile;
        private FileInfo _actualFile;
        private Exception _actualException;

        [Given(@"file exists")]
        public void GivenFileExists()
        {
            _givenFile = new FileInfo(Directory.GetFiles(Directory.GetCurrentDirectory()).First());
        }

        [Given(@"file does not exists")]
        public void GivenFileDoesNotExists()
        {
            _givenFile = new FileInfo(Guid.NewGuid().ToString());
        }

        [When(@"I call Argument\.FileExists\(<param>, <paramName>\)")]
        public void WhenICallArgument_FileExists_param_paramName()
        {
            try
            {
                _actualFile = Argument.FileMustExist(_givenFile, "dummy");
            }
            catch (Exception exception)
            {
                _actualException = exception;
            }
        }

        [Then(@"<param> should be returned")]
        public void ThenShouldBeReturned()
        {
            _actualException.Should().BeNull();
            _actualFile.Should().BeSameAs(_givenFile);
        }

        [Then(@"ArgumentExection should be thrown")]
        public void ThenArgumentExectionShouldBeThrown()
        {
            _actualException.Should().BeOfType<ArgumentException>();
        }

        [Then(@"exception message should be:")]
        public void ThenExceptionMessageShouldBe(string expectedMessage)
        {
            _actualException.Message.Should().Be(expectedMessage.Replace("\n", "\r\n"));
        }
    }
}
