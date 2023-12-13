using Common.Contracts.Exceptions.Application;
using System.Diagnostics;

namespace Common.Helpers
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/architecture/patterns/retry
    /// </summary>
    public static class RetryHelper
    {
        public static void BasicRetry(Action action, Int32 retryLimit, Int32 retryDelaySec, String operationName = null)
        {
            var retryCount = 1;

            while (true)
            {
                try
                {
                    action.Invoke();
                    break;
                }
                catch (Exception ex)
                {
                    HandleRetry(ex, retryCount, retryLimit, operationName);
                }

                Thread.Sleep(retryDelaySec * 1000);

                retryCount++;
            }
        }
        public static T BasicRetry<T>(Func<T> func, Int32 retryLimit, Int32 retryDelaySec, String operationName = null)
        {
            var retryCount = 1;

            while (true)
            {
                try
                {
                    return func.Invoke();
                }
                catch (Exception ex)
                {
                    HandleRetry(ex, retryCount, retryLimit, operationName);
                }

                Thread.Sleep(retryDelaySec * 1000);

                retryCount++;
            }
        }
        public static void ProgressiveRetry(Action action, Int32 retryLimit, Int32 retryDelaySec, Int32 retryIncrementSec, String operationName = null)
        {
            var retryCount = 1;
            var retryDelay = retryDelaySec;

            while (true)
            {
                try
                {
                    action.Invoke();
                    break;
                }
                catch (Exception ex)
                {
                    HandleRetry(ex, retryCount, retryLimit, operationName);
                }

                Thread.Sleep(retryDelay * 1000);

                retryCount++;
                retryDelay += retryIncrementSec;
            }
        }
        public static T ProgressiveRetry<T>(Func<T> func, Int32 retryLimit, Int32 retryDelaySec, Int32 retryIncrementSec, String operationName = null)
        {
            var retryCount = 1;
            var retryDelay = retryDelaySec;

            while (true)
            {
                try
                {
                    return func.Invoke();
                }
                catch (Exception ex)
                {
                    HandleRetry(ex, retryCount, retryLimit, operationName);
                }

                Thread.Sleep(retryDelay * 1000);

                retryCount++;
                retryDelay += retryIncrementSec;
            }
        }


        // SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
        private static void HandleRetry(Exception ex, Int32 retryCount, Int32 retryLimit, String operationName)
        {
            Trace.TraceError(String.IsNullOrWhiteSpace(operationName) ?
                $"Retry {retryCount} of {retryLimit}" :
                $"\"{operationName}\", retry {retryCount} of {retryLimit}");

            if (retryCount <= retryLimit)
                return;

            var exceptionMessage = $"Retry limit reached because of:\r\n{ex.Message}\r\n{ex.StackTrace}";

            Trace.TraceError(exceptionMessage);
            throw new RetryException(exceptionMessage, ex);
        }
    }
}