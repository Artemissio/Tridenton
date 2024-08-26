namespace Tridenton.Core.Operations;

public interface IOperationsFlowsManager
{
    ValueTask<Result<GetFlowResponse>> GetFlowAsync(GetFlowRequest request, CancellationToken cancellationToken = default);

    ValueTask<IOperationsFlow> StartNewFlowAsync(OperationsFlowContext context);

    ValueTask<Result> CancelFlowAsync(CancelFlowRequest request);
}