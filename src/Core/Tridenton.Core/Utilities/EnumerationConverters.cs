using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;
using System.Globalization;

namespace Tridenton.Core.Utilities;

/// <summary>
/// JSON converter
/// </summary>
/// <typeparam name="TEnumeration"></typeparam>
public sealed class EnumerationJsonConverter<TEnumeration> : JsonConverter<TEnumeration> where TEnumeration : Enumeration
{
    public override TEnumeration? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetInt32(out var index))
        {
            return Enumeration.GetValue<TEnumeration>(index);
        }

        var value = reader.GetString();

        return Enumeration.GetValue<TEnumeration>(value!);
    }

    public override void Write(Utf8JsonWriter writer, TEnumeration value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>
/// Type converter
/// </summary>
/// <typeparam name="TEnumeration"></typeparam>
public sealed class EnumerationTypeConverter<TEnumeration> : TypeConverter where TEnumeration : Enumeration
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || sourceType == typeof(int) || sourceType == typeof(TEnumeration);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
    {
        return value switch
        {
            string strValue => Enumeration.GetValue<TEnumeration>(strValue),
            int index => Enumeration.GetValue<TEnumeration>(index),
            _ => null
        };
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is TEnumeration enumeration)
        {
            return enumeration.Value;
        }

        if (value is string strValue)
        {
            return strValue;
        }

        return null;
    }
}

/// <summary>
/// EF Core value converter
/// </summary>
/// <typeparam name="TEnum"></typeparam>
public sealed class EnumerationDbContextValueConverter<TEnum> : ValueConverter<TEnum, string> where TEnum : Enumeration
{
    public EnumerationDbContextValueConverter() : base(e => e.Value, value => Enumeration.GetValue<TEnum>(value)!) { }
}

/// <summary>
/// EF Core index converter
/// </summary>
/// <typeparam name="TEnum"></typeparam>
public sealed class EnumerationDbContextIndexConverter<TEnum> : ValueConverter<TEnum, int> where TEnum : Enumeration
{
    public EnumerationDbContextIndexConverter() : base(e => e.Index, index => Enumeration.GetValue<TEnum>(index)!) { }
}