namespace Tridenton.Core.Operations.Internal;

internal class OperationsPipeline : Durable, IOperationsPipeline
{
    private readonly OperationsPipelineContext _context;
    private readonly List<Operation> _notStartedOperations;
    private readonly List<Operation> _completedOperations;
    private readonly List<Operation> _failedOperations;
    private readonly List<Operation> _rolledBackOperations;
    private readonly List<Operation> _failedToRollbackOperations;

    public event AsyncEventHandler<Operation>? OnOperationStarted;
    public event AsyncEventHandler<Operation>? OnOperationCompleted;
    public event AsyncEventHandler<Operation>? OnOperationFailed;
    public event AsyncEventHandler<Operation>? OnOperationCanceled;
    public event AsyncEventHandler<Operation>? OnOperationRollbackStarted;
    public event AsyncEventHandler<Operation>? OnOperationRollbackCompleted;
    public event AsyncEventHandler<Operation>? OnOperationRollbackFailed;

    public Ulid Id { get; }

    public Operation? CurrentOperation { get; private set; }

    public Operation? CanceledOperation { get; private set; }

    public IReadOnlyCollection<Operation> NotStartedOperations => _notStartedOperations.AsReadOnly();
    public IReadOnlyCollection<Operation> CompletedOperations => _completedOperations.AsReadOnly();
    public IReadOnlyCollection<Operation> FailedOperations => _failedOperations.AsReadOnly();
    public IReadOnlyCollection<Operation> RolledBackOperations => _rolledBackOperations.AsReadOnly();
    public IReadOnlyCollection<Operation> FailedToRollbackOperations => _failedToRollbackOperations.AsReadOnly();

    public OperationsPipeline(OperationsPipelineContext context)
    {
        Id = Ulid.NewUlid();

        _context = context;

        _notStartedOperations = new(context.Operations);
        _completedOperations = new(context.Operations.Length);
        _failedOperations = new(context.Operations.Length);
        _rolledBackOperations = new(context.Operations.Length);
        _failedToRollbackOperations = new(context.Operations.Length);
    }

    internal async ValueTask<Result> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        foreach (var operation in _context.Operations)
        {
            CurrentOperation = operation;

            await InvokeEventAsync(OnOperationStarted);

            var result = await CurrentOperation.ExecuteAsync(cancellationToken);

            if (result.Successful)
            {
                _completedOperations.Add(CurrentOperation);
                await InvokeEventAsync(OnOperationCompleted);
            }
            else
            {
                if (CurrentOperation.Status == OperationStatus.Canceled)
                {
                    CanceledOperation = CurrentOperation;
                    await InvokeEventAsync(OnOperationCanceled);
                }
                else
                {
                    _failedOperations.Add(CurrentOperation);
                    await InvokeEventAsync(OnOperationFailed);

                    await RollbackAsync();
                }

                CurrentOperation = null;

                return result.Error!;
            }
        }

        return Result.Success;
    }

    private async ValueTask RollbackAsync()
    {
        foreach (var operation in CompletedOperations.Reverse())
        {
            CurrentOperation = operation;
            await InvokeEventAsync(OnOperationRollbackStarted);

            var result = await CurrentOperation.RollbackAsync();

            if (result.Successful)
            {
                _rolledBackOperations.Add(CurrentOperation);
                await InvokeEventAsync(OnOperationRollbackCompleted);
            }
            else
            {
                _failedToRollbackOperations.Add(CurrentOperation);
                await InvokeEventAsync(OnOperationRollbackFailed);
            }
        }
    }

    private ValueTask InvokeEventAsync(AsyncEventHandler<Operation>? @event)
    {
        if (@event is null || CurrentOperation is null)
        {
            return ValueTask.CompletedTask;
        }

        return @event.Invoke(CurrentOperation);
    }
}