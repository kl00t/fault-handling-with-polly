using Polly;
using Polly.Retry;

namespace RequestService.Policies
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }

        public ClientPolicy()
        {
            // If the response message is not a success status code then retry 5 times.
            ImmediateHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                response => !response.IsSuccessStatusCode)
                .RetryAsync(5);

            // If the response message is not a success status code then wait 3 seconds and then retry 5 times.
            LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                response => !response.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(3));

            // If the response message is not a success status code then wait exponential time for 5 attempts
            ExponentialHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                response => !response.IsSuccessStatusCode)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}