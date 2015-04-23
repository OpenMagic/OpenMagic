using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OpenMagic.Extensions;
using OpenMagic.Logging;
using TechTalk.SpecFlow;

namespace OpenMagic.Specifications.Steps.Extensions.TimeSpanExtensions
{
    [Binding]
    // ReSharper disable once InconsistentNaming
    public class TimeSpanExtensions_WarnWhenGreaterThanSteps
    {
        private readonly FakeLog _fakeLog = new FakeLog();
        private double _givenElapsedMilliseconds;
        private int _givenMaximumMilliseconds;
        private string _givenWarningMessageTemplate;

        [Given(@"elapsed time is '(.*)' milliseconds")]
        public void GivenElapsedTimeIsMilliseconds(int elapsedMilliseconds)
        {
            _givenElapsedMilliseconds = elapsedMilliseconds;
        }

        [Given(@"maximum is '(.*)' milliseconds")]
        public void GivenMaximumIsMilliseconds(int maximumMilliseconds)
        {
            _givenMaximumMilliseconds = maximumMilliseconds;
        }

        [Given(@"warning message template is '(.*)'")]
        public void GivenWarningMessageTemplateIs(string givenWarningMessageTemplate)
        {
            _givenWarningMessageTemplate = givenWarningMessageTemplate;
        }

        [When(@"WarnWhenGreaterThan\(stopwatch, message\) is called")]
        public void WhenWarnWhenGreaterThanStopwatchMessageIsCalled()
        {
            var elapsed = TimeSpan.FromMilliseconds(_givenElapsedMilliseconds);

            elapsed.WarnWhenGreaterThan(TimeSpan.FromMilliseconds(_givenMaximumMilliseconds), _givenWarningMessageTemplate, _fakeLog);
        }

        [Then(@"a warning is not logged")]
        public void ThenAWarningIsNotLogged()
        {
            _fakeLog.Calls.Any(call => call.logLevel == LogLevel.Warn).Should().BeFalse();
        }

        [Then(@"'(.*)' is added to the log")]
        public void ThenIsAddedToTheLog(string expectedWarning)
        {
            var call = _fakeLog.Calls.Single();
            var format = call.messageFunc();
            var args = call.formatParameters;
            string actualLogMessage = string.Format(format, args);

            actualLogMessage.Should().Be(expectedWarning);
        }

        public class FakeLog : ILog
        {
            public FakeLog()
            {
                Calls = new List<dynamic>();
            }

            public List<dynamic> Calls { get; set; }

            bool ILog.Log(LogLevel logLevel, Func<string> messageFunc, Exception exception, params object[] formatParameters)
            {
                // messageFunc is null when Is<LogLevel>Enabled() is called. We want to ignore these.
                if (messageFunc != null)
                {
                    Calls.Add(new {logLevel, messageFunc, exception, formatParameters});
                }

                return true;
            }
        }
    }
}