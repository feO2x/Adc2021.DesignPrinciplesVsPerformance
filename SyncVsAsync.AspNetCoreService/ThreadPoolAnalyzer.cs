using System.Threading;

namespace SyncVsAsync.AspNetCoreService;

public sealed class ThreadPoolAnalyzer
{
    public readonly int MaximumWorkerThreads;
    private int _usedWorkerThreads;

    public ThreadPoolAnalyzer() =>
        ThreadPool.GetMaxThreads(out MaximumWorkerThreads, out _);

    public int UsedWorkerThreads => Volatile.Read(ref _usedWorkerThreads);

    public void UpdateMaximumParallelismLevel()
    {
        ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out _);
        var currentlyUsedWorkerThreads = MaximumWorkerThreads - availableWorkerThreads;
        InterlockedMaximum(ref _usedWorkerThreads, currentlyUsedWorkerThreads);
    }

    private static void InterlockedMaximum(ref int target, int value)
    {
        int temporaryValue;
        var readValueOfTarget = target;
        do
        {
            if (value <= readValueOfTarget)
                return;
            temporaryValue = readValueOfTarget;
            readValueOfTarget = Interlocked.CompareExchange(ref target, value, readValueOfTarget);
        } while (temporaryValue != readValueOfTarget);
    }

    public void Reset() => _usedWorkerThreads = 0;
}