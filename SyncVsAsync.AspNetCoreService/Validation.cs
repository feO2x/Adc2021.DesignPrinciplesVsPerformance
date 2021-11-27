using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SyncVsAsync.AspNetCoreService;

public static class Validation
{
    public static bool CheckWaitIntervalForErrors(int waitIntervalInMilliseconds,
                                                  [NotNullWhen(true)] out Dictionary<string, string>? errors)
    {
        if (waitIntervalInMilliseconds is >= 15 and <= 60_000)
        {
            errors = null;
            return false;
        }

        errors = new Dictionary<string, string>(1)
        {
            [nameof(waitIntervalInMilliseconds)] = "waitIntervalInMilliseconds must be between 15ms and 60,000ms"
        };
        return true;
    }
}