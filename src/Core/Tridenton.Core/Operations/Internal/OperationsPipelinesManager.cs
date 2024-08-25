namespace Tridenton.Core.Operations.Internal;

internal sealed class OperationsPipelinesManager : IOperationsPipelinesManager
{
    private readonly Dictionary<Ulid, IOperationsPipeline> _pipelines;
    private readonly Dictionary<Ulid, CancellationTokenSource> _pipelinesTokens;

    public OperationsPipelinesManager()
	{
        _pipelines = new();
        _pipelinesTokens = new();
	}

    public ValueTask<Result<GetPipelineResponse>> GetPipelineAsync(GetPipelineRequest request, CancellationToken cancellationToken = default)
    {
        Result<GetPipelineResponse> result = _pipelines.TryGetValue(request.PipelineId, out var pipeline)
            ? new GetPipelineResponse(pipeline)
            : new PipelineNotFoundError(request.PipelineId);

        return ValueTask.FromResult(result);
    }

    public async ValueTask<IOperationsPipeline> StartNewPipelineAsync(OperationsPipelineContext context)
    {
        var pipeline = new OperationsPipeline(context);

        var cts = new CancellationTokenSource();

        _pipelines[pipeline.Id] = pipeline;
        _pipelinesTokens[pipeline.Id] = cts;

        await pipeline.ExecuteAsync(cts.Token);

        return pipeline;
    }

    public async ValueTask<Result> CancelPipelineAsync(CancelPipelineRequest request)
    {
        var pipelineResult = await GetPipelineAsync(new GetPipelineRequest(request.PipelineId));

        if (pipelineResult.Failed)
        {
            return pipelineResult.Error!;
        }

        _pipelinesTokens[request.PipelineId].Cancel();

        return Result.Success;
    }
}