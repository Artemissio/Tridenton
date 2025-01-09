namespace Tridenton.EventLink.SDK;

[JsonConverter(typeof(EnumerationJsonConverter<EventType>))]
public sealed class EventType : Enumeration
{
    private EventType(int index, string value) : base(index, value) { }

    public static readonly EventType None = new(0, string.Empty);
    public static readonly EventType All = new(1, "*");
    public static readonly EventType Read = new(2, "Read");
    public static readonly EventType ReadBatch = new(3, "ReadBatch");
    public static readonly EventType Create = new(4, "Create");
    public static readonly EventType CreateBatch = new(5, "CreateBatch");
    public static readonly EventType Update = new(6, "Update");
    public static readonly EventType UpdateBatch = new(7, "UpdateBatch");
    public static readonly EventType Delete = new(8, "Delete");
    public static readonly EventType DeleteBatch = new(9, "DeleteBatch");
}