using System.Diagnostics;
using System.Reflection;

namespace Tridenton.Core;

[DebuggerDisplay("{Value}")]
public class Enumeration
{
    public readonly int Index;
    public readonly string Value;

    protected Enumeration(int index, string value)
    {
        Index = index;
        Value = value;
    }

    public static TEnumeration? GetValue<TEnumeration>(int index) where TEnumeration : Enumeration
    {
        return GetValues<TEnumeration>().FirstOrDefault(v => v.Index == index);
    }

    public static TEnumeration? GetValue<TEnumeration>(string value) where TEnumeration : Enumeration
    {
        return GetValues<TEnumeration>().FirstOrDefault(v => v.ValueEquals(value));
    }

    public override string ToString() => Value;

    public override int GetHashCode() => ToString().GetHashCode();

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Enumeration enumeration => enumeration.ValueEquals(Value),
            string str => ValueEquals(str),
            int index => Index == index,
            _ => false
        };
    }

    public static implicit operator string(Enumeration? enumeration) => enumeration is null ? string.Empty : enumeration.Value;

    public static bool operator ==(Enumeration? a, Enumeration? b)
    {
        if (ReferenceEquals(a, b)) return true;

        return a is not null && a.Equals(b);
    }

    public static bool operator !=(Enumeration? a, Enumeration? b) => !(a == b);

    public static bool operator ==(Enumeration a, string b) => a.ValueEquals(b);
    public static bool operator !=(Enumeration a, string b) => !a.ValueEquals(b);

    public static bool operator ==(string? a, Enumeration b) => b.ValueEquals(a);
    public static bool operator !=(string? a, Enumeration b) => !b.ValueEquals(a);

    public static bool operator ==(Enumeration a, int b) => a.Index == b;
    public static bool operator !=(Enumeration a, int b) => a.Index != b;

    public static bool operator ==(int a, Enumeration b) => a == b.Index;
    public static bool operator !=(int a, Enumeration b) => a != b.Index;

    private bool ValueEquals(string? value) => Value.Equals(value?.Trim(), StringComparison.OrdinalIgnoreCase);

    private static TEnumeration[] GetValues<TEnumeration>() where TEnumeration : Enumeration
    {
        return typeof(TEnumeration)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Select(f => f.GetValue(null))
            .Cast<TEnumeration>()
            .ToArray();
    }
}