using Tridenton.SDK.Core.Auth;

namespace Tridenton.SDK.Core;

public abstract class TridentonClient : IDisposable
{
    private readonly HttpClient _httpClient;

    protected TridentonCredentials Credentials { get; private set; }

    private TridentonClient()
    {
        _httpClient = new HttpClient();

        Credentials = TridentonCredentials.Anonymous;
    }

    protected TridentonClient(TridentonCredentials credentials) : this()
    {
        Credentials = credentials;
    }

    protected TridentonClient(CredentialsFetcher fetcher) : this()
    {
        var credentialsFetchResult = fetcher.FetchCredentials();

        if (credentialsFetchResult.Failed)
        {
            throw new Exception(credentialsFetchResult.Error!.Description);
        }
        
        Credentials = credentialsFetchResult.Value;
    }

    // protected async ValueTask<Result<TResponse>> SendRequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
    //     where TRequest : class
    //     where TResponse : class
    // {
    //     return null;
    // }

    public void Dispose()
    {
        _httpClient.Dispose();
        Credentials = TridentonCredentials.Anonymous;
        GC.SuppressFinalize(this);
    }
}
