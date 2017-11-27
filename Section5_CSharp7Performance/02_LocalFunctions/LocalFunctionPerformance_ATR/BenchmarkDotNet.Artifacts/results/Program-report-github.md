``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i5-5200U CPU 2.20GHz (Broadwell), ProcessorCount=4
Frequency=2143474 Hz, Resolution=466.5324 ns, Timer=TSC
.NET Core SDK=2.0.0
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
 |                     Method |     Mean |     Error |    StdDev |     Gen 0 |     Gen 1 |     Gen 2 | Allocated |
 |--------------------------- |---------:|----------:|----------:|----------:|----------:|----------:|----------:|
 |      CalculateAtr_Delegate | 63.78 ms | 0.8702 ms | 0.8140 ms | 7937.5000 | 2875.0000 | 1937.5000 |  16.23 MB |
 | CalculateAtr_LocalFunction | 60.43 ms | 1.3788 ms | 1.5326 ms | 1937.5000 | 1937.5000 | 1937.5000 |   8.26 MB |
