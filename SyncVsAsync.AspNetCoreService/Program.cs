using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SyncVsAsync.AspNetCoreService;

var builder = WebApplication.CreateBuilder();
builder.Host.UseSerilog(Logging.CreateLogger());
builder.Services.AddSingleton(new ThreadPoolAnalyzer());
var app = builder.Build();

app.UseSerilogRequestLogging()
   .UseRouting();
app.MapGet("/api/synchronous",
           (int waitIntervalInMilliseconds, [FromServices] ThreadPoolAnalyzer analyzer) =>
           {
               if (Validation.CheckWaitIntervalForErrors(waitIntervalInMilliseconds, out var errors))
                   return Results.BadRequest(errors);
               Thread.Sleep(waitIntervalInMilliseconds);
               analyzer.UpdateMaximumParallelismLevel();
               return Results.Ok();
           });

app.MapGet("/api/asynchronous",
           async (int waitIntervalInMilliseconds, [FromServices] ThreadPoolAnalyzer analyzer) =>
           {
               if (Validation.CheckWaitIntervalForErrors(waitIntervalInMilliseconds, out var errors))
                   return Results.BadRequest(errors);
               await Task.Delay(1000);
               analyzer.UpdateMaximumParallelismLevel();
               return Results.Ok();
           });

app.MapGet("/api/threadingResults",
           ([FromServices] ThreadPoolAnalyzer analyzer) =>
           {
               var threadingResults = new ThreadingResults(analyzer.UsedWorkerThreads,
                                                           analyzer.MaximumWorkerThreads);
               analyzer.Reset();
               return threadingResults;
           });

app.Run();