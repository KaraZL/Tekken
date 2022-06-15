using Microsoft.Data.SqlClient;
using Polly;
using Polly.Retry;

namespace CharactersAPI.Policies
{
    public class ClientPolicy
    {
        public RetryPolicy MigrationRetryPolicy { get; set; }

        public ClientPolicy()
        {
            MigrationRetryPolicy = Policy.Handle<SqlException>()
                .WaitAndRetry(
                    retryCount: 5,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, retryAttempt) =>
                    {
                        Console.WriteLine($"-- TekkenAPI : TekkenDatabase Migration Retry Polly... [{retryCount}]");
                    });
        }
    }
}
