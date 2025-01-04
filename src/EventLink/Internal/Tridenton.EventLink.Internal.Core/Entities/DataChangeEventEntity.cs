namespace Tridenton.Internal.EventLink.Core.Entities;

public sealed record DataChangeEventEntity : Entity
{
    public DataChangeEventEntity() : base(
        partition: InternalConstants.TridentonWebServicesPartition,
        servicesGroup: "event-link",
        service: "core",
        resourceType: "events")
    {
    }
}