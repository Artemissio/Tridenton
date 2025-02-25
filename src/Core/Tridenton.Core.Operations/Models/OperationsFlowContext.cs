namespace Tridenton.Core.Operations;

/// <summary>
/// Operations flow execution context
/// </summary>
public record OperationsFlowContext : OperationsFlowsOptionsBuilder
{
    /// <summary>
    /// Operations to execute
    /// </summary>
    public Operation[] Operations { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="OperationsFlowContext"/>
    /// </summary>
    public OperationsFlowContext() : base()
    {
        Operations = [];
    }
}
