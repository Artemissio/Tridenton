namespace Tridenton.SDK.Core.Auth;

public sealed record TridentonCredentials
{
    /// <summary>
    /// Gets the AccessKey property for the current credentials.
    /// </summary>
    public string AccessKeyId { get; }

    /// <summary>
    /// Gets the SecretKey property for the current credentials.
    /// </summary>
    public string SecretAccessKey { get; }

    /// <summary>
    /// Gets the Token property for the current credentials.
    /// </summary>
    public string? Token { get; }
    
    public TridentonCredentials(string accessKeyId, string secretAccessKey, string? token = null)
    {
        if (string.IsNullOrWhiteSpace(accessKeyId))
        {
            throw new ArgumentNullException(nameof(accessKeyId));
        }

        if (string.IsNullOrWhiteSpace(secretAccessKey))
        {
            throw new ArgumentNullException(nameof(secretAccessKey));
        }

        AccessKeyId = accessKeyId;
        SecretAccessKey = secretAccessKey;

        Token = token ?? string.Empty;
    }
    
    public static readonly TridentonCredentials Anonymous = new(string.Empty, string.Empty);
}