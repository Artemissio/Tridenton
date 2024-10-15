using BenchmarkDotNet.Attributes;

namespace Tridenton.Tests.Core;

[MemoryDiagnoser]
public class GetByKeyBenchmarks
{
    private const int ItemsCount = 100;
    
    private GuidList _guidList = [];
    private UlidList _ulidList = [];
    private IndexList _indexList = [];

    private Guid _guidKey;
    private Ulid _ulidKey;
    private int _indexKey;

    public GetByKeyBenchmarks()
    {
        Setup();
    }
    
    [GlobalSetup]
    public void Setup()
    {
        _guidList = new GuidList(Enumerable.Repeat(new GuidRecord(), ItemsCount));
        _ulidList = new UlidList(Enumerable.Repeat(new UlidRecord(), ItemsCount));
        _indexList = new IndexList(Enumerable.Repeat(new IndexRecord(), ItemsCount));
        
        _guidKey = _guidList[ItemsCount / 2].Id;
        _ulidKey = _ulidList[ItemsCount / 2].Id;
        _indexKey = _indexList[ItemsCount / 2].Id;
    }

    [Benchmark]
    [Obsolete("GetByIdLinq is obsolete")]
    public GuidRecord Guid()
    {
        return _guidList.GetByIdLinq(_guidKey);
    }

    [Benchmark]
    public GuidRecord GuidForeach()
    {
        return _guidList.GetById(_guidKey);
    }

    [Benchmark]
    [Obsolete("GetByIdLinq is obsolete")]
    public UlidRecord Ulid()
    {
        return _ulidList.GetByIdLinq(_ulidKey);
    }

    [Benchmark]
    public UlidRecord UlidForeach()
    {
        return _ulidList.GetById(_ulidKey);
    }

    [Benchmark]
    [Obsolete("GetByIdLinq is obsolete")]
    public IndexRecord Index()
    {
        return _indexList.GetByIdLinq(_indexKey);
    }

    [Benchmark]
    public IndexRecord IndexForeach()
    {
        return _indexList.GetById(_indexKey);
    }
}