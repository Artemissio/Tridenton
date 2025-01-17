using System.Text.Json.Serialization;

namespace Tridenton.EventLink.Internal.Application.Core.Models;

/// <summary>
/// 
/// </summary>
public sealed record StreamSnapshot
{
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset Utc { get; }

    /// <summary>
    /// 
    /// </summary>
    public StreamStatus Status { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public int Capacity { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public int AmountOfEvents { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public int RemainingCapacity { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amountOfEvents"></param>
    /// <param name="status"></param>
    /// <param name="capacity"></param>
    /// <param name="remainingCapacity"></param>
    [JsonConstructor]
    public StreamSnapshot(StreamStatus status, int capacity, int amountOfEvents, int remainingCapacity)
    {
        Utc = DateTimeOffset.UtcNow;
        Status = status;
        Capacity = capacity;
        AmountOfEvents = amountOfEvents;
        RemainingCapacity = remainingCapacity;
    }
}