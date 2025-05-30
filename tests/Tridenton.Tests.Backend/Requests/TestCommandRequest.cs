using Tridenton.Core;
using Tridenton.Internal.Core.CQRS;

namespace Tridenton.Tests.Backend.Requests;

public sealed record TestCommandRequest(int CommandId) : ITridentonRequest;

internal sealed class TestCommandRequestHandler : IRequestHandler<TestCommandRequest>
{
    public async Task<Result> HandleAsync(TestCommandRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(1_000, cancellationToken);
        
        return Result.Success;
    }
}