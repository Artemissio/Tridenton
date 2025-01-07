namespace Tridenton.EventLink.SDK;

public sealed record DataChangeEventPayload
{
    public JsonElement[] Records { get; init; }

    public DataChangeEventPayload()
    {
        Records = [];
    }
    
    public static readonly DataChangeEventPayload Empty = new();
}