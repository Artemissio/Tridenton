using Tridenton.Core.Models;

namespace Tridenton.SDK.Core.Models;

public abstract class TridentonClient : IDisposable
{
    private readonly HttpClient _httpClient;

    public TridentonClient()
    {
        _httpClient = new();
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
        GC.SuppressFinalize(this);
    }
}
