namespace Tridenton.EventLink.Internal.Core.Entities;

public sealed record EventLinkInstanceEntity : Entity
{
    public string Title { get; init; }
    public InstanceSettings Settings { get; init; }
    
    public EventLinkInstanceEntity() : base(
        partition: InternalConstants.Partitions.Horizon,
        servicesGroup: "event-link",
        service: "core",
        resourceType: "instances")
    {
        Title = string.Empty;
        Settings = default!;
    }
}