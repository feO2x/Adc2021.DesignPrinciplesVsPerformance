``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1348 (21H2)
AMD Ryzen 9 5950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  Job-FTZBOG : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

Runtime=.NET 6.0  

```
|                 Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|----------------------- |-----------:|---------:|---------:|------:|--------:|-------:|----------:|
| UpdateEmployeesViaLoop |   841.6 ns |  9.37 ns |  8.76 ns |  1.00 |    0.00 |      - |         - |
| UpdateEmployeesViaLinq | 2,031.3 ns | 26.10 ns | 23.14 ns |  2.41 |    0.05 | 0.0038 |      96 B |
