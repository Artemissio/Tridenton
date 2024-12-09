using Tridenton.Core;
using Tridenton.Core.Utilities;
using Tridenton.Internal.Core.Utilities;

namespace Tridenton.Internal.EventLink.Core.Utilities;

internal readonly struct EventLinkTreidGenerator
{
    public static Treid Generate(string account, string resourceType, string resourceId = Constants.Wildcard)
    {
        return new Treid(
            partition: InternalConstants.TridentonWebServicesPartition,
            account: account,
            servicesGroup: string.Empty,
            service: "event-link",
            resourceType: resourceType,
            resourceId: resourceId);
    }
}