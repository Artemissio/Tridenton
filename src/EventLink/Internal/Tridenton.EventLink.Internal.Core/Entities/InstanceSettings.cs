using Tridenton.EventLink.SDK.Destinations;
using Tridenton.EventLink.SDK.Sources;

namespace Tridenton.EventLink.Internal.Core.Entities;

public sealed record InstanceSettings
{
    public SourceSettings Source { get; init; }
    public DestinationSettings Destination { get; init; }

    public InstanceSettings()
    {
        Source = new();
        Destination = new();
    }
}