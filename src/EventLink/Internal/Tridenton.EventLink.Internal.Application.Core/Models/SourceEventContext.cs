using Tridenton.Core.Utilities.Collections;

namespace Tridenton.EventLink.Internal.Application.Core.Models;

/// <summary>
/// 
/// </summary>
public sealed record SourceEventContext
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
public sealed record SourceCommand
{
    public required string CommandText { get; init; }

    public static readonly SourceCommand Empty = new()
    {
        CommandText = string.Empty,
    };
}

/// <summary>
/// 
/// </summary>
public sealed record SourceCommandParsedPayload
{
    public PropertiesCollection[] Properties { get; init; }

    public SourceCommandParsedPayload()
    {
        Properties = [];
    }

    public static readonly SourceCommandParsedPayload Empty = new()
    {
        Properties = []
    };
}