using MasPatas.Application.Interfaces;
using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;
using Polly.Wrap;

namespace MasPatas.Infrastructure.Resilience;

public class PollyResiliencePolicyExecutor : IResiliencePolicyExecutor
{
    public PollyResiliencePolicyExecutor() { }

    public async Task<T> ExecuteAsync<T>(
    Func<CancellationToken, Task<T>> action,
    CancellationToken cancellationToken = default)
    {
        var retry = Policy<T>
            .Handle<TimeoutRejectedException>()
            .Or<MongoDB.Driver.MongoException>()
            .WaitAndRetryAsync(3, attempt =>
            {
                var jitter = Random.Shared.Next(50, 150);
                return TimeSpan.FromMilliseconds(150 * attempt + jitter);
            });

        var circuit = Policy<T>
            .Handle<TimeoutRejectedException>()
            .Or<MongoDB.Driver.MongoException>()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

        var bulkhead = Policy.BulkheadAsync<T>(50, 100);

        var timeout = Policy.TimeoutAsync<T>(
            TimeSpan.FromSeconds(8),
            TimeoutStrategy.Pessimistic);

        var fallback = Policy<T>
            .Handle<BrokenCircuitException>()
            .Or<MongoDB.Driver.MongoConnectionException>()
            .Or<TimeoutRejectedException>()
            .FallbackAsync(ct => Task.FromException<T>(
                new InvalidOperationException(
                    "Servicio temporalmente no disponible. Reintente en unos segundos.")
            ));

        var policyWrap = Policy.WrapAsync(
            fallback,
            retry,
            circuit,
            timeout,
            bulkhead
        );

        return await policyWrap.ExecuteAsync(action, cancellationToken);
    }
}