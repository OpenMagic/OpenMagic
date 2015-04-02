using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using OpenMagic.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace OpenMagic.Specifications.Steps.Arguments
{
    [Binding]
    public class FileMustExistSteps
    {
        private readonly GivenData _given;
        private readonly ActualData _actual;

        public FileMustExistSteps(GivenData given, ActualData actual)
        {
            _given = given;
            _actual = actual;
        }

        [Given(@"file exists")]
        public void GivenFileExists()
        {
            _given.File = new FileInfo(Directory.GetFiles(Directory.GetCurrentDirectory()).First());
        }

        [Given(@"file does not exists")]
        public void GivenFileDoesNotExists()
        {
            _given.File = new FileInfo(Guid.NewGuid().ToString());
        }

        [When(@"I call Argument\.FileExists\(<param>, <paramName>\)")]
        public void WhenICallArgument_FileExists_param_paramName()
        {
            _actual.GetResult(() => Argument.FileMustExist(_given.File, "dummy"));
        }

        [Scope(Feature = "FileMustExist")]
        [Then(@"<param> should be returned")]
        public void ThenShouldBeReturned()
        {
            _actual.Exception.Should().BeNull();
            _actual.Result.Should().BeSameAs(_given.File);
        }
    }
}
