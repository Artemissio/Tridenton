namespace Tridenton.EventLink.SDK;

public struct DataChangeEventTimestamps
{
    /// <summary>
    /// Timestamp in UTC format when the event initially happened in the data source
    /// </summary>
    public DateTime EventUtc { get; private set; }
    
    /// <summary>
    /// Timestamp in UTC format when the event was read and handled by EventLink
    /// </summary>
    public DateTime HandleUtc { get; private set; }
    
    /// <summary>
    /// Timestamp in UTC format when the event was emitted by EventLink to the destination
    /// </summary>
    public DateTime EmissionUtc { get; private set; }

    internal void SetEventUtc()
    {
        EventUtc = DateTime.UtcNow;
    }

    internal void SetHandleUtc()
    {
        HandleUtc = DateTime.UtcNow;
    }

    internal void SetEmissionUtc()
    {
        EmissionUtc = DateTime.UtcNow;
    }
}