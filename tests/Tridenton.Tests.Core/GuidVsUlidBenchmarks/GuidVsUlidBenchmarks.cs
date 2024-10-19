using BenchmarkDotNet.Attributes;

namespace Tridenton.Tests.Core;

[MemoryDiagnoser]
public class GuidVsUlidBenchmarks
{
    [Benchmark]
    public Guid GetGuid()
    {
        return Guid.NewGuid();
    }
    
    [Benchmark]
    public Ulid GetUlid()
    {
        return Ulid.NewUlid();
    }
}