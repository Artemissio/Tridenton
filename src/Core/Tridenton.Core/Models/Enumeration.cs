using System.Diagnostics;
using System.Reflection;

namespace Tridenton.Core.Models;

[DebuggerDisplay("{Value}")]
public class Enumeration
{
    private readonly string _value;

    public string Value => _value;

    protected Enumeration(string value)
    {
        _value = value;
    }

    public static TEnumeration? GetValue<TEnumeration>(string value) where TEnumeration : Enumeration
    {
        return GetValues<TEnumeration>().FirstOrDefault(v => v.ValueEquals(value));
    }

    public override string ToString() => _value;

    public override int GetHashCode() => ToString().GetHashCode();

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Enumeration enumeration => enumeration.ValueEquals(_value),
            string str => ValueEquals(str),
            _ => false
        };
    }

    public static implicit operator string(Enumeration? value) => value is null ? string.Empty : value._value;

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

    private bool ValueEquals(string? value) => _value.Equals(value?.Trim(), StringComparison.OrdinalIgnoreCase);

    private static TEnumeration[] GetValues<TEnumeration>() where TEnumeration : Enumeration
    {
        return typeof(TEnumeration).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).Select(f => f.GetValue(null)).Cast<TEnumeration>().ToArray();
    }
}