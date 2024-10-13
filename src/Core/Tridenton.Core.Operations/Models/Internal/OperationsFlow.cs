namespace Tridenton.Core.Operations.Internal;

internal sealed record OperationsFlow : Durable, IOperationsFlow
{
    private readonly List<Operation> _notStartedOperations;
    private readonly List<Operation> _completedOperations;
    private readonly List<Operation> _failedOperations;
    private readonly List<Operation> _rolledBackOperations;
    private readonly List<Operation> _failedToRollbackOperations;

    public event AsyncEventHandler<OperationsFlowStartedEventArgs>? OnFlowStarted;
    public event AsyncEventHandler<OperationsFlowCompletedEventArgs>? OnFlowCompleted;

    public event AsyncEventHandler<OperationStatusChangedEventArgs>? OnOperationStarted;
    public event AsyncEventHandler<OperationStatusChangedEventArgs>? OnOperationCompleted;
    public event AsyncEventHandler<OperationStatusChangedEventArgs>? OnOperationFailed;
    public event AsyncEventHandler<OperationStatusChangedEventArgs>? OnOperationCanceled;
    public event AsyncEventHandler<OperationStatusChangedEventArgs>? OnOperationRollbackStarted;
    public event AsyncEventHandler<OperationStatusChangedEventArgs>? OnOperationRollbackCompleted;
    public event AsyncEventHandler<OperationStatusChangedEventArgs>? OnOperationRollbackFailed;

    public Ulid Id { get; init; }

    public Operation? CurrentOperation { get; private set; }

    public Operation? CanceledOperation { get; private set; }

    public IReadOnlyCollection<Operation> NotStartedOperations       => _notStartedOperations.AsReadOnly();
    public IReadOnlyCollection<Operation> CompletedOperations        => _completedOperations.AsReadOnly();
    public IReadOnlyCollection<Operation> FailedOperations           => _failedOperations.AsReadOnly();
    public IReadOnlyCollection<Operation> RolledBackOperations       => _rolledBackOperations.AsReadOnly();
    public IReadOnlyCollection<Operation> FailedToRollbackOperations => _failedToRollbackOperations.AsReadOnly();

    public OperationsFlowContext Context { get; }

    public OperationsFlow(OperationsFlowContext context)
    {
        Id = Ulid.NewUlid();

        Context = context;

        _notStartedOperations       = [..Context.Operations];
        _completedOperations        = new List<Operation>(Context.Operations.Length);
        _failedOperations           = new List<Operation>(Context.Operations.Length);
        _rolledBackOperations       = new List<Operation>(Context.Operations.Length);
        _failedToRollbackOperations = new List<Operation>(Context.Operations.Length);
    }

    internal async ValueTask<Result> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (OnFlowStarted is not null)
        {
            await OnFlowStarted.Invoke(new OperationsFlowStartedEventArgs(this));
        }

        foreach (var operation in Context.Operations)
        {
            CurrentOperation = operation;

            await InvokeOperationStatusChangedEventAsync(OnOperationStarted);

            var result = await CurrentOperation.ExecuteAsync(new OperationContext(Context.Parameters), cancellationToken);

            if (result.Successful)
            {
                _completedOperations.Add(CurrentOperation);
                _notStartedOperations.Remove(CurrentOperation);
                await InvokeOperationStatusChangedEventAsync(OnOperationCompleted);
            }
            else
            {
                if (CurrentOperation.Status == OperationStatus.Canceled)
                {
                    CanceledOperation = CurrentOperation;
                    await InvokeOperationStatusChangedEventAsync(OnOperationCanceled);

                    if (Context.RollbackPreviousOperationsOnCancel)
                    {
                        await RollbackAsync();
                    }
                }
                else
                {
                    _failedOperations.Add(CurrentOperation);
                    await InvokeOperationStatusChangedEventAsync(OnOperationFailed);

                    await RollbackAsync();
                }

                CurrentOperation = null;

                return result.Error!;
            }
        }

        if (OnFlowCompleted is not null)
        {
            await OnFlowCompleted.Invoke(new OperationsFlowCompletedEventArgs(this));
        }

        return Result.Success;
    }

    private async ValueTask RollbackAsync()
    {
        foreach (var operation in CompletedOperations.Reverse())
        {
            CurrentOperation = operation;
            await InvokeOperationStatusChangedEventAsync(OnOperationRollbackStarted);

            var result = await CurrentOperation.RollbackAsync();

            if (result.Successful)
            {
                _rolledBackOperations.Add(CurrentOperation);
                await InvokeOperationStatusChangedEventAsync(OnOperationRollbackCompleted);
            }
            else
            {
                _failedToRollbackOperations.Add(CurrentOperation);
                await InvokeOperationStatusChangedEventAsync(OnOperationRollbackFailed);
            }
        }
    }

    private ValueTask InvokeOperationStatusChangedEventAsync(AsyncEventHandler<OperationStatusChangedEventArgs>? @event)
    {
        if (@event is null || CurrentOperation is null)
        {
            return ValueTask.CompletedTask;
        }

        return @event.Invoke(new OperationStatusChangedEventArgs(CurrentOperation));
    }
}