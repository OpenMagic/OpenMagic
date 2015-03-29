using System;

namespace OpenMagic.Specifications.Helpers
{
    public class ActualData
    {
        public Exception Exception { get; set; }
        public object Result { get; set; }

        public void GetResult(Func<bool> action)
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