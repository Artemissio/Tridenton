using System.Collections.Concurrent;

namespace Tridenton.Core.Operations.Internal;

internal sealed class OperationsFlowsManager : IOperationsFlowsManager
{
    private readonly ConcurrentDictionary<Ulid, IOperationsFlow> _flows;
    private readonly ConcurrentDictionary<Ulid, CancellationTokenSource> _flowsTokens;

    public OperationsFlowsManager()
	{
        _flows = new();
        _flowsTokens = new();
	}

    public ValueTask<Result<GetFlowResponse>> GetFlowAsync(GetFlowRequest request, CancellationToken cancellationToken = default)
    {
        Result<GetFlowResponse> result = _flows.TryGetValue(request.FlowId, out var pipeline)
            ? new GetFlowResponse(pipeline)
            : new FlowNotFoundError(request.FlowId);

        return ValueTask.FromResult(result);
    }

    public async ValueTask<IOperationsFlow> StartNewFlowAsync(OperationsFlowContext context)
    {
        var pipeline = new OperationsFlow(context);
        var cts = new CancellationTokenSource();

        pipeline.OnFlowCompleted -= OnPipelineCompleted;
        pipeline.OnFlowCompleted += OnPipelineCompleted;

        _flows[pipeline.Id] = pipeline;
        _flowsTokens[pipeline.Id] = cts;

        await pipeline.ExecuteAsync(cts.Token);

        return pipeline;
    }

    public async ValueTask<Result> CancelFlowAsync(CancelFlowRequest request)
    {
        var pipelineResult = await GetFlowAsync(new GetFlowRequest(request.PipelineId));

        if (pipelineResult.Failed)
        {
            return pipelineResult.Error!;
        }

        _flowsTokens[request.PipelineId].Cancel();

        return Result.Success;
    }

    private ValueTask OnPipelineCompleted(OperationsFlowCompletedEventArgs args)
    {
        Task.Delay(args.Flow.Context.KeepDuration)
            .ContinueWith(task =>
            {
                _flows.Remove(args.Flow.Id, out _);
                _flowsTokens.Remove(args.Flow.Id, out _);
            });

        return ValueTask.CompletedTask;
    }
}