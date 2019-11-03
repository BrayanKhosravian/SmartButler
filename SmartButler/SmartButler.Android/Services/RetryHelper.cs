using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartButler.Droid.Services
{
    internal static class RetryHelper
    {
	    public static Task RetryOnExceptionAsync(
            int times, TimeSpan delay, Func<Task> operation)
        {
           return RetryOnExceptionAsync<Exception>(times, delay, operation);
        }

        public static async Task RetryOnExceptionAsync<TException>(
            int times, TimeSpan delay, Func<Task> operation) where TException : Exception
        {
            if (times <= 0)
                throw new ArgumentOutOfRangeException(nameof(times));

            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    await operation();
                    break;
                }
                catch (TException ex)
                {
                    if (attempts == times)
                        throw;

                    await CreateDelayForException(times, attempts, delay, ex);
                }
            } while (true);
        }

        private static Task CreateDelayForException(
            int times, int attempts, TimeSpan delay, Exception ex)
        {
            var delay1 = IncreasingDelayInSeconds(attempts);
            
            return Task.Delay(delay1);
        }

        internal static int[] DelayPerAttemptInSeconds =
        {
            (int) TimeSpan.FromSeconds(2).TotalSeconds,
            (int) TimeSpan.FromSeconds(30).TotalSeconds,
            (int) TimeSpan.FromMinutes(2).TotalSeconds,
            (int) TimeSpan.FromMinutes(10).TotalSeconds,
            (int) TimeSpan.FromMinutes(30).TotalSeconds
        };

        static int IncreasingDelayInSeconds(int failedAttempts)
        {
            if (failedAttempts <= 0) throw new ArgumentOutOfRangeException();

            return failedAttempts > DelayPerAttemptInSeconds.Length ? DelayPerAttemptInSeconds.Last() : DelayPerAttemptInSeconds[failedAttempts];
        }
    }
}