using Tridenton.Core;
using Tridenton.Internal.Core.CQRS;

namespace Tridenton.Tests.Backend.Requests;

public sealed record GetTestDataRequest : ITridentonRequest<GetTestDataResponse>;

public sealed record GetTestDataResponse(DateTimeOffset Utc)
{
    public DateTimeOffset Start { get; set; }
    
    public TimeSpan Duration => Utc - Start;
}

internal sealed class GetTestDataRequestHandler : IRequestHandler<GetTestDataRequest, GetTestDataResponse>
{
    public async Task<Result<GetTestDataResponse>> HandleAsync(GetTestDataRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return new GetTestDataResponse(DateTimeOffset.UtcNow);
    }
}
