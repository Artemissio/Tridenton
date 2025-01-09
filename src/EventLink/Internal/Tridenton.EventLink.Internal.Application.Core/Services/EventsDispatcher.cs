namespace Tridenton.EventLink.Internal.Application.Core.Services;

public abstract class EventsDispatcher<TSettings> : IEventsDispatcher
    where TSettings : IDestinationSettingsMarker
{
    protected readonly IEventLinkSettingsProvider SettingsProvider;

    protected EventsDispatcher(IEventLinkSettingsProvider settingsProvider)
    {
        SettingsProvider = settingsProvider;
    }
    
    public async ValueTask<Result> DispatchEventsAsync(DataChangeEventPayload payload)
    {
        try
        {
            await DispatchEventsCoreAsync(payload);
            
            return Result.Success;
        }
        catch (Exception exception)
        {
            return new InternalServerError("", exception.Message);
        }
    }
    
    protected abstract ValueTask DispatchEventsCoreAsync(DataChangeEventPayload payload);
}