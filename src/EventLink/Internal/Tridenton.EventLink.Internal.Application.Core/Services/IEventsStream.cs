namespace Tridenton.EventLink.Internal.Application.Core.Services;

/// <summary>
/// 
/// </summary>
public interface IEventsStream
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="payload"></param>
    /// <returns></returns>
    ValueTask WriteAsync(DataChangeEventPayload payload);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    ValueTask<IEventsStreamingContext> ReadAsync();
}