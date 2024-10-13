namespace Tridenton.Core.Operations;

/// <summary>
/// 
/// </summary>
public abstract record Operation : Durable, IExecutable<OperationContext>, IUlidUnique
{
    public Ulid Id { get; init; }

    public string Name { get; }

    public DateTime? StatusUpdateUtc { get; private set; }

    private OperationStatus _status;
    public OperationStatus Status
    {
        get => _status;
        private set
        {
            _status = value;
            StatusUpdateUtc = DateTime.UtcNow;
        }
    }

    public Error? Error { get; private set; }

    public Operation(string name = Constants.EmptyString)
    {
        Name = string.IsNullOrWhiteSpace(name)
            ? GetType().Name
            : name;

        Id = Ulid.NewUlid();
        _status = OperationStatus.NotStarted;
    }

    public async ValueTask<Result> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
    {
        Status = OperationStatus.InProgress;
        StartUtc = DateTime.UtcNow;

        Result result;
        try
        {
            result = await ExecuteCoreAsync(context, cancellationToken);

            Status = result.Successful
                ? OperationStatus.Completed
                : OperationStatus.Failed;
        }
        catch (TaskCanceledException)
        {
            Status = OperationStatus.Canceled;
            result = new InternalServerError("Common.TaskCanceled", $"'{Name}' was canceled.");
        }

        FinishUtc = DateTime.UtcNow;
        Error = result.Error;

        return result;
    }

    public async ValueTask<Result> RollbackAsync()
    {
        Status = OperationStatus.RollbackStarted;

        var result = await RollbackCoreAsync();

        Status = result.Successful
            ? OperationStatus.RollbackCompleted
            : OperationStatus.RollbackFailed;

        return result;
    }

    protected abstract ValueTask<Result> ExecuteCoreAsync(OperationContext context, CancellationToken cancellationToken = default);
    protected abstract ValueTask<Result> RollbackCoreAsync();
}