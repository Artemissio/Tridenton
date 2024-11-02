using Microsoft.EntityFrameworkCore;

namespace Tridenton.Internal.Core.Extensions;

public static class QueryableExtensions
{
    public static async ValueTask<long> TryLongCountAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default)
    {
        try
        {
            return await source.LongCountAsync(cancellationToken);
        }
        catch (NotImplementedException)
        {
            return source.LongCount();
        }
    }
    
    public static async ValueTask<TSource[]> TryToArrayAsync<TSource>(this IQueryable<TSource> source, CancellationToken cancellationToken = default)
    {
        try
        {
            return await source.ToArrayAsync(cancellationToken);
        }
        catch (NotImplementedException)
        {
            return source.ToArray();
        }
    }
}