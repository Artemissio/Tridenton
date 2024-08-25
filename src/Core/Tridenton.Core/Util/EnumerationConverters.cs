using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel;
using System.Globalization;

namespace Tridenton.Core.Util;

/// <summary>
/// JSON converter
/// </summary>
/// <typeparam name="TEnumeration"></typeparam>
public sealed class EnumerationJsonConverter<TEnumeration> : JsonConverter<TEnumeration> where TEnumeration : Enumeration
{
    public override TEnumeration? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
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
        return sourceType == typeof(string) || sourceType == typeof(TEnumeration);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object? value)
    {
        return Enumeration.GetValue<TEnumeration>(value!.ToString()!);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        return value is TEnumeration constant ? constant.Value : base.ConvertTo(context, culture, value, destinationType);
    }
}

/// <summary>
/// EF Core value converter
/// </summary>
/// <typeparam name="TEnum"></typeparam>
public sealed class EnumerationDataContextValueConverter<TEnum> : ValueConverter<TEnum, string> where TEnum : Enumeration
{
    public EnumerationDataContextValueConverter() : base(e => e.Value, e => Enumeration.GetValue<TEnum>(e)!) { }
}