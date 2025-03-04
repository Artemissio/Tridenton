using System.Text;
using Dapper;
using Npgsql;
using Tridenton.EventLink.Internal.Application.Core.Models;
using Tridenton.EventLink.Internal.Application.Core.Services;
using Tridenton.EventLink.Internal.Sources.PostgreSQL.Models;
using Tridenton.EventLink.Internal.Sources.PostgreSQL.Utilities;
using Tridenton.EventLink.SDK.Sources;

namespace Tridenton.EventLink.Internal.Sources.PostgreSQL.Services;

internal sealed class PostgreSQLEventsListener : RelationalDatabaseEventsListener<PostgreSQLSettings>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly string _connectionString;
    
    private NpgsqlConnection? _connection;
    
    public PostgreSQLEventsListener(
        IEventLinkSettingsProvider settingsProvider,
        IListeningLimiter limiter,
        IEventsStream eventsStream,
        IEventsErrorsRepository errorsRepository,
        IEventTypeDeterminator eventTypeDeterminator,
        ISourceCommandParser commandParser,
        IEnumerable<EventsFilter> filters,
        IServiceProvider serviceProvider)
        : base(settingsProvider, limiter, eventsStream, errorsRepository, eventTypeDeterminator, commandParser, filters)
    {
        _serviceProvider = serviceProvider;
        _connectionString = FormatConnectionString();
    }

    protected override async ValueTask StartCoreAsync()
    {
        _connection = new NpgsqlConnection(_connectionString);

        await _connection.OpenAsync();
    }

    protected override async ValueTask StopCoreAsync()
    {
        if (_connection is null)
        {
            return;
        }

        await _connection.CloseAsync();
    }

    protected override async ValueTask DisposeCoreAsync()
    {
        if (_connection is null)
        {
            return;
        }

        await _connection.DisposeAsync();
        
        _connection = null;
    }

    protected override async ValueTask InitializeCoreAsync()
    {
        if (_connection is null)
        {
            return;
        }

        await using var transaction = await _connection.BeginTransactionAsync();

        var getAllTablesCommandText = string.Format(SQLScripts.GetAllTablesCommandTemplate, Settings.Schema);
        
        var existingTables = (await _connection.QueryAsync<string>(getAllTablesCommandText))
            .ToArray();
        
        var createChangesTableCommandText = string.Format(SQLScripts.CreateChangesTableCommand, Settings.Schema);
        var createChangesLoggingFunctionCommandText = string.Format(SQLScripts.CreateChangesLoggingFunctionCommand, Settings.Schema);
        
        await _connection.ExecuteAsync(createChangesTableCommandText, transaction);
        await _connection.ExecuteAsync(createChangesLoggingFunctionCommandText, transaction);

        var triggers = EventLinkSettings.FilteringSettings.Pairs
            .Select(p => new Trigger(
                table: p.Collection,
                schema: Settings.Schema!,
                existingTables: existingTables,
                eventTypes: p.EventTypes));

        var applyTriggersCommandBuilder = new StringBuilder();

        foreach (var trigger in triggers)
        {
            applyTriggersCommandBuilder.AppendLine(trigger.Apply());
        }
        
        var applyTriggersCommand = applyTriggersCommandBuilder.ToString();

        if (!string.IsNullOrEmpty(applyTriggersCommand))
        {
            await _connection.ExecuteAsync(applyTriggersCommand, transaction);
        }

        await transaction.CommitAsync();
    }

    protected override PostgreSQLSettings GetSettings()
    {
        return EventLinkSettings.SourceSettings.PostgreSQL!;
    }

    protected override string FormatConnectionString()
    {
        var builder = new NpgsqlConnectionStringBuilder
        {
            Host = Settings.Url,
            Port = Settings.Port,
            Database = Settings.Database,
            Username = Settings.Username,
            Password = Settings.Password,
        };

        return builder.ConnectionString;
    }
}