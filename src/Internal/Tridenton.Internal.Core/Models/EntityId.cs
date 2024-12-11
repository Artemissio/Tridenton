using System.Diagnostics.CodeAnalysis;

namespace Tridenton.Internal.Core.Models;

public readonly struct EntityId : IParsable<EntityId>
{
    private readonly Ulid _value;

    private EntityId(Ulid value)
    {
        _value = value;
    }

    public static EntityId NewId() => new(Ulid.NewUlid());
    public static readonly EntityId Empty = new(Ulid.Empty);

    public static EntityId Parse(string s, IFormatProvider? provider)
    {
        return TryParse(s, provider, out var id) ? id : Empty;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out EntityId result)
    {
        if (Ulid.TryParse(s, provider, out var ulid))
        {
            result = new EntityId(ulid);
            return true;
        }

        result = Empty;
        return false;
    }
}