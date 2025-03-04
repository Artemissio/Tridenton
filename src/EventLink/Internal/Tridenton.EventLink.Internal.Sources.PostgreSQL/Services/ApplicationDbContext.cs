using Microsoft.EntityFrameworkCore;
using Tridenton.EventLink.Internal.Application.Core.Services;
using Tridenton.EventLink.Internal.Sources.PostgreSQL.Models;

namespace Tridenton.EventLink.Internal.Sources.PostgreSQL.Services;

internal sealed class ApplicationDbContext : DbContext
{
    private readonly IEventLinkSettingsProvider _eventLinkSettingsProvider;
    
    public DbSet<ChangeEntity> Changes { get; set; }
    
    public DbSet<TriggerEntity> Triggers { get; set; }

    public ApplicationDbContext(
        DbContextOptions options,
        IEventLinkSettingsProvider eventLinkSettingsProvider) : base(options)
    {
        _eventLinkSettingsProvider = eventLinkSettingsProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var eventLinkSettings = _eventLinkSettingsProvider.GetSettings();
        
        modelBuilder.HasDefaultSchema(eventLinkSettings.SourceSettings.PostgreSQL!.Schema);

        modelBuilder.ApplyConfiguration(new ChangeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TriggerEntityConfiguration());
    }
}