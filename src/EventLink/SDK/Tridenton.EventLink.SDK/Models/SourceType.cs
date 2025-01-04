namespace Tridenton.EventLink.SDK;

[JsonConverter(typeof(EnumerationJsonConverter<SourceType>))]
public sealed class SourceType : Enumeration
{
    private SourceType(int index, string value) : base(index, value) { }
    
    public static readonly SourceType None = new(0, string.Empty);
    public static readonly SourceType PostgreSQL = new(1, "PostgreSQL");
}