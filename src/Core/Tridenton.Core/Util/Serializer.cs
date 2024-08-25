namespace Tridenton.Core.Util;

public readonly struct Serializer
{
    public static readonly JsonSerializerOptions Options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Converters =
        {

        }
    };

    public static string ToJson<TEntity>(TEntity entity) => ToJson(entity, Options);
    public static string ToJson<TEntity>(TEntity entity, JsonSerializerOptions? options = null) => JsonSerializer.Serialize(entity, options);

    public static TEntity? FromJson<TEntity>(string json) => FromJson<TEntity>(json, Options);
    public static TEntity? FromJson<TEntity>(string json, JsonSerializerOptions? options = null) => JsonSerializer.Deserialize<TEntity>(json, options);
}