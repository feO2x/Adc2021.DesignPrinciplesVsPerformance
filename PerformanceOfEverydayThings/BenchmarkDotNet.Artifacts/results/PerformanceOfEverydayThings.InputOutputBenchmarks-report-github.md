``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1348 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Job-QDNXBM : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

Runtime=.NET 6.0  InvocationCount=1024  UnrollFactor=16  

```
|                       Method |     Mean |   Error |  StdDev | Ratio |  Gen 0 |  Gen 1 | Allocated |
|----------------------------- |---------:|--------:|--------:|------:|-------:|-------:|----------:|
|      LoadEmployeeFromRavenDb | 208.2 μs | 1.02 μs | 0.95 μs |  1.00 | 0.9766 |      - |     22 KB |
| LoadPersonFromMsSqlViaEfCore | 285.4 μs | 1.20 μs | 1.12 μs |  1.37 | 6.8359 | 0.9766 |    117 KB |
