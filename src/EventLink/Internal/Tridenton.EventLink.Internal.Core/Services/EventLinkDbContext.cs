using Microsoft.EntityFrameworkCore;

namespace Tridenton.EventLink.Core.Services;

internal sealed class EventLinkDbContext : DbContext
{
    public DbSet<DataChangeEventEntity> Events { get; init; }
    
    public DbSet<EventLinkInstanceEntity> Instances { get; init; }

    public EventLinkDbContext(DbContextOptions<EventLinkDbContext> options) : base(options)
    {
        
    }
}