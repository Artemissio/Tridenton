using System.Text.Json;
using System.Text.Json.Serialization;
using Tridenton.EventLink.Internal.Application.Core.Services;

namespace Tridenton.EventLink.Internal.Sources.PostgreSQL.Models;

/// <summary>
/// 
/// </summary>
internal sealed record ChangeEntityData
{
    /// <summary>
    /// 
    /// </summary>
    public required string Table { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required RelationalDatabaseCommand Command { get; init; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public JsonElement? OldRecord { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required JsonElement NewRecord { get; init; }
}