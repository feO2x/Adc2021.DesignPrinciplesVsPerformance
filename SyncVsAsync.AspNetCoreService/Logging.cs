﻿using Serilog;
using Serilog.Events;

namespace SyncVsAsync.AspNetCoreService;

public static class Logging
{
    public static ILogger CreateLogger() =>
        new LoggerConfiguration().MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                 .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                                 .WriteTo.Console()
                                 .CreateLogger();
}