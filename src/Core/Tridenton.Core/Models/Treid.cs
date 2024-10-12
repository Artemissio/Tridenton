using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Tridenton.Core;

/// <summary>
/// <b>T</b>ridenton <b>RE</b>source <b>ID</b>
/// </summary>
[JsonConverter(typeof(TreidJsonConverter))]
public record Treid : IParsable<Treid>
{
    /// <summary>
    /// Partition
    /// </summary>
    public readonly string Partition;

    /// <summary>
    /// Account ID
    /// </summary>
    public readonly string Account;

    /// <summary>
    /// Services group
    /// </summary>
    public readonly string ServicesGroup;

    /// <summary>
    /// Service
    /// </summary>
    public readonly string Service;

    /// <summary>
    /// Resource type
    /// </summary>
    public readonly string ResourceType;

    /// <summary>
    /// Resource ID or "*"
    /// </summary>
    public readonly string ResourceId;

    /// <summary>
    /// Defines whether specified resource does not belong to specific user
    /// </summary>
    public bool IsAccountIndependent => string.IsNullOrWhiteSpace(Account);

    /// <summary>
    /// Defines whether specified resource does not belong to specific service
    /// </summary>
    public bool IsGeneralResource => string.IsNullOrWhiteSpace(Service);

    /// <summary>
    /// Initializes a new instance of <see cref="Treid"/>
    /// </summary>
    /// <param name="partition">Partition</param>
    /// <param name="account">Account number</param>
    /// <param name="servicesGroup">Services group</param>
    /// <param name="service">Service</param>
    /// <param name="resourceType">Resource type</param>
    /// <param name="resourceId">Resource Id</param>
    public Treid(string partition, string account, string servicesGroup, string service, string resourceType, string resourceId = Constants.Wildcard)
    {
        Partition = partition;
        Account = account;
        ServicesGroup = servicesGroup;
        Service = service;
        ResourceType = resourceType;
        ResourceId = resourceId;
    }

    public virtual bool Equals(Treid? other)
    {
        if (other is null)
        {
            return false;
        }

        if (other.Partition != Partition)
        {
            return false;
        }

        if (other.Account != Account)
        {
            return false;
        }

        if (other.ServicesGroup != ServicesGroup)
        {
            return false;
        }

        if (other.Service != Service)
        {
            return false;
        }

        if (other.ResourceType != ResourceType)
        {
            return false;
        }

        return other.ResourceId == ResourceId;
    }

    /// <summary>
    /// Converts current TREID structure to the following string format:
    /// <code>
    ///     treid:{partition}:{account}:{services-group}:{service}:{resource-type}:{resource-id}
    /// </code>
    /// </summary>
    /// <returns>String representation of <see cref="Treid"/></returns>'
    public override string ToString()
    {
        return new StringBuilder()
            .Append(Constants.Treid)
            .Append(Constants.TreidDelimiter)
            .Append(Partition)
            .Append(Constants.TreidDelimiter)
            .Append(Account)
            .Append(Constants.TreidDelimiter)
            .Append(ServicesGroup)
            .Append(Constants.TreidDelimiter)
            .Append(Service)
            .Append(Constants.TreidDelimiter)
            .Append(ResourceType)
            .Append(Constants.TreidDelimiter)
            .Append(ResourceId)
            .ToString();
    }
    
    /// <summary>
    /// Returns the hash code for this instance
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => ToString().GetHashCode();

    /// <summary>
    /// A read-only instance of the <see cref="Treid"/> structure whose value is all empty strings.
    /// </summary>
    public static readonly Treid Empty = new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
    
    /// <summary>
    /// Converts the string representation of TREID to the equivalent <see cref="Treid"/> structure.
    /// </summary>
    /// <param name="input">The string to convert</param>
    /// <param name="provider">Format provider (optional)</param>
    /// <returns>A structure that contains the value that was parsed</returns>
    public static Treid Parse(string? input, IFormatProvider? provider)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new MalformedTreidException();
        }

        var segments = input
            .Split(Constants.TreidDelimiter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .AsSpan();

        if (segments.Length != 7)
        {
            throw new MalformedTreidException();
        }

        if (segments[0] != Constants.TreidDelimiter)
        {
            throw new MalformedTreidException();
        }

        var partition = segments[1];
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new MalformedTreidException("no Partition specified");
        }

        var account = segments[2];
        var servicesGroup = segments[3];
        var service = segments[4];

        var resourceType = segments[5];
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new MalformedTreidException("no Resource type specified");
        }

        var resourceIdString = segments[6];
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new MalformedTreidException("no Resource Id specified");
        }

        return new Treid(partition, account, servicesGroup, service, resourceType, resourceIdString);
    }

    /// <summary>
    /// Converts the string representation of TREID to the equivalent <see cref="Treid"/> structure.
    /// </summary>
    /// <param name="input">A string containing the TREID to convert</param>
    /// <param name="provider">Format provider (optional)</param>
    /// <param name="result">A Treid instance to contain the parsed value. If the method returns true, result contains a valid Treid. If the method returns false, result equals <see cref="Empty"/></param>
    /// <returns><see langword="true"/> if the parse operation was successful; otherwise, <see langword="false"/></returns>
    public static bool TryParse([NotNullWhen(true)] string? input, IFormatProvider? provider, [MaybeNullWhen(false)] out Treid result)
    {
        try
        {
            result = IsTreid(input)
                ? Parse(input, provider)
                : Empty;
        }
        catch (MalformedTreidException)
        {
            result = Empty;
        }

        return !result.Equals(Empty);
    }
    
    /// <summary>
    /// Returns <see langword="true"/> if the string appears to be an TREID by seeing if the string starts with "treid:".
    /// This method doesn't guarantee the string is a valid TREID.
    /// <para>
    /// To validate the string call <see cref="TryParse"/>f
    /// </para>
    /// </summary>
    /// <param name="input">Specified string</param>
    /// <returns><see langword="true"/> if the string starts with "treid:"; otherwise, <see langword="false"/></returns>
    private static bool IsTreid(string? input) => !string.IsNullOrWhiteSpace(input) && input.StartsWith($"{Constants.Treid}{Constants.TreidDelimiter}");
}
