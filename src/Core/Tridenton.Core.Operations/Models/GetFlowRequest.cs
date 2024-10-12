namespace Tridenton.Core.Operations;

public record GetFlowRequest(Ulid FlowId);

public record GetFlowResponse(IOperationsFlow Flow);

public record FlowNotFoundError(Ulid FlowId) : NotFoundError("NotFound.Flow", $"Flow with Id: {FlowId} was not found.");