using Tridenton.Core.Utilities.Collections;

namespace Tridenton.EventLink.Internal.Application.Core.Models;

/// <summary>
/// 
/// </summary>
public sealed record SourceEventContext : Extendable
{
    /// <summary>
    /// 
    /// </summary>
    public required EventType EventType { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required SourceCommand Command { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required SourceCommandParsedPayload ParsedPayload { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public static readonly SourceEventContext Empty = new()
    {
        EventType = EventType.None,
        Command = SourceCommand.Empty,
        ParsedPayload = SourceCommandParsedPayload.Empty,
    };
}

/// <summary>
/// 
/// </summary>
public sealed record SourceCommand : Extendable
{
    /// <summary>
    /// 
    /// </summary>
    public required string CommandText { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required string Collection { get; init; }

    public ReadOnlySpan<string> CommandSegments => CommandText
        .Trim()
        .Split(" ")
        .AsSpan();
    
    /// <summary>
    /// 
    /// </summary>
    public static readonly SourceCommand Empty = new()
    {
        CommandText = string.Empty,
        Collection = string.Empty,
    };
}

/// <summary>
/// 
/// </summary>
public sealed record SourceCommandParsedPayload
{
    /// <summary>
    /// 
    /// </summary>
    public PropertiesCollection[] Properties { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public SourceCommandParsedPayload()
    {
        Properties = [];
    }

    /// <summary>
    /// 
    /// </summary>
    public static readonly SourceCommandParsedPayload Empty = new()
    {
        Properties = []
    };
}