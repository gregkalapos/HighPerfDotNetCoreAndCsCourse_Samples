``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i5-5200U CPU 2.20GHz (Broadwell), ProcessorCount=4
Frequency=2143474 Hz, Resolution=466.5324 ns, Timer=TSC
.NET Core SDK=2.0.0
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
 |               Method |      Mean |     Error |    StdDev |
 |--------------------- |----------:|----------:|----------:|
 | ClassicIsAs1MioTimes | 10.975 ms | 0.0973 ms | 0.0910 ms |
 |   IsPattern1MioTimes |  9.530 ms | 0.1051 ms | 0.0983 ms |
