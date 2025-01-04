namespace Tridenton.EventLink.SDK;

public sealed record DataChangeEventDestination
{
    public Treid DestinationTreid { get; init; }
    public DestinationType Type { get; init; }

    public DataChangeEventDestination()
    {
        DestinationTreid = Treid.Empty;
        Type = DestinationType.None;
    }
}