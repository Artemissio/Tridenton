using Tridenton.Core;

namespace Tridenton.SDK.Core.Auth;

public sealed class EnvironmentVariablesCredentialsFetcher : CredentialsFetcher
{
    // Standard across all Tridenton SDKs that support reading keys from environment variables
    public const string AccessKeyIdVariable = "TRIDENTON_ACCESS_KEY_ID";
    public const string SecretAccessKeyVariable = "TRIDENTON_SECRET_ACCESS_KEY";
    public const string SessionTokenVariable = "TRIDENTON_SESSION_TOKEN";
    
    public override Result<TridentonCredentials> FetchCredentials()
    {
        var accessKeyId = Environment.GetEnvironmentVariable(AccessKeyIdVariable);

        if (string.IsNullOrWhiteSpace(accessKeyId))
        {
            return new BadRequestError("SDK.MissingEnvironmentVariable", $"{AccessKeyIdVariable} is missing.");
        }
        
        var secretAccessKey = Environment.GetEnvironmentVariable(SecretAccessKeyVariable);

        if (string.IsNullOrWhiteSpace(secretAccessKey))
        {
            return new BadRequestError("SDK.MissingEnvironmentVariable", $"{SecretAccessKeyVariable} is missing.");
        }
        
        var sessionToken = Environment.GetEnvironmentVariable(SessionTokenVariable);
        
        return new TridentonCredentials(accessKeyId, secretAccessKey, sessionToken);
    }
}