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
        foreach (var enumeration in GetValues<TEnumeration>())
        {
            if (enumeration.IndexEquals(index))
            {
                return enumeration;
            }
        }
        
        return null;
    }

    public static TEnumeration? GetValue<TEnumeration>(string value) where TEnumeration : Enumeration
    {
        foreach (var enumeration in GetValues<TEnumeration>())
        {
            if (enumeration.ValueEquals(value))
            {
                return enumeration;
            }
        }
        
        return null;
    }

    public static Enumeration? GetValue(Type enumerationType, int index)
    {
        foreach (var enumeration in GetValues(enumerationType))
        {
            if (enumeration.IndexEquals(index))
            {
                return enumeration;
            }
        }
        
        return null;
    }

    public static Enumeration? GetValue(Type enumerationType, string value)
    {
        foreach (var enumeration in GetValues(enumerationType))
        {
            if (enumeration.ValueEquals(value))
            {
                return enumeration;
            }
        }
        
        return null;
    }

    public override string ToString() => Value;

    public override int GetHashCode() => ToString().GetHashCode();

    public override bool Equals(object? obj)
    {
        return obj switch
        {
            Enumeration enumeration => IndexEquals(enumeration.Index),
            string str => ValueEquals(str),
            int index => IndexEquals(index),
            _ => false
        };
    }

    public static implicit operator string(Enumeration? enumeration) => enumeration is null ? string.Empty : enumeration.Value;

    public static bool operator ==(Enumeration? a, Enumeration? b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        return a is not null && a.Equals(b);
    }

    public static bool operator !=(Enumeration? a, Enumeration? b) => !(a == b);

    public static bool operator ==(Enumeration a, string b) => a.ValueEquals(b);
    public static bool operator !=(Enumeration a, string b) => !a.ValueEquals(b);

    public static bool operator ==(string? a, Enumeration b) => b.ValueEquals(a);
    public static bool operator !=(string? a, Enumeration b) => !b.ValueEquals(a);

    public static bool operator ==(Enumeration a, int b) => a.IndexEquals(b);
    public static bool operator !=(Enumeration a, int b) => !a.IndexEquals(b);

    public static bool operator ==(int a, Enumeration b) => b.IndexEquals(a);
    public static bool operator !=(int a, Enumeration b) => !b.IndexEquals(a);

    public bool ValueEquals(string? value) => Value.Equals(value?.Trim(), StringComparison.OrdinalIgnoreCase);
    public bool IndexEquals(int value) => Index == value;
    
    public static IEnumerable<TEnumeration> GetValues<TEnumeration>() where TEnumeration : Enumeration
    {
        return GetValues(typeof(TEnumeration))
            .Cast<TEnumeration>();
    }
    
    public static IEnumerable<Enumeration> GetValues(Type enumerationType)
    {
        return enumerationType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Select(f => f.GetValue(null))
            .Cast<Enumeration>();
    }
}