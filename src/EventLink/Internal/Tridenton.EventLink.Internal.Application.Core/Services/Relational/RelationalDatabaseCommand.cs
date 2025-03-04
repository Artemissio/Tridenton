using System.Text.Json.Serialization;
using Tridenton.Core.Utilities.Converters;

namespace Tridenton.EventLink.Internal.Application.Core.Services;

[JsonConverter(typeof(EnumerationJsonConverter<RelationalDatabaseCommand>))]
public sealed class RelationalDatabaseCommand : Enumeration
{
    private RelationalDatabaseCommand(int index, string value) : base(index, value) { }
    
    public static readonly RelationalDatabaseCommand Insert = new(1, "INSERT");
    public static readonly RelationalDatabaseCommand Update = new(2, "UPDATE");
    public static readonly RelationalDatabaseCommand Delete = new(3, "DELETE");

    public static RelationalDatabaseCommand? FromEventType(EventType eventType)
    {
        if (eventType == EventType.Create)
        {
            return Insert;
        }

        if (eventType == EventType.Update)
        {
            return Update;
        }

        if (eventType == EventType.Delete)
        {
            return Delete;
        }
        
        return null;
    }
}