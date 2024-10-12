namespace Tridenton.Core.Operations;

/// <summary>
/// Operations execution flow abstraction
/// </summary>
public interface IOperationsFlow : IDurable, IUlidUnique
{
    /// <summary>
    /// Flow start event
    /// </summary>
    event AsyncEventHandler<OperationsFlowStartedEventArgs> OnFlowStarted;

    /// <summary>
    /// Flow completion event
    /// </summary>
    event AsyncEventHandler<OperationsFlowCompletedEventArgs> OnFlowCompleted;

    /// <summary>
    /// Operation start event
    /// </summary>
    event AsyncEventHandler<OperationStatusChangedEventArgs> OnOperationStarted;

    /// <summary>
    /// Operation completion event
    /// </summary>
    event AsyncEventHandler<OperationStatusChangedEventArgs> OnOperationCompleted;

    /// <summary>
    /// Operation failure event
    /// </summary>
    event AsyncEventHandler<OperationStatusChangedEventArgs> OnOperationFailed;

    /// <summary>
    /// Operation cancellation event
    /// </summary>
    event AsyncEventHandler<OperationStatusChangedEventArgs> OnOperationCanceled;

    /// <summary>
    /// Operation rollback start event
    /// </summary>
    event AsyncEventHandler<OperationStatusChangedEventArgs> OnOperationRollbackStarted;

    /// <summary>
    /// Operation rollback completion event
    /// </summary>
    event AsyncEventHandler<OperationStatusChangedEventArgs> OnOperationRollbackCompleted;

    /// <summary>
    /// Operation rollback failure event
    /// </summary>
    event AsyncEventHandler<OperationStatusChangedEventArgs> OnOperationRollbackFailed;

    /// <summary>
    /// Flow context
    /// </summary>
    OperationsFlowContext Context { get; }

    /// <summary>
    /// Operation that is being executed at the current moment of time
    /// </summary>
    Operation? CurrentOperation { get; }

    /// <summary>
    /// Canceled operation
    /// </summary>
    Operation? CanceledOperation { get; }

    /// <summary>
    /// Read-only collection of not started operations
    /// </summary>
    IReadOnlyCollection<Operation> NotStartedOperations { get; }

    /// <summary>
    /// Read-only collection of completed operations
    /// </summary>
    IReadOnlyCollection<Operation> CompletedOperations { get; }

    /// <summary>
    /// Read-only collection of failed operations
    /// </summary>
    IReadOnlyCollection<Operation> FailedOperations { get; }

    /// <summary>
    /// Read-only collection of rolled back operations
    /// </summary>
    IReadOnlyCollection<Operation> RolledBackOperations { get; }

    /// <summary>
    /// Read-only collection of operations, that failed to rollback
    /// </summary>
    IReadOnlyCollection<Operation> FailedToRollbackOperations { get; }
}