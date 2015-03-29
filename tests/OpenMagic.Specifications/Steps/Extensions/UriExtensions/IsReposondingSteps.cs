using System;
using FluentAssertions;
using OpenMagic.Extensions;
using OpenMagic.Specifications.Helpers;
using TechTalk.SpecFlow;

namespace OpenMagic.Specifications.Steps.Extensions.UriExtensions
{
    [Binding]
    public class IsReposondingSteps
    {
        private readonly GivenData _given;
        private readonly ActualData _actual;

        public IsReposondingSteps(GivenData given, ActualData actual)
        {
            _given = given;
            _actual = actual;
        }

        [Given(@"URI is responding")]
        public void GivenUriIsResponding()
        {
            _given.Uri = new Uri("http://www.google.com");
        }

        [Given(@"URI is not responding")]
        public void GivenUriIsNotResponding()
        {
            _given.Uri = new Uri("http://" + Guid.NewGuid());
        }
        
        [When(@"I call IsResponding\(<uri>\)")]
        public void WhenICallIsResponding()
        {
            _actual.GetResult(() => _given.Uri.IsResponding());
        }
    }
}
