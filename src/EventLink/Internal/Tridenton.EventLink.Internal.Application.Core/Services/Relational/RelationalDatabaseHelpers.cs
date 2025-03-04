using System.Collections.Frozen;

namespace Tridenton.EventLink.Internal.Application.Core.Services;

public readonly struct RelationalDatabaseHelpers
{
    public static readonly FrozenSet<RelationalDatabaseStatement> Statements = new List<RelationalDatabaseStatement>
    {
        new(RelationalDatabaseCommand.Insert, EventType.Create, 2),
        new(RelationalDatabaseCommand.Update, EventType.Update, 1),
        new(RelationalDatabaseCommand.Delete, EventType.Delete, 2),
    }.ToFrozenSet();
}

public sealed record RelationalDatabaseStatement(RelationalDatabaseCommand Command, EventType EventType, int CollectionNameIndex);