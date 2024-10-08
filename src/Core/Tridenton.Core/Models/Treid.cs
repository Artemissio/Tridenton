using System.Diagnostics.CodeAnalysis;

namespace Tridenton.Core.Models;

public sealed record InvalidTreidError : BadRequestError
{
    public InvalidTreidError() : base("InvalidTreid", $"TREID is in incorrect format. TREID format is: {Constants.Treid}{Constants.TreidDelimiter}<partition>{Constants.TreidDelimiter}<account-id>{Constants.TreidDelimiter}<service>{Constants.TreidDelimiter}<resource-type>{Constants.TreidDelimiter}<resource-id>")
    {
        
    }

    public InvalidTreidError(string message) : base("InvalidTreid", $"Malformed TREID - {message}")
    {

    }
}

/// <summary>
/// <b>T</b>ridenton <b>RE</b>source <b>ID</b>
/// </summary>
[JsonConverter(typeof(TreidJsonConverter))]
public record Treid
{
    /// <summary>
    /// Partition
    /// </summary>
    public readonly string Partition;

    /// <summary>
    /// Account ID
    /// </summary>
    public readonly string AccountId;

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
    public bool IsAccountIndependent => AccountId.IsEmpty();

    /// <summary>
    /// Defines whether specified resource does not belong to specific service
    /// </summary>
    public bool IsGeneralResource => Service.IsEmpty();

    /// <summary>
    /// Initializes a new instance of <see cref="Treid"/>
    /// </summary>
    /// <param name="partition"></param>
    /// <param name="accountId"></param>
    /// <param name="service"></param>
    /// <param name="resourceType"></param>
    /// <param name="resourceId"></param>
    public Treid(string partition, string accountId, string service, string resourceType, string resourceId)
    {
        Partition = partition;
        AccountId = accountId;
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
    /// Converts current TREID structure to string format "treid:partition:account-id:service:resource-type:resource-id"
    /// </summary>
    /// <returns>String representation of <see cref="Treid"/></returns>'
    public override string ToString() => $"{Constants.Treid}{Constants.TreidDelimiter}{Partition}{Constants.TreidDelimiter}{AccountId}{Constants.TreidDelimiter}{Service}{Constants.TreidDelimiter}{ResourceType}{Constants.TreidDelimiter}{ResourceId}";

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
    public static bool IsTreid(string input) => !input.IsEmpty() && input.StartsWith($"{Constants.Treid}{Constants.TreidDelimiter}");

    /// <summary>
    /// Converts the string representation of TREID to the equivalent <see cref="Treid"/> structure.
    /// </summary>
    /// <param name="input">A string containing the TREID to convert</param>
    /// <param name="treid">A Treid instance to contain the parsed value. If the method returns true, result contains a valid Treid. If the method returns false, result equals <see cref="Empty"/></param>
    /// <returns><see langword="true"/> if the parse operation was successful; otherwise, <see langword="false"/></returns>
    public static bool TryParse(string? input, out Treid treid)
    {
        var parseResult = Parse(input);

        if (parseResult.Failed)
        {
            treid = Empty;
            return false;
        }

        treid = parseResult.Value;
        return !treid.Equals(Empty);
    }

    /// <summary>
    /// Converts the string representation of TREID to the equivalent <see cref="Treid"/> structure.
    /// </summary>
    /// <param name="input">The string to convert</param>
    /// <returns>A structure that contains the value that was parsed</returns>
    public static Result<Treid> Parse(string? input)
    {
        if (input.IsEmpty())
        {
            return new InvalidTreidError();
        }

        var segments = input!
            .Split(Constants.TreidDelimiter)
            .AsSpan();

        if (segments.Length != 6)
        {
            return new InvalidTreidError();
        }

        if (segments[0] != Constants.TreidDelimiter)
        {
            return new InvalidTreidError();
        }

        var partition = segments[1];
        if (partition.IsEmpty())
        {
            return new InvalidTreidError("no partition specified");
        }

        var accountID = segments[2];
        var service = segments[3];

        var resourceType = segments[4];
        if (resourceType.IsEmpty())
        {
            return new InvalidTreidError("no resource type specified");
        }

        string resourceIDString = segments[5];
        if (resourceIDString.IsEmpty())
        {
            return new InvalidTreidError("no resource Id specified");
        }
        
        if (resourceIDString != Constants.Wildcard && (!Ulid.TryParse(resourceIDString, out var resourceId) || resourceId == Ulid.Empty))
        {
            return new InvalidTreidError("resource ID is not valid");
        }

        return new Treid(partition, accountID, service, resourceType, resourceIDString);
    }
}
