namespace Tridenton.Internal.Core.CQRS.Internal;

internal sealed record OrchestratorSetup(Dictionary<Type, Type> Handlers);