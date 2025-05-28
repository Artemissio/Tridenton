using System.Diagnostics.CodeAnalysis;

namespace Tridenton.Core;

public readonly struct RequestId : IParsable<RequestId>, IEquatable<RequestId>
{
    private readonly Ulid _value;

    private RequestId(Ulid value)
    {
        _value = value;
    }

    public override string ToString() => _value.ToString();
    
    public override int GetHashCode() => _value.GetHashCode();
    
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj switch
        {
            RequestId requestId => _value == requestId._value,
            Ulid ulid => _value == ulid,
            string stringValue => Ulid.TryParse(stringValue, out var parsed) && _value == parsed,
            _ => false
        };
    }

    public bool Equals(RequestId other)
    {
        return _value == other._value;
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

    public static bool operator ==(RequestId left, RequestId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(RequestId left, RequestId right)
    {
        return !left.Equals(right);
    }
}