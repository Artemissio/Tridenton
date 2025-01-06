using Tridenton.Core;

namespace Tridenton.SDK.Core.Auth;

/// <summary>
/// 
/// </summary>
public abstract class CredentialsFetcher
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract Result<TridentonCredentials> FetchCredentials();
}