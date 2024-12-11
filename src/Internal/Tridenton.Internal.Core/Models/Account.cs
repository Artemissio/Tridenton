namespace Tridenton.Internal.Core.Models;

public sealed record Account : Entity
{
    public Account(string partition, string servicesGroup, string service, string resource) : base(partition, servicesGroup, service, resource)
    {
    }
}