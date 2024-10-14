namespace Tridenton.Core.Utilities;

public readonly struct Serializer
{
    public static readonly JsonSerializerOptions Options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
    };

    public static string ToJson<TEntity>(TEntity entity) => ToJson(entity, Options);
    public static string ToJson<TEntity>(TEntity entity, JsonSerializerOptions options) => JsonSerializer.Serialize(entity, options);

    public static TEntity? FromJson<TEntity>(string json) => FromJson<TEntity>(json, Options);
    public static TEntity? FromJson<TEntity>(string json, JsonSerializerOptions options) => JsonSerializer.Deserialize<TEntity>(json, options);
}