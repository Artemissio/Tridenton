namespace Tridenton.Core.Operations;

/// <summary>
/// Operations flow execution context
/// </summary>
public record OperationsFlowContext
{
    /// <summary>
    /// Operations to execute
    /// </summary>
    public Operation[] Operations { get; init; }

    /// <summary>
    /// Identifies whether cancellation of one of the operations should rollback all of the previous operations.
    /// By default - <see langword="true"/>
    /// </summary>
    public bool RollbackPreviousOperationsOnCancel { get; init; }

    /// <summary>
    /// Additional properties
    /// </summary>
    public PropertiesCollection Properties { get; init; }

    /// <summary>
    /// Identifies duration while flow is accessible after completion. By default - 30 minutes
    /// </summary>
    public TimeSpan KeepDuration { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="OperationsFlowContext"/>
    /// </summary>
    public OperationsFlowContext()
    {
        Operations = Array.Empty<Operation>();
        Properties = new();
        RollbackPreviousOperationsOnCancel = true;
        KeepDuration = TimeSpan.FromMinutes(30);
    }
}
