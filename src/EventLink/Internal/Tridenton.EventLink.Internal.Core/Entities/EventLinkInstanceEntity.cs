namespace Tridenton.Internal.EventLink.Core.Entities;

public sealed record EventLinkInstanceEntity : Entity
{
    public string Title { get; init; }
    public InstanceSettings Settings { get; init; }
    
    public EventLinkInstanceEntity() : base(
        partition: InternalConstants.TridentonWebServicesPartition,
        servicesGroup: "event-link",
        service: "core",
        resourceType: "instances")
    {
        Title = string.Empty;
        Settings = default!;
    }
}