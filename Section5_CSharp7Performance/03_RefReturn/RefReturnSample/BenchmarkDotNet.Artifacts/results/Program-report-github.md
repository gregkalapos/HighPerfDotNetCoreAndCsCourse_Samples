``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i5-5200U CPU 2.20GHz (Broadwell), ProcessorCount=4
Frequency=2143472 Hz, Resolution=466.5328 ns, Timer=TSC
.NET Core SDK=2.0.0
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
 |         Method |     Mean |     Error |    StdDev | Allocated |
 |--------------- |---------:|----------:|----------:|----------:|
 |    TestWithRef | 13.39 ms | 0.2241 ms | 0.2096 ms |     840 B |
 | TestWithoutRef | 18.01 ms | 0.3472 ms | 0.3248 ms |     840 B |
