using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tridenton.Core.Utilities;

namespace Tridenton.EventLink.Internal.Sources.PostgreSQL.Models;

/// <summary>
/// 
/// </summary>
internal sealed record ChangeEntity
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required ChangeEntityData Data { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset OccuredOnUtc { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset? ProcessedOnUtc { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string? Error { get; init; }
}

internal sealed class ChangeEntityConfiguration : IEntityTypeConfiguration<ChangeEntity>
{
    public void Configure(EntityTypeBuilder<ChangeEntity> builder)
    {
        builder.ToTable("tridenton_event_link_changes");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasColumnName("id")
            .IsRequired();

        builder
            .Property(c => c.Data)
            .HasColumnName("data")
            .HasColumnType("jsonb") // Explicitly map to JSONB
            .HasConversion<string>(
                data => Serializer.ToJson(data),
                json => Serializer.FromJson<ChangeEntityData>(json)!)
            .IsRequired();

        builder
            .Property(c => c.OccuredOnUtc)
            .HasColumnName("occured_on_utc")
            .IsRequired();

        builder
            .Property(c => c.ProcessedOnUtc)
            .HasColumnName("processed_on_utc");

        builder
            .Property(c => c.Error)
            .HasColumnName("error");
    }
}