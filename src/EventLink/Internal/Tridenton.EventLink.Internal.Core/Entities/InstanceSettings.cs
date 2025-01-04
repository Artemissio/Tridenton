namespace Tridenton.Internal.EventLink.Core.Entities;

public sealed record InstanceSettings
{
    public SourceSettings Source { get; init; }
    public DestinationSettings Destination { get; init; }

    public InstanceSettings()
    {
        Source = SourceSettings.None;
        Destination = DestinationSettings.None;
    }
}

public abstract record SourceSettings
{
    public SourceType Type { get; init; }

    protected SourceSettings()
    {
        Type = SourceType.None;
    }

    public static readonly SourceSettings None = new DefaultSourceSettings();

    private sealed record DefaultSourceSettings : SourceSettings { }
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