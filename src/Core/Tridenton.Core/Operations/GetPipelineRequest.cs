namespace Tridenton.Core.Operations;

public record GetPipelineRequest(Ulid PipelineId);

public record GetPipelineResponse(IOperationsPipeline Pipeline);

public record PipelineNotFoundError(Ulid PipelineId) : NotFoundError("NotFound.Pipeline", $"Pipeline with Id: {PipelineId} was not found.");