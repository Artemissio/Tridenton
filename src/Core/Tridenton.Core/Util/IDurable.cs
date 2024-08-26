namespace Tridenton.Core.Util;

/// <summary>
/// Durable abstraction
/// </summary>
public interface IDurable
{
    /// <summary>
    /// Process start UTC
    /// </summary>
    DateTime? StartUtc { get; }

    /// <summary>
    /// Process finish UTC
    /// </summary>
    DateTime? FinishUtc { get; }

    /// <summary>
    /// Total processing time
    /// </summary>
    TimeSpan ProcessingTime { get; }
}

public abstract class Durable : IDurable
{
    public DateTime? StartUtc { get; protected set; }

    public DateTime? FinishUtc { get; protected set; }

    public TimeSpan ProcessingTime
    {
        get
        {
            if (!StartUtc.HasValue)
            {
                return TimeSpan.Zero;
            }

            if (!FinishUtc.HasValue)
            {
                return DateTime.UtcNow - StartUtc.Value;
            }

            return FinishUtc.Value - StartUtc.Value;
        }
    }
}