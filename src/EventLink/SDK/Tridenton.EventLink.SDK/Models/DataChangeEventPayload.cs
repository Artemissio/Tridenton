namespace Tridenton.EventLink.SDK;

public sealed record DataChangeEventPayload
{
    public JsonElement[] Records { get; init; }

    public DataChangeEventPayload()
    {
        Records = [];
    }
}