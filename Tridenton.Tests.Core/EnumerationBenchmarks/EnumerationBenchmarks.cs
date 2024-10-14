using BenchmarkDotNet.Attributes;
using Tridenton.Core;

namespace Tridenton.Tests.Core;

public sealed class TestEnumeration : Enumeration
{
    private TestEnumeration(int index, string value) : base(index, value) { }
    
    public static readonly TestEnumeration Test1 = new(1, "Test1");
    public static readonly TestEnumeration Test2 = new(2, "Test2");
    public static readonly TestEnumeration Test3 = new(3, "Test3");
    public static readonly TestEnumeration Test4 = new(4, "Test4");
    public static readonly TestEnumeration Test5 = new(5, "Test5");
    public static readonly TestEnumeration Test6 = new(6, "Test6");
    public static readonly TestEnumeration Test7 = new(7, "Test7");
    public static readonly TestEnumeration Test8 = new(8, "Test8");
    public static readonly TestEnumeration Test9 = new(9, "Test9");
}

[MemoryDiagnoser]
public class EnumerationBenchmarks
{
    public static int[] GetIndices()
    {
        return 
        [
            1,
            5,
            9
        ];
    }

    public static string[] GetValues()
    {
        return
        [
            "Test1",
            "Test5",
            "Test9",
        ];
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(GetIndices))]
    public TestEnumeration? GetByIndexLinq(int index)
    {
        return Enumeration
            .GetValues<TestEnumeration>()
            .FirstOrDefault(t => t.IndexEquals(index));
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(GetIndices))]
    public TestEnumeration? GetByIndexForeach(int index)
    {
        foreach (var enumeration in Enumeration.GetValues<TestEnumeration>())
        {
            if (enumeration.IndexEquals(index))
            {
                return enumeration;
            }
        }
        
        return null;
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(GetIndices))]
    public TestEnumeration? GetByIndexArrayLoop(int index)
    {
        var enumerations = Enumeration.GetValues<TestEnumeration>().ToArray();

        for (var i = 0; i < enumerations.Length; i++)
        {
            var enumeration = enumerations[i];
            
            if (enumeration.IndexEquals(index))
            {
                return enumeration;
            }
        }
        
        return null;
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(GetValues))]
    public TestEnumeration? GetByValueLinq(string? value)
    {
        return Enumeration
            .GetValues<TestEnumeration>()
            .FirstOrDefault(t => t.ValueEquals(value));
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(GetValues))]
    public TestEnumeration? GetByValueForeach(string? value)
    {
        foreach (var enumeration in Enumeration.GetValues<TestEnumeration>())
        {
            if (enumeration.ValueEquals(value))
            {
                return enumeration;
            }
        }
        
        return null;
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(GetValues))]
    public TestEnumeration? GetByValueArrayLoop(string? value)
    {
        var enumerations = Enumeration.GetValues<TestEnumeration>().ToArray();

        for (var i = 0; i < enumerations.Length; i++)
        {
            var enumeration = enumerations[i];
            if (enumeration.ValueEquals(value))
            {
                return enumeration;
            }
        }
        
        return null;
    }
}