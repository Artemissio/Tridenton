namespace Tridenton.Core.Util;

public interface IDurable
{
    DateTime? StartUtc { get; }

    DateTime? FinishUtc { get; }

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