using System;
using Common.Logging;

namespace OpenMagic.Extensions
{
    public static class TimeSpanExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public static void WarnWhenGreaterThan(this TimeSpan timespan, TimeSpan maximumTimeSpan, string messageFormat)
        {
            timespan.WarnWhenGreaterThan(maximumTimeSpan, messageFormat, Log);
        }

        public static void WarnWhenGreaterThan(this TimeSpan timespan, TimeSpan maximumTimeSpan, string messageFormat, ILog log)
        {
            if (timespan < maximumTimeSpan)
            {
                return;
            }

            if (messageFormat.Contains("{milliseconds}", StringComparison.InvariantCultureIgnoreCase))
            {
                log.WarnFormat(messageFormat.Replace("{milliseconds}", "{0:N0}"), timespan.TotalMilliseconds);
            }
            else
            {
                log.WarnFormat(messageFormat, timespan);
            }
        }
    }
}