using System;
using System.IO;
using FluentAssertions;
using OpenMagic.Specifications.Helpers;
using Reqnroll;

namespace OpenMagic.Specifications.Steps.Argument
{
    [Binding]
    public class DirectoryMustExistSteps
    {
        private readonly ActualData _actual;
        private readonly GivenData _given;

        public DirectoryMustExistSteps(GivenData given, ActualData actual)
        {
            _given = given;
            _actual = actual;
        }

        [Given(@"directory exists")]
        public void GivenDirectoryExists()
        {
            _given.Directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        }

        [Given(@"directory does not exists")]
        public void GivenDirectoryDoesNotExists()
        {
            _given.Directory = new DirectoryInfo(Guid.NewGuid().ToString());
        }

        [When(@"I call Argument\.DirectoryExists\(<param>, <paramName>\)")]
        public void WhenICallArgument_DirectoryExists_param_paramName()
        {
            _actual.GetResult(() => OpenMagic.Argument.DirectoryMustExist(_given.Directory, "dummy"));
        }

        [Scope(Feature = "DirectoryMustExist")]
        [Then(@"passed <param> should be returned")]
        public void ThenShouldBeReturned()
        {
            _actual.Exception.Should().BeNull();
            _actual.Result.Should().BeSameAs(_given.Directory);
        }
    }
}