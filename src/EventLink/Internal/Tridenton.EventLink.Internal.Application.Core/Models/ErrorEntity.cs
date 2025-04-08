using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tridenton.EventLink.Internal.Application.Core.Models;

/// <summary>
/// 
/// </summary>
public record ErrorEntity
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public HttpStatusCode StatusCode { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Message { get; init; }
}

internal sealed class ErrorEntityConfiguration : IEntityTypeConfiguration<ErrorEntity>
{
    public void Configure(EntityTypeBuilder<ErrorEntity> builder)
    {
        builder.ToTable("errors");
        
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id");

        builder
            .Property(x => x.StatusCode)
            .HasColumnName("status_code");
        
        builder
            .Property(x => x.Message)
            .HasColumnName("message")
            .IsRequired();
    }
}