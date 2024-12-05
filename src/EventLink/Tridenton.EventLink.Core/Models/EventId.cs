using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Tridenton.EventLink.Core;

[DebuggerDisplay("{_value}")]
public readonly struct EventId : IParsable<EventId>
{
    private readonly Ulid _value;

    private EventId(Ulid value)
    {
        _value = value;
    }

    public override string ToString() => _value.ToString();

    public static EventId NewId() => new(Ulid.NewUlid());
    public static readonly EventId Empty = new(Ulid.Empty);

    public static EventId Parse(string s, IFormatProvider? provider)
    {
        return TryParse(s, provider, out var id) ? id : Empty;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out EventId result)
    {
        if (Ulid.TryParse(s, provider, out var ulid))
        {
            result = new EventId(ulid);
            return true;
        }

        result = Empty;
        return false;
    }
}