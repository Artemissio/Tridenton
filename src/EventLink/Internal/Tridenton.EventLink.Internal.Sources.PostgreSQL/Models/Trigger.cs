using System.Text;
using Tridenton.Core;
using Tridenton.EventLink.Internal.Application.Core.Services;
using Tridenton.EventLink.SDK;

namespace Tridenton.EventLink.Internal.Sources.PostgreSQL.Models;

internal struct Trigger
{
    /// <summary>
    /// <para>
    /// 0 - operation lowercase (insert/update/delete)
    /// </para>
    /// <para>
    /// 1 - table name normalized (lowercase with underscore)
    /// </para>
    /// <para>
    /// 2 - operation uppercase (INSERT/UPDATE/DELETE)
    /// </para>
    /// <para>
    /// 3 - schema
    /// </para>
    /// <para>
    /// 4 - table name
    /// </para>
    /// </summary>
    private const string CreateTriggerCommandTemplate = """
        CREATE TRIGGER IF NOT EXISTS after_{0}_{1}_trigger
        AFTER {2} ON {3}.{4}
        FOR EACH ROW EXECUTE FUNCTION log_tridenton_event_link_changes();
    """;
    
    /// <summary>
    /// <para>
    /// 0 - operation lowercase (insert/update/delete)
    /// </para>
    /// <para>
    /// 1 - table name normalized (lowercase with underscore)
    /// </para>
    /// </summary>
    private const string DropTriggerCommandTemplate = "DROP TRIGGER IF EXISTS after_{0}_{1}_trigger;";
    
    private string _table;
    private readonly string _schema;
    private readonly string[] _existingTables;
    private readonly EventType[] _eventTypes;

    public Trigger(string table, string schema, string[] existingTables, EventType[] eventTypes)
    {
        _table = table;
        _schema = schema;
        _existingTables = existingTables;
        _eventTypes = eventTypes;
    }

    public string Apply()
    {
        var normalizedTableName = _table
            .Replace("\"", string.Empty)
            .Replace(" ", "_")
            .ToLower();
        
        var result = new StringBuilder();
        
        var dropTableTriggersCommand = DropTableTriggers(normalizedTableName);
        
        result.AppendLine(dropTableTriggersCommand);
        
        if (!_existingTables.Contains(_table))
        {
            _table = $"\"{normalizedTableName}\"";
            
            if (!_existingTables.Contains(_table))
            {
                return result.ToString();
            }
        }

        if (_eventTypes.Length == 1 && _eventTypes[0] == EventType.All)
        {
            result.AppendLine(GetTriggerCreationCommand(normalizedTableName, EventType.Create));
            result.AppendLine(GetTriggerCreationCommand(normalizedTableName, EventType.Update));
            result.AppendLine(GetTriggerCreationCommand(normalizedTableName, EventType.Delete));
        }

        if (_eventTypes.Contains(EventType.Create))
        {
            result.AppendLine(GetTriggerCreationCommand(normalizedTableName, EventType.Create));
        }

        if (_eventTypes.Contains(EventType.Update))
        {
            result.AppendLine(GetTriggerCreationCommand(normalizedTableName, EventType.Update));
        }

        if (_eventTypes.Contains(EventType.Delete))
        {
            result.AppendLine(GetTriggerCreationCommand(normalizedTableName, EventType.Delete));
        }
        
        return result.ToString();
    }

    private string DropTableTriggers(string normalizedTableName)
    {
        var dropTriggersCommandBuilder = new StringBuilder();

        foreach (var databaseCommand in Enumeration.GetValues<RelationalDatabaseCommand>())
        {
            var dropTriggerCommand = string.Format(DropTriggerCommandTemplate, databaseCommand.Value.ToLower(), normalizedTableName);
                
            dropTriggersCommandBuilder.AppendLine(dropTriggerCommand);
        }
            
        return dropTriggersCommandBuilder.ToString();
    }

    private string GetTriggerCreationCommand(string normalizedTableName, EventType eventType)
    {
        var databaseCommand = RelationalDatabaseCommand.FromEventType(eventType);

        if (databaseCommand is null)
        {
            return string.Empty;
        }
        
        return string.Format(CreateTriggerCommandTemplate,
            databaseCommand.Value.ToLower(),
            normalizedTableName,
            databaseCommand.Value,
            _schema,
            _table);
    }
}