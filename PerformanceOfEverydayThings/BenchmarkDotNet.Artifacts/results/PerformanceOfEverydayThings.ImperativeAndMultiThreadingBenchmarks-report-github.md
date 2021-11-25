``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1348 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Job-FTZBOG : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

Runtime=.NET 6.0  

```
|                         Method |           Mean |         Error |        StdDev |         Median |         Ratio |      RatioSD |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
|------------------------------- |---------------:|--------------:|--------------:|---------------:|--------------:|-------------:|-------:|-------:|-------:|----------:|
|               CallStaticMethod |      0.0046 ns |     0.0008 ns |     0.0008 ns |      0.0043 ns |          1.00 |         0.00 |      - |      - |      - |         - |
|             CallInstanceMethod |      0.0804 ns |     0.0007 ns |     0.0007 ns |      0.0802 ns |         18.14 |         3.01 |      - |      - |      - |         - |
|            CallInterfaceMethod |      0.6519 ns |     0.0028 ns |     0.0026 ns |      0.6515 ns |        147.06 |        24.72 |      - |      - |      - |         - |
|        CallViaOverriddenMethod |      0.3195 ns |     0.0028 ns |     0.0027 ns |      0.3197 ns |         72.06 |        12.09 |      - |      - |      - |         - |
|                   CallDelegate |      0.6506 ns |     0.0031 ns |     0.0024 ns |      0.6515 ns |        147.53 |        27.17 |      - |      - |      - |         - |
| InstantiateStructAndCallMethod |      0.0063 ns |     0.0054 ns |     0.0048 ns |      0.0038 ns |          1.42 |         1.13 |      - |      - |      - |         - |
| InstantiateObjectAndCallMethod |      1.9335 ns |     0.0174 ns |     0.0162 ns |      1.9354 ns |        436.04 |        72.63 | 0.0014 |      - |      - |      24 B |
|              IncrementWithLock |      4.8415 ns |     0.0115 ns |     0.0102 ns |      4.8422 ns |      1,085.22 |       188.24 |      - |      - |      - |         - |
|         ThrowAndCatchException |  3,622.2367 ns |    42.9262 ns |    40.1532 ns |  3,624.9165 ns |    817,164.86 |   138,623.31 | 0.0114 |      - |      - |     200 B |
|          IncrementOnThreadPool |  1,001.3994 ns |    12.6626 ns |    11.8446 ns |  1,006.0232 ns |    225,854.27 |    37,941.16 | 0.0038 |      - |      - |      72 B |
|           IncrementOnNewThread | 86,072.4048 ns | 1,611.1480 ns | 1,507.0688 ns | 86,388.3179 ns | 19,412,780.06 | 3,280,915.77 | 0.3662 | 0.3662 | 0.3662 |     136 B |
