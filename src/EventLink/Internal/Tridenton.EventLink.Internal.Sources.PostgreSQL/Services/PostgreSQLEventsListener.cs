using Tridenton.EventLink.Internal.Application.Core.Models;
using Tridenton.EventLink.Internal.Application.Core.Services;
using Tridenton.EventLink.SDK.Sources;

namespace Tridenton.EventLink.Internal.Sources.PostgreSQL.Services;

internal sealed class PostgreSQLEventsListener : RelationalDatabaseEventsListener<PostgreSQLSettings>
{
    public PostgreSQLEventsListener(
        IListeningLimiter limiter,
        IEventsStream eventsStream,
        IEventsErrorsRepository errorsRepository,
        IEventTypeDeterminator eventTypeDeterminator,
        ISourceCommandParser commandParser,
        IEnumerable<EventsFilter> filters)
        : base(limiter, eventsStream, errorsRepository, eventTypeDeterminator, commandParser, filters)
    {
    }

    protected override async ValueTask StartCoreAsync()
    {
        
    }

    protected override async ValueTask StopCoreAsync()
    {
        
    }

    protected override async ValueTask InitializeCoreAsync()
    {
        
    }
}