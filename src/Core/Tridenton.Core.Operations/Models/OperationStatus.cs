namespace Tridenton.Core.Operations;

[JsonConverter(typeof(EnumerationJsonConverter<OperationStatus>))]
public sealed class OperationStatus : Enumeration
{
    private OperationStatus(int index, string value) : base(index, value) { }

    public static readonly OperationStatus NotStarted        = new(1, "Not started");
    public static readonly OperationStatus InProgress        = new(2, "In progress");
    public static readonly OperationStatus Completed         = new(3, "Completed");
    public static readonly OperationStatus Failed            = new(4, "Failed");
    public static readonly OperationStatus Canceled          = new(5, "Canceled");
    public static readonly OperationStatus RollbackStarted   = new(6, "Rollback started");
    public static readonly OperationStatus RollbackCompleted = new(7, "Rollback completed");
    public static readonly OperationStatus RollbackFailed    = new(8, "Rollback failed");
}
