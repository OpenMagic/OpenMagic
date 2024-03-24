using System;
using System.Diagnostics;
using System.Threading;

namespace OpenMagic.Utilities;

/// <summary>
///     Collection of methods that run an action multiple times before throwing an exception.
/// </summary>
public static class Retry
{
    public static readonly TimeSpan NormalRetryPeriod = TimeSpan.FromSeconds(1);
    public static readonly TimeSpan LongRetryPeriod = TimeSpan.FromSeconds(10);
    public static readonly TimeSpan NormalRetrySleepPeriod = TimeSpan.FromMilliseconds(10);
    public static readonly TimeSpan LongRetrySleepPeriod = TimeSpan.FromMilliseconds(100);

    /// <summary>
    ///     Executes the specified action multiple times before throwing an exception.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    public static void WhileExceptionIsThrown(Action action)
    {
        WhileExceptionIsThrown(action, NormalRetryPeriod, NormalRetrySleepPeriod);
    }

    /// <summary>
    ///     Executes the specified action multiple times before throwing an exception.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="retryPeriod">The maximum time to retry the action.</param>
    public static void WhileExceptionIsThrown(Action action, TimeSpan retryPeriod)
    {
        WhileExceptionIsThrown(action, retryPeriod, NormalRetrySleepPeriod);
    }

    /// <summary>
    ///     Executes the specified action multiple times before throwing an exception.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="retryPeriod">The maximum time to retry the action.</param>
    /// <param name="retrySleepPeriod">How long to sleep after an exception.</param>
    public static void WhileExceptionIsThrown(Action action, TimeSpan retryPeriod, TimeSpan retrySleepPeriod)
    {
        var sw = Stopwatch.StartNew();

        while (sw.Elapsed < retryPeriod)
        {
            try
            {
                action();
                return;
            }
            catch (Exception)
            {
                // Swallow the exception and try again.
                Thread.Sleep(retrySleepPeriod);
            }
        }

        // Final attempt at the action.
        action();
    }
}