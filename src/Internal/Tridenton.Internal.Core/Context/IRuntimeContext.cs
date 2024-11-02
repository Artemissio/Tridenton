using Tridenton.Internal.Core.Models;

namespace Tridenton.Internal.Core.Context;

public interface IRuntimeContext
{
    RequestId RequestId { get; }
    
    Error? InitializationError { get; }
}