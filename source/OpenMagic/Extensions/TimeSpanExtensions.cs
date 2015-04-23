using System;
using OpenMagic.Logging;

namespace OpenMagic.Extensions
{
    public static class TimeSpanExtensions
    {
        private static readonly ILog Log = LogProvider.GetLogger("TimeSpanExtensions");

        public static void WarnWhenGreaterThan(this TimeSpan timespan, TimeSpan maximumTimeSpan, string messageFormat)
        {
            WarnWhenGreaterThan(timespan, maximumTimeSpan, messageFormat, Log);
        }

        public static void WarnWhenGreaterThan(this TimeSpan timespan, TimeSpan maximumTimeSpan, string messageFormat, ILog log)
        {
            if (timespan < maximumTimeSpan)
            {
                return;
            }

            if (messageFormat.Contains("{milliseconds}", StringComparison.CurrentCultureIgnoreCase))
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