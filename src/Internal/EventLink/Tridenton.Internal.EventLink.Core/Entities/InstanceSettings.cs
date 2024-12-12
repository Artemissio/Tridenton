namespace Tridenton.Internal.EventLink.Core.Entities;

public sealed record InstanceSettings
{
    public DestinationSettings DestinationSettings { get; init; }

    public InstanceSettings()
    {
        DestinationSettings = DestinationSettings.None;
    }
}

public abstract record DestinationSettings
{
    public DestinationType Type { get; init; }

    protected DestinationSettings()
    {
        Type = DestinationType.None;
    }

    public static readonly DestinationSettings None = new DefaultDestinationSettings();

    private sealed record DefaultDestinationSettings : DestinationSettings { }
}