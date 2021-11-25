``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1348 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Job-FTZBOG : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

Runtime=.NET 6.0  

```
|                 Method | NumberOfStrings |          Mean |       Error |     StdDev |    Ratio | RatioSD | Allocated |
|----------------------- |---------------- |--------------:|------------:|-----------:|---------:|--------:|----------:|
|   **FindLastStringInList** |             **100** |    **214.193 ns** |   **0.7382 ns** |  **0.6906 ns** |    **25.84** |    **0.13** |         **-** |
| FindStringInDictionary |             100 |      8.290 ns |   0.0343 ns |  0.0321 ns |     1.00 |    0.00 |         - |
|                        |                 |               |             |            |          |         |           |
|   **FindLastStringInList** |            **1000** |  **2,302.362 ns** |  **11.0493 ns** | **10.3355 ns** |   **293.17** |    **1.85** |         **-** |
| FindStringInDictionary |            1000 |      7.854 ns |   0.0470 ns |  0.0439 ns |     1.00 |    0.00 |         - |
|                        |                 |               |             |            |          |         |           |
|   **FindLastStringInList** |           **10000** | **23,859.921 ns** | **101.5373 ns** | **94.9781 ns** | **3,045.63** |   **19.94** |         **-** |
| FindStringInDictionary |           10000 |      7.834 ns |   0.0427 ns |  0.0400 ns |     1.00 |    0.00 |         - |
