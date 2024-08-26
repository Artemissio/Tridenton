namespace Tridenton.Core.Operations;

public sealed class OperationStatus : Enumeration
{
    private OperationStatus(string value) : base(value) { }

    public static readonly OperationStatus NotStarted        = new("Not started");
    public static readonly OperationStatus InProgress        = new("In progress");
    public static readonly OperationStatus Completed         = new("Completed");
    public static readonly OperationStatus Failed            = new("Failed");
    public static readonly OperationStatus Canceled          = new("Canceled");
    public static readonly OperationStatus RollbackStarted   = new("Rollback started");
    public static readonly OperationStatus RollbackCompleted = new("Rollback completed");
    public static readonly OperationStatus RollbackFailed    = new("Rollback failed");
}
