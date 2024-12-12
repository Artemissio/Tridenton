namespace Tridenton.Internal.Core.Models;

public sealed record User : CoreEntity
{
    public string Account { get; init; }
    
    public User() : base("users")
    {
        Account = string.Empty;
    }
}