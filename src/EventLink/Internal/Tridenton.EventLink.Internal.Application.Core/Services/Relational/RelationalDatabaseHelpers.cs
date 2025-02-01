using System.Collections.Frozen;

namespace Tridenton.EventLink.Internal.Application.Core.Services;

public readonly struct RelationalDatabaseHelpers
{
    public static readonly FrozenSet<RelationalDatabaseStatement> Statements = new List<RelationalDatabaseStatement>
    {
        new("INSERT", EventType.Create, 2),
        new("UPDATE", EventType.Update, 1),
        new("DELETE", EventType.Delete, 2),
    }.ToFrozenSet();
}

public sealed record RelationalDatabaseStatement(string Statement, EventType EventType, int CollectionNameIndex);