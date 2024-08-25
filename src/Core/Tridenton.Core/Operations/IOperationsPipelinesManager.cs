namespace Tridenton.Core.Operations;

public interface IOperationsPipelinesManager
{
    ValueTask<Result<GetPipelineResponse>> GetPipelineAsync(GetPipelineRequest request, CancellationToken cancellationToken = default);

    ValueTask<IOperationsPipeline> StartNewPipelineAsync(OperationsPipelineContext context);

    ValueTask<Result> CancelPipelineAsync(CancelPipelineRequest request);
}