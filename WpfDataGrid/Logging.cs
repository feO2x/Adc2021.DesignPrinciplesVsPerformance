using System.IO;
using Serilog;

namespace WpfDataGrid;

public static class Logging
{
    public static ILogger CreateLogger() =>
        new LoggerConfiguration().WriteTo.File(Path.Combine("Logs", "WpfDataGrid.log"), rollingInterval: RollingInterval.Day)
                                 .CreateLogger();
}