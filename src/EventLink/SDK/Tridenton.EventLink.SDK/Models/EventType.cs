namespace Tridenton.EventLink.SDK;

[JsonConverter(typeof(EnumerationJsonConverter<EventType>))]
public sealed class EventType : Enumeration
{
    private EventType(int index, string value) : base(index, value) { }

    public static readonly EventType None = new(0, string.Empty);
    public static readonly EventType All = new(1, Constants.Wildcard);
    public static readonly EventType Create = new(2, "Create");
    public static readonly EventType Update = new(3, "Update");
    public static readonly EventType Delete = new(4, "Delete");
}