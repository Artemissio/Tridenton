namespace Tridenton.EventLink.Internal.Application.Core.Services.Internal;

internal sealed class EventsErrorsRepository : IEventsErrorsRepository
{
    public async ValueTask PersistErrorAsync(Error error)
    {
        // TODO
        
        await ValueTask.CompletedTask;
    }
}