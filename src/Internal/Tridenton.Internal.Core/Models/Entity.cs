using System.ComponentModel.DataAnnotations.Schema;

namespace Tridenton.Internal.Core.Models;

/// <summary>
/// 
/// </summary>
public abstract record Entity
{
    [NotMapped]
    private readonly string _partition;

    [NotMapped]
    private readonly string _servicesGroup;

    [NotMapped]
    private readonly string _service;

    [NotMapped]
    private readonly string _resourceType;
    
    /// <summary>
    /// 
    /// </summary>
    public EntityId Id { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public EntityId UserId { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public User User { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset CreatedUtc { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset LastUpdateUtc { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset? DeleteUtc { get; init; }

    [NotMapped]
    public Treid Treid =>
        new(
            partition: _partition,
            account: User.Account,
            servicesGroup: _servicesGroup,
            service: _service,
            resourceType: _resourceType,
            resourceId: Id.ToString()!);

    protected Entity(string partition, string servicesGroup, string service, string resourceType)
    {
        _partition = partition;
        _servicesGroup = servicesGroup;
        _service = service;
        _resourceType = resourceType;

        User = default!;
    }
}