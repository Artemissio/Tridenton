namespace Tridenton.EventLink.Core;

[JsonConverter(typeof(EnumerationJsonConverter<EventType>))]
public sealed class EventType : Enumeration
{
    private EventType(int index, string value) : base(index, value) { }

    public static readonly EventType None = new(0, string.Empty);
    public static readonly EventType Read = new(1, "Read");
    public static readonly EventType ReadBatch = new(2, "ReadBatch");
    public static readonly EventType Create = new(3, "Create");
    public static readonly EventType CreateBatch = new(4, "CreateBatch");
    public static readonly EventType Update = new(5, "Update");
    public static readonly EventType UpdateBatch = new(6, "UpdateBatch");
    public static readonly EventType Delete = new(7, "Delete");
    public static readonly EventType DeleteBatch = new(8, "DeleteBatch");
}