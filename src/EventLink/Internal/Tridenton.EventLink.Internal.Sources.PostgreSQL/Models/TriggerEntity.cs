using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tridenton.EventLink.Internal.Sources.PostgreSQL.Models;

internal sealed record TriggerEntity
{
    public required string Table { get; init; }
    public bool InsertTriggerApplied { get; init; }
    public bool UpdateTriggerApplied { get; init; }
    public bool DeleteTriggerApplied { get; init; }
}

internal sealed class TriggerEntityConfiguration : IEntityTypeConfiguration<TriggerEntity>
{
    public void Configure(EntityTypeBuilder<TriggerEntity> builder)
    {
        _ = builder.ToTable("tridenton_event_link_triggers");

        _ = builder.HasKey(t => t.Table);

        builder
            .Property(c => c.Table)
            .HasColumnName("table")
            .IsRequired();

        builder
            .Property(c => c.InsertTriggerApplied)
            .HasColumnName("insert_trigger_applied");

        builder
            .Property(c => c.UpdateTriggerApplied)
            .HasColumnName("update_trigger_applied");

        builder
            .Property(c => c.DeleteTriggerApplied)
            .HasColumnName("delete_trigger_applied");
    }
}