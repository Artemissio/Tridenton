namespace Tridenton.Internal.Core.Models;

public abstract record CoreEntity : Entity
{
    protected CoreEntity(string resource) : base("core", string.Empty, string.Empty, resource)
    {
    }
}