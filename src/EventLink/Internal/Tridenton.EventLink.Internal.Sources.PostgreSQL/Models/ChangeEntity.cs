using Tridenton.EventLink.Internal.Application.Core.Services;

namespace Tridenton.EventLink.Internal.Sources.PostgreSQL.Models;

internal sealed record ChangeEntity
{
    public Guid Id { get; init; }
    
    public required RelationalDatabaseStatement Statement { get; init; }
    
    public required string Data { get; init; }

    public DateTimeOffset OccuredOnUtc { get; init; }
    
    public DateTimeOffset? ProcessedOnUtc { get; init; }

    public string? Error { get; init; }
}