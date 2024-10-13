namespace Tridenton.Core.Utilities.Converters;

internal sealed class TreidJsonConverter : JsonConverter<Treid>
{
    public override Treid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        return Treid.TryParse(value, null, out var treid)
            ? treid
            : null;
    }

    public override void Write(Utf8JsonWriter writer, Treid value, JsonSerializerOptions options)
    {
        writer.WriteRawValue(value.ToString());
    }
}