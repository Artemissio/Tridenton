namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSettings"></typeparam>
public abstract class EventsListener<TSettings> : IEventsListener
    where TSettings : ISourceSettingsMarker
{
    private readonly IListeningLimiter _limiter;
    private readonly IEventsStream _eventsStream;

    public ListenerStatus Status { get; private set; }
    
    protected EventsListener(IListeningLimiter limiter, IEventsStream eventsStream)
    {
        Status = ListenerStatus.NotStarted;
        
        _limiter = limiter;
        _eventsStream = eventsStream;
    }

    public async ValueTask<Result> StartAsync()
    {
        Status = ListenerStatus.Starting;

        try
        {
            await StartCoreAsync();
            
            Status = ListenerStatus.Started;
            
            return Result.Success;
        }
        catch (Exception exception)
        {
            Status = ListenerStatus.FailedToStart;
            
            return new InternalServerError("", exception.Message);
        }
    }
    
    public async ValueTask<Result> PauseAsync()
    {
        Status = ListenerStatus.Pausing;

        await PauseCoreAsync();
            
        Status = ListenerStatus.Paused;
            
        return Result.Success;
    }

    public async ValueTask<Result> StopAsync()
    {
        Status = ListenerStatus.Stopping;

        await StopCoreAsync();
            
        Status = ListenerStatus.Stopped;
            
        return Result.Success;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual ValueTask PauseCoreAsync()
    {
        return ValueTask.CompletedTask;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected abstract ValueTask StartCoreAsync();
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected abstract ValueTask StopCoreAsync();
}