namespace Tridenton.EventLink.SDK;

[JsonConverter(typeof(EnumerationJsonConverter<DestinationType>))]
public sealed class DestinationType : Enumeration
{
    private DestinationType(int index, string value) : base(index, value) { }
    
    public static readonly DestinationType None = new(0, string.Empty);
    public static readonly DestinationType Webhooks = new(1, "Webhooks");
    public static readonly DestinationType RabbitMq = new(2, "RabbitMq");
}