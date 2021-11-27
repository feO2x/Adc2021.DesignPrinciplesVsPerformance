namespace SyncVsAsync.AspNetCoreService;

public sealed record ThreadingResults(int NumberOfUsedWorkerThreads, int MaximumNumberOfWorkerThreads);