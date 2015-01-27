using System;
using Common.Logging;
using FakeItEasy;
using FluentAssertions;
using OpenMagic.Extensions;
using TechTalk.SpecFlow;

namespace OpenMagic.Tests.Extensions.Steps
{
    [Binding]
    // ReSharper disable once InconsistentNaming
    public class TimeSpanExtensions_WarnWhenGreaterThanSteps
    {
        private static readonly ILog Log = LogManager.GetLogger<TimeSpanExtensions_WarnWhenGreaterThanSteps>();

        private readonly ILog FakeLog;
        private object[] ActualArgs;
        private string ActualFormat;

        private double GivenElapsedMilliseconds;
        private int GivenMaximumMilliseconds;
        private string GivenWarningMessageTemplate;

        public TimeSpanExtensions_WarnWhenGreaterThanSteps()
        {
            FakeLog = A.Fake<ILog>();
            A.CallTo(() => FakeLog.WarnFormat(A<string>.Ignored, A<object[]>.Ignored))
                .Invokes((string format, object[] args) =>
                {
                    ActualFormat = format;
                    ActualArgs = args;

                    // Send to real loggers.
                    Log.WarnFormat(ActualFormat, ActualArgs);
                });
        }

        [Given(@"elapsed time is '(.*)' milliseconds")]
        public void GivenElapsedTimeIsMilliseconds(int elapsedMilliseconds)
        {
            GivenElapsedMilliseconds = elapsedMilliseconds;
        }

        [Given(@"maximum is '(.*)' milliseconds")]
        public void GivenMaximumIsMilliseconds(int maximumMilliseconds)
        {
            GivenMaximumMilliseconds = maximumMilliseconds;
        }

        [Given(@"warning message template is '(.*)'")]
        public void GivenWarningMessageTemplateIs(string givenWarningMessageTemplate)
        {
            GivenWarningMessageTemplate = givenWarningMessageTemplate;
        }

        [When(@"WarnWhenGreaterThan\(stopwatch, message\) is called")]
        public void WhenWarnWhenGreaterThanStopwatchMessageIsCalled()
        {
            var elapsed = TimeSpan.FromMilliseconds(GivenElapsedMilliseconds);

            elapsed.WarnWhenGreaterThan(TimeSpan.FromMilliseconds(GivenMaximumMilliseconds), GivenWarningMessageTemplate, FakeLog);
        }

        [Then(@"a warning is not logged")]
        public void ThenAWarningIsNotLogged()
        {
            A.CallTo(() => FakeLog.WarnFormat(A<string>.Ignored, A<object>.Ignored)).MustHaveHappened(Repeated.Never);
        }

        [Then(@"'(.*)' is added to the log")]
        public void ThenIsAddedToTheLog(string expectedWarning)
        {
            string.Format(ActualFormat, ActualArgs).Should().Be(expectedWarning);
        }
    }
}