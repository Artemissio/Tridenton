namespace Tridenton.EventLink.Core;

public sealed record DataChangeEventPayload
{
    public JsonElement[] Records { get; init; }

    public DataChangeEventPayload()
    {
        Records = [];
    }
}