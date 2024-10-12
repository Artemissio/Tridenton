namespace Tridenton.Core.Operations;

public abstract record OperationsFlowEventArgs;

public record OperationStatusChangedEventArgs(Operation Operation)   : OperationsFlowEventArgs;
public record OperationsFlowStartedEventArgs(IOperationsFlow Flow)   : OperationsFlowEventArgs;
public record OperationsFlowCompletedEventArgs(IOperationsFlow Flow) : OperationsFlowEventArgs;