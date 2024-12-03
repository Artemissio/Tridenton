using System.Diagnostics.CodeAnalysis;

namespace Tridenton.Core;

public readonly struct RequestId : IParsable<RequestId>
{
    private readonly Ulid _value;

    private RequestId(Ulid value)
    {
        _value = value;
    }

    public static RequestId NewId() => new(Ulid.NewUlid());
    public static readonly RequestId Empty = new(Ulid.Empty);

    public static RequestId Parse(string s, IFormatProvider? provider)
    {
        return TryParse(s, provider, out var id) ? id : Empty;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out RequestId result)
    {
        if (Ulid.TryParse(s, provider, out var ulid))
        {
            result = new RequestId(ulid);
            return true;
        }

        result = Empty;
        return false;
    }
}