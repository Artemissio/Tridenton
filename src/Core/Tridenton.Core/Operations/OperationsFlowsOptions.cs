namespace Tridenton.Core.Operations;

public record OperationsFlowsOptionsBuilder
{
    /// <summary>
    /// Identifies whether cancellation of one of the operations should rollback all of the previous operations.
    /// By default - <see langword="true"/>
    /// </summary>
    public bool RollbackPreviousOperationsOnCancel { get; init; }

    /// <summary>
    /// Additional properties
    /// </summary>
    public ParametersCollection Parameters { get; init; }

    /// <summary>
    /// Identifies duration while flow is accessible after completion. By default - 30 minutes
    /// </summary>
    public TimeSpan KeepDuration { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="OperationsFlowsOptionsBuilder"/>
    /// </summary>
    public OperationsFlowsOptionsBuilder()
    {
        Parameters = [];
        RollbackPreviousOperationsOnCancel = true;
        KeepDuration = TimeSpan.FromMinutes(30);
    }
}