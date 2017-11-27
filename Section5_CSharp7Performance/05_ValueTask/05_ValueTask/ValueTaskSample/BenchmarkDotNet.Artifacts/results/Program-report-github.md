``` ini

BenchmarkDotNet=v0.10.9, OS=Windows 10 Redstone 2 (10.0.15063)
Processor=Intel Core i5-5200U CPU 2.20GHz (Broadwell), ProcessorCount=4
Frequency=2143474 Hz, Resolution=466.5324 ns, Timer=TSC
.NET Core SDK=2.0.0
  [Host]     : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.0 (Framework 4.6.00001.0), 64bit RyuJIT


```
 |          Method |     Mean |     Error |    StdDev |     Gen 0 |    Gen 1 |    Gen 2 | Allocated |
 |---------------- |---------:|----------:|----------:|----------:|---------:|---------:|----------:|
 |   ValueTaskImpl | 70.01 ms | 0.4628 ms | 0.3865 ms | 3195.8333 | 750.0000 | 312.5000 |   5.38 KB |
 | ClassicTaskImpl | 70.46 ms | 0.5604 ms | 0.4680 ms | 3312.5000 | 750.0000 | 312.5000 |   5.37 KB |
