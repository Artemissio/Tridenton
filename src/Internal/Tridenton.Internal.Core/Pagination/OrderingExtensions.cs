using Tridenton.Core;

namespace Tridenton.Internal.Core.Pagination;

public static class OrderingExtensions
{
    internal static bool IsEmpty(this Ordering ordering) => string.IsNullOrEmpty(ordering.OrderBy);
}