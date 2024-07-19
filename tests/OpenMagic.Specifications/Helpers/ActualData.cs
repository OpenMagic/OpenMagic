using System;
using JetBrains.Annotations;

namespace OpenMagic.Specifications.Helpers
{
    [UsedImplicitly]
    public class ActualData
    {
        public Exception Exception { get; set; }
        public object Result { get; set; }

        public void GetResult(Func<object> action)
        {
            try
            {
                Result = action();
                Exception = null;
            }
            catch (Exception exception)
            {
                Result = null;
                Exception = exception;
            }
        }
    }
}