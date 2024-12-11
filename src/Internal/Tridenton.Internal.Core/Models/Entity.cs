using System.ComponentModel.DataAnnotations.Schema;

namespace Tridenton.Internal.Core.Models;

/// <summary>
/// 
/// </summary>
public abstract record Entity
{
    [NotMapped]
    protected string Partition { get; }
    
    [NotMapped]
    protected string ServicesGroup { get; }
    
    [NotMapped]
    protected string Service { get; }
    
    [NotMapped]
    protected string Resource { get; }
    
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
    public Account User { get; init; }
    
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

    // [NotMapped]
    // public Treid Treid
    // {
    //     get
    //     {
    //         return new Treid(
    //             partition: Partition,
    //             servicesGroup: ServicesGroup,
    //             service: Service,
    //             )
    //     }
    // }
    
    protected Entity(string partition, string servicesGroup, string service, string resource)
    {
        Partition = partition;
        ServicesGroup = servicesGroup;
        Service = service;
        Resource = resource;
    }
}