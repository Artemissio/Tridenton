using System.Diagnostics.CodeAnalysis;

namespace Tridenton.Core.Models;

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
    /// <param name="partition"></param>
    /// <param name="account"></param>
    /// <param name="service"></param>
    /// <param name="resourceType"></param>
    /// <param name="resourceId"></param>
    public Treid(string partition, string account, string service, string resourceType, string resourceId)
    {
        Partition = partition;
        Account = account;
        Service = service;
        ResourceType = resourceType;
        ResourceId = resourceId;
    }

    public Treid(string partition, string accountID, string service, string resourceType) : this(partition, accountID, service, resourceType, Constants.Wildcard) { }

    /// <summary>
    /// A read-only instance of the <see cref="Treid"/> structure whose value is all empty strings.
    /// </summary>
    public static Treid Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

    /// <summary>
    /// Converts current TREID structure to string format "treid:partition:account:service:resource-type:resource-id"
    /// </summary>
    /// <returns>String representation of <see cref="Treid"/></returns>'
    public override string ToString() => $"{Constants.Treid}{Constants.TreidDelimiter}{Partition}{Constants.TreidDelimiter}{Account}{Constants.TreidDelimiter}{Service}{Constants.TreidDelimiter}{ResourceType}{Constants.TreidDelimiter}{ResourceId}";

    /// <summary>
    /// Returns the hash code for this instance
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => ToString().GetHashCode();

    /// <summary>
    /// Returns <see langword="true"/> if the string appears to be an TREID by seeing if the string starts with "treid:".
    /// This method doesn't guarantee the string is a valid TREID. To validate the string call <see cref="TryParse(string, out Treid?)"/>
    /// </summary>
    /// <param name="input">Specified string</param>
    /// <returns><see langword="true"/> if the string starts with "treid:"; otherwise, <see langword="false"/></returns>
    public static bool IsTreid(string? input) => !string.IsNullOrWhiteSpace(input) && input!.StartsWith($"{Constants.Treid}{Constants.TreidDelimiter}");

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

        var segments = input!
            .Split(Constants.TreidDelimiter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .AsSpan();

        if (segments.Length != 6)
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
        var service = segments[3];

        var resourceType = segments[4];
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new MalformedTreidException("no Resource type specified");
        }

        var resourceIdString = segments[5];
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new MalformedTreidException("no Resource Id specified");
        }
        
        if (resourceIdString != Constants.Wildcard && (!Ulid.TryParse(resourceIdString, out var resourceId) || resourceId == Ulid.Empty))
        {
            throw new MalformedTreidException("Resource Id is invalid");
        }

        return new Treid(partition, account, service, resourceType, resourceIdString);
    }

    /// <summary>
    /// Converts the string representation of TREID to the equivalent <see cref="Treid"/> structure.
    /// </summary>
    /// <param name="input">A string containing the TREID to convert</param>
    /// <param name="provider">Format provider (optional)</param>
    /// <param name="treid">A Treid instance to contain the parsed value. If the method returns true, result contains a valid Treid. If the method returns false, result equals <see cref="Empty"/></param>
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
}
