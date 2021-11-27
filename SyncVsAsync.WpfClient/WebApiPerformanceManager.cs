using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Light.GuardClauses;

namespace SyncVsAsync.WpfClient;

public sealed class WebApiPerformanceManager
{
    public WebApiPerformanceManager(HttpClient httpClient) =>
        Client = httpClient.MustNotBeNull(nameof(httpClient));

    private HttpClient Client { get; }

    private Stopwatch StopWatch { get; } = new ();

    private JsonSerializerOptions JsonSerializerOptions { get; } =
        new ()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

    public async Task<WebApiPerformanceResults> MeasureApiCallsAsync(bool isCallingAsynchronousApi,
                                                                     int numberOfCalls,
                                                                     int waitIntervalInMilliseconds)
    {
        numberOfCalls.MustBeGreaterThan(0, nameof(numberOfCalls));

        var targetUrl = isCallingAsynchronousApi ? "http://localhost:5000/api/asynchronous" : "http://localhost:5000/api/synchronous";
        targetUrl = targetUrl + "?waitIntervalInMilliseconds=" + waitIntervalInMilliseconds;

        StopWatch.Restart();
        var tasks = new Task<bool>[numberOfCalls];
        for (var i = 0; i < numberOfCalls; i++)
        {
            tasks[i] = CallApiAsync(targetUrl);
        }

        await Task.WhenAll(tasks);
        StopWatch.Stop();

        var response = await Client.GetAsync("http://localhost:5000/api/threadingResults");
        var textContent = await response.Content.ReadAsStringAsync();
        var threadingResults = JsonSerializer.Deserialize<ThreadingResults>(textContent,
                                                                            JsonSerializerOptions);

        return new WebApiPerformanceResults(StopWatch.Elapsed,
                                            tasks.Count(t => t.Result),
                                            tasks.Count(t => !t.Result),
                                            threadingResults);
    }

    private async Task<bool> CallApiAsync(string targetUrl)
    {
        try
        {
            // All calls are started directly from the UI thread. ConfigureAwait(false) tells
            // the asynchronous state machine that is generated for this method to run the
            // continuation of this method on the Thread Pool instead of the UI thread.
            // ConfigureAwait(true) is the default and pushes the continuation on the calling
            // thread if (and only if) it has a SynchronizationContext associated with it.
            var response = await Client.GetAsync(targetUrl).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}