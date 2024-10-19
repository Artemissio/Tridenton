using BenchmarkDotNet.Attributes;
using Tridenton.Core.Utilities;
using Tridenton.Core.Utilities.Collections;

namespace Tridenton.Tests.Core;

public sealed record GuidRecord : RecordMarker, IGuidUnique
{
    public Guid Id { get; init; }
}

public class GuidList : GuidExtendedList<GuidRecord>
{
    public GuidList(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="invalidOperationBehavior"></param>
    public GuidList(IEnumerable<GuidRecord> items, ExtendedListInvalidOperationBehavior invalidOperationBehavior = ExtendedListInvalidOperationBehavior.Return)
        : base(items, invalidOperationBehavior) { }
}

public sealed record UlidRecord : RecordMarker, IUlidUnique
{
    public Ulid Id { get; init; }
}

public class UlidList : UlidExtendedList<UlidRecord>
{
    public UlidList(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="invalidOperationBehavior"></param>
    public UlidList(IEnumerable<UlidRecord> items, ExtendedListInvalidOperationBehavior invalidOperationBehavior = ExtendedListInvalidOperationBehavior.Return)
        : base(items, invalidOperationBehavior) { }
}

public sealed record IndexRecord : RecordMarker, IIndexUnique
{
    public int Id { get; init; }
}

public class IndexList : IndexExtendedList<IndexRecord>
{
    public IndexList(ExtendedListInvalidOperationBehavior behavior = ExtendedListInvalidOperationBehavior.Return)
        : base(behavior) { }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    /// <param name="invalidOperationBehavior"></param>
    public IndexList(IEnumerable<IndexRecord> items, ExtendedListInvalidOperationBehavior invalidOperationBehavior = ExtendedListInvalidOperationBehavior.Return)
        : base(items, invalidOperationBehavior) { }
}

[MemoryDiagnoser]
public class ExtendedCollectionsBenchmarks
{
    [Params(1000)]
    public int ItemsCount { get; set; }
    
    private readonly GuidList _guidList;
    private readonly UlidList _ulidList;
    private readonly IndexList _indexList;

    public ExtendedCollectionsBenchmarks()
    {
        _guidList = [];
        _ulidList = [];
        _indexList = [];
    }

    [Benchmark]
    public void PopulateGuidList()
    {
        var items = Enumerable.Repeat(new GuidRecord(), ItemsCount);
        
        _guidList.AddRange(items);
    }

    [Benchmark]
    public void PopulateUlidList()
    {
        var items = Enumerable.Repeat(new UlidRecord(), ItemsCount);
        
        _ulidList.AddRange(items);
    }

    [Benchmark]
    public void PopulateIndexList()
    {
        var items = Enumerable.Repeat(new IndexRecord(), ItemsCount);
        
        _indexList.AddRange(items);
    }
}