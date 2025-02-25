namespace Tridenton.EventLink.Internal.Application.Core.Services.Internal;

/// <summary>
/// 
/// </summary>
internal sealed record EventsStreamingContext : IEventsStreamingContext
{
    /// <summary>
    /// 
    /// </summary>
    public DataChangeEvent Payload { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    public EventsStreamingContext(DataChangeEvent payload)
    {
        ArgumentNullException.ThrowIfNull(payload);
        
        Payload = payload;
    }
}