using System.Collections;

namespace Tridenton.Core.Utilities.Collections;

public enum PropertyType
{
    Boolean,
    Char,
    String,
    Short,
    UnsignedShort,
    Integer,
    UnsignedInteger,
    Long,
    UnsignedLong,
    Decimal,
    Double,
    Float,
    DateTime,
    DateOnly,
    DateTimeOffset,
    TimeSpan,
    TimeOnly,
    Guid,
    Ulid,
    Byte,
    Bytes,
    Enum,
    Object,
    Array,
    List,
}

public class PropertiesCollection : IEnumerable<KeyValuePair<string, string>>
{
    private sealed record PropertyValue(PropertyType Type, string Value);

    private readonly Dictionary<string, PropertyValue> _properties;

    public PropertiesCollection()
    {
        _properties = new();
    }

    public T Get<T>(string key)
    {
        var property = _properties[key];
        
        var value = property.Type switch
        {
            PropertyType.Boolean => bool.Parse(property.Value),
            PropertyType.Char => property.Value.ToCharArray()[0],
            PropertyType.String => property.Value,
            PropertyType.Short => short.Parse(property.Value),
            PropertyType.UnsignedShort => ushort.Parse(property.Value),
            PropertyType.Integer => int.Parse(property.Value),
            PropertyType.UnsignedInteger => uint.Parse(property.Value),
            PropertyType.Long => long.Parse(property.Value),
            PropertyType.UnsignedLong => ulong.Parse(property.Value),
            PropertyType.Decimal => decimal.Parse(property.Value),
            PropertyType.Double => double.Parse(property.Value),
            PropertyType.Float => float.Parse(property.Value),
            PropertyType.DateTime => DateTime.Parse(property.Value),
            PropertyType.DateOnly => DateOnly.Parse(property.Value),
            PropertyType.DateTimeOffset => DateTimeOffset.Parse(property.Value),
            PropertyType.TimeSpan => TimeSpan.Parse(property.Value),
            PropertyType.TimeOnly => TimeOnly.Parse(property.Value),
            PropertyType.Guid => Guid.Parse(property.Value),
            PropertyType.Ulid => Ulid.Parse(property.Value),
            PropertyType.Byte => byte.Parse(property.Value),
            PropertyType.Bytes => Convert.FromBase64String(property.Value),
            PropertyType.Enum => Enum.Parse(typeof(T), property.Value, false),
            _ => Serializer.FromJson<T>(property.Value)!,
        };

        var result = (T)value;
        
        return result;
    }

    public bool TryGet<T>(string key, out T value)
    {
        try
        {
            value = Get<T>(key);
            return true;
        }
        catch (Exception)
        {
            value = default!;
            return false;
        }
    }

    public void Set(string key, bool value)
    {
        SetCore(key, PropertyType.Boolean, value.ToString());
    }

    public void Set(string key, char value)
    {
        SetCore(key, PropertyType.Char, value.ToString());
    }

    public void Set(string key, string value)
    {
        SetCore(key, PropertyType.String, value);
    }

    public void Set(string key, short value)
    {
        SetCore(key, PropertyType.Short, value.ToString());
    }

    public void Set(string key, ushort value)
    {
        SetCore(key, PropertyType.UnsignedShort, value.ToString());
    }

    public void Set(string key, int value)
    {
        SetCore(key, PropertyType.Integer, value.ToString());
    }

    public void Set(string key, uint value)
    {
        SetCore(key, PropertyType.UnsignedInteger, value.ToString());
    }

    public void Set(string key, long value)
    {
        SetCore(key, PropertyType.Long, value.ToString());
    }

    public void Set(string key, ulong value)
    {
        SetCore(key, PropertyType.UnsignedLong, value.ToString());
    }

    public void Set(string key, decimal value, IFormatProvider provider)
    {
        SetCore(key, PropertyType.Decimal, value.ToString(provider));
    }

    public void Set(string key, decimal value, string? format = null)
    {
        SetCore(key, PropertyType.Decimal, value.ToString(format));
    }

    public void Set(string key, double value, IFormatProvider provider)
    {
        SetCore(key, PropertyType.Double, value.ToString(provider));
    }

    public void Set(string key, double value, string? format = null)
    {
        SetCore(key, PropertyType.Double, value.ToString(format));
    }

    public void Set(string key, float value, IFormatProvider provider)
    {
        SetCore(key, PropertyType.Float, value.ToString(provider));
    }

    public void Set(string key, float value, string? format = null)
    {
        SetCore(key, PropertyType.Float, value.ToString(format));
    }

    public void Set(string key, DateTime value, IFormatProvider provider)
    {
        SetCore(key, PropertyType.DateTime, value.ToString(provider));
    }

    public void Set(string key, DateTime value, string? format = null)
    {
        SetCore(key, PropertyType.DateTime, value.ToString(format));
    }

    public void Set(string key, DateOnly value, IFormatProvider provider)
    {
        SetCore(key, PropertyType.DateOnly, value.ToString(provider));
    }

    public void Set(string key, DateOnly value, string? format = null)
    {
        SetCore(key, PropertyType.DateOnly, value.ToString(format));
    }

    public void Set(string key, DateTimeOffset value, IFormatProvider provider)
    {
        SetCore(key, PropertyType.DateTimeOffset, value.ToString(provider));
    }
    
    public void Set(string key, DateTimeOffset value, string? format = null)
    {
        SetCore(key, PropertyType.DateTimeOffset, value.ToString(format));
    }

    public void Set(string key, TimeSpan value)
    {
        SetCore(key, PropertyType.TimeSpan, value.ToString());
    }

    public void Set(string key, TimeOnly value)
    {
        SetCore(key, PropertyType.TimeOnly, value.ToString());
    }

    public void Set(string key, Guid value)
    {
        SetCore(key, PropertyType.Guid, value.ToString());
    }

    public void Set(string key, Ulid value)
    {
        SetCore(key, PropertyType.Ulid, value.ToString());
    }

    public void Set(string key, byte value)
    {
        SetCore(key, PropertyType.Byte, value.ToString());
    }

    public void Set(string key, byte[] value)
    {
        SetCore(key, PropertyType.Bytes, Convert.ToBase64String(value));
    }

    public void Set(string key, Enum value)
    {
        SetCore(key, PropertyType.Enum, value.ToString());
    }

    public void Set<T>(string key, T value) where T : class
    {
        SetCore(key, PropertyType.Object, Serializer.ToJson(value));
    }

    public void Set<T>(string key, T[] value)
    {
        SetCore(key, PropertyType.Array, Serializer.ToJson(value));
    }

    public void Set<T>(string key, List<T> value)
    {
        SetCore(key, PropertyType.List, Serializer.ToJson(value));
    }

    public void Remove(string key)
    {
        _properties.Remove(key);
    }

    private void SetCore(string key, PropertyType type, string value)
    {
        var property = new PropertyValue(type, value);

        _properties[key] = property;
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _properties
            .Select(kvp => KeyValuePair.Create(kvp.Key, kvp.Value.Value))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}