using System;
using System.Text;
using Light.GuardClauses;

namespace SyncVsAsync.WpfClient;

public sealed class WebApiPerformanceResults
{
    public WebApiPerformanceResults(TimeSpan elapsedTime,
                                    int successfulCalls,
                                    int erroneousCalls,
                                    ThreadingResults threadingResults)
    {
        ElapsedTime = elapsedTime.MustNotBeLessThanOrEqualTo(TimeSpan.Zero, nameof(elapsedTime));
        SuccessfulCalls = successfulCalls.MustNotBeLessThan(0, nameof(successfulCalls));
        ErroneousCalls = erroneousCalls.MustNotBeLessThan(0, nameof(erroneousCalls));
        ThreadingResults = threadingResults.MustNotBeNull(nameof(threadingResults));
    }

    public TimeSpan ElapsedTime { get; }
    public int SuccessfulCalls { get; }
    public int ErroneousCalls { get; }
    public ThreadingResults ThreadingResults { get; }

    public override string ToString()
    {
        return new StringBuilder().AppendLine($"Performed {SuccessfulCalls + ErroneousCalls} API calls in {ElapsedTime.TotalSeconds:N2} seconds, {ErroneousCalls} of them being erroneous.")
                                  .AppendLine($"{ThreadingResults.NumberOfUsedWorkerThreads} worker threads were used concurrently at maximum.")
                                  .ToString();
    }
}