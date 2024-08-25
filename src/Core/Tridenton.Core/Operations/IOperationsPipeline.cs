namespace Tridenton.Core.Operations;

public interface IOperationsPipeline : IDurable, IUnique
{
    event AsyncEventHandler<Operation> OnOperationStarted;
    event AsyncEventHandler<Operation> OnOperationCompleted;
    event AsyncEventHandler<Operation> OnOperationFailed;
    event AsyncEventHandler<Operation> OnOperationCanceled;
    event AsyncEventHandler<Operation> OnOperationRollbackStarted;
    event AsyncEventHandler<Operation> OnOperationRollbackCompleted;
    event AsyncEventHandler<Operation> OnOperationRollbackFailed;

    Operation? CurrentOperation { get; }
    Operation? CanceledOperation { get; }

    IReadOnlyCollection<Operation> NotStartedOperations { get; }
    IReadOnlyCollection<Operation> CompletedOperations { get; }
    IReadOnlyCollection<Operation> FailedOperations { get; }
    IReadOnlyCollection<Operation> RolledBackOperations { get; }
    IReadOnlyCollection<Operation> FailedToRollbackOperations { get; }
}

public record OperationsPipelineContext(params Operation[] Operations);