namespace Tridenton.EventLink.Core;

[JsonConverter(typeof(EnumerationJsonConverter<DestinationType>))]
public sealed class DestinationType : Enumeration
{
    private DestinationType(int index, string value) : base(index, value) { }
    
    public static readonly DestinationType None = new(0, string.Empty);
    
}