namespace Tridenton.Internal.Core.Models;

public abstract record CoreEntity : Entity
{
    protected CoreEntity(string resource) : base(
        partition: "core",
        servicesGroup: string.Empty,
        service: string.Empty,
        resourceType: resource)
    {
    }
}