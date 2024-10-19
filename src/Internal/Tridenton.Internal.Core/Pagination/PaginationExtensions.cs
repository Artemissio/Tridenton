using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Tridenton.Internal.Core.Extensions;

// ReSharper disable AccessToModifiedClosure

namespace Tridenton.Internal.Core.Pagination;

public static class PaginationExtensions
{
    /// <summary>
    ///     Asynchronously filters, sorts and divides <paramref name="source"/> into paginated response
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source">Input source</param>
    /// <param name="request">Request instance</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the <see cref="PaginatedResponse{TEntity}"/>
    /// </returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    public static async ValueTask<Result<PaginatedResponse<TEntity>>> ToPaginatedResponseAsync<TEntity>(this IQueryable<TEntity> source, PaginatedRequest request, CancellationToken cancellationToken = default) where TEntity : class
    {
        var sourceFilteringResult = source.FilterQuery(request);

        if (sourceFilteringResult.Failed)
        {
            return sourceFilteringResult.Error!;
        }

        source = sourceFilteringResult.Value.OrderQuery(request);

        var totalRecords = await source.TryLongCountAsync(cancellationToken);

        source = await DivideQueryIntoPagesAsync(source, request, cancellationToken);

        var items = await source.TryToArrayAsync(cancellationToken);

        var response = new PaginatedResponse<TEntity>
        {
            Items = items,
            TotalRecordsCount = totalRecords,
            Page = request.Page,
            Size = request.Size,
        };

        return response;
    }

    /// <summary>
    ///     Filters <paramref name="source"/> by specified conditions
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source">Input source</param>
    /// <param name="request">Pagination request</param>
    /// <returns>
    ///     A filtered <see cref="IQueryable{T}"/>
    /// </returns>
    private static Result<IQueryable<TEntity>> FilterQuery<TEntity>(this IQueryable<TEntity> source, PaginatedRequest request) where TEntity : class
    {
        var filteringExpressions = request.Filtering.Where(f => !f.IsEmpty()).ToArray();

        if (filteringExpressions.Length == 0)
        {
            return ResultExtensions.ExplicitSuccess(source);
        }

        var entityType = typeof(TEntity);

        var predicateParts = new List<string>();
        var predicateValues = new List<object>();

        var filtersGroups = filteringExpressions
            .GroupBy(f => f.Property)
            .ToArray();

        var currentFilterIndex = 0;

        foreach (var group in filtersGroups)
        {
            var filters = group.Select(fg => fg).ToArray();

            var property = entityType.GetProperty(group.Key);

            ArgumentNullException.ThrowIfNull(property, $"Property {group.Key} does not exist");

            var filtersCount = filters.Length;

            for (var i = 0; i < filtersCount; i++)
            {
                var predicatePart = "(";

                var filter = filters[i];

                if (property.PropertyType.BaseType == typeof(Enumeration))
                {
                    var config = new ParsingConfig(property.PropertyType, filter, ExpressionOperator.GetEnumOperators());

                    var enumValuesResult = TypeParser.ParseEnum(config);

                    if (enumValuesResult.Failed)
                    {
                        return enumValuesResult.Error!;
                    }

                    predicatePart += string.Join(PaginationConstants.Or, enumValuesResult.Value.Select(_ =>
                    {
                        var predicate = filter.Operator.FormatExpression(filter.Property, currentFilterIndex);
                        currentFilterIndex++;

                        return predicate;
                    }));

                    predicateValues.AddRange(enumValuesResult.Value);
                }
                else
                {
                    if (property.PropertyType == typeof(string))
                    {
                        if (!ExpressionOperator.GetStringOperators().Contains(filter.Operator))
                        {
                            return new InvalidExpressionOperatorError(filter);
                        }

                        if (filter.Operator == ExpressionOperator.Contains)
                        {
                            var valuesResult = filter.GetValues(false);

                            if (valuesResult.Failed)
                            {
                                return valuesResult.Error!;
                            }

                            predicatePart += string.Join(PaginationConstants.Or, valuesResult.Value.Select(_ =>
                            {
                                var predicate = filter.Operator.FormatExpression(filter.Property, currentFilterIndex);
                                currentFilterIndex++;

                                return predicate;
                            }));

                            predicateValues.AddRange(valuesResult.Value);
                        }
                        else
                        {
                            var valuesResult = filter.GetValues();

                            if (valuesResult.Failed)
                            {
                                return valuesResult.Error!;
                            }

                            predicatePart += filter.Operator.FormatExpression(property.Name, Enumerable.Range(currentFilterIndex, valuesResult.Value.Length).ToArray());
                            currentFilterIndex += valuesResult.Value.Length;

                            predicateValues.AddRange(valuesResult.Value);
                        }
                    }
                    else
                    {
                        var typeCode = Type.GetTypeCode(property.PropertyType);

                        if (!TypeParser.TryGetTypeHandler(typeCode, out var handler))
                        {
                            continue;
                        }

                        var valuesResult = handler!.Invoke(property.PropertyType, filter);

                        if (valuesResult.Failed)
                        {
                            return valuesResult.Error!;
                        }

                        if (filter.Operator == ExpressionOperator.Between)
                        {
                            predicatePart += filter.Operator.FormatBetweenExpression(filter.Property,
                                currentFilterIndex, currentFilterIndex + 1);
                            currentFilterIndex += filter.Operator.ParamsCount;
                        }
                        else
                        {
                            predicatePart += string.Join(PaginationConstants.And, valuesResult.Value.Select(_ =>
                            {
                                var predicate =
                                    filter.Operator.FormatExpression(filter.Property, currentFilterIndex);
                                currentFilterIndex++;

                                return predicate;
                            }));
                        }

                        predicateValues.AddRange(valuesResult.Value);
                    }
                }

                predicatePart += ")";

                predicateParts.Add(predicatePart);
            }
        }

        var predicate = string.Join(PaginationConstants.And, predicateParts);

        source = source.Where(predicate, predicateValues.ToArray());

        return ResultExtensions.ExplicitSuccess(source);
    }

    /// <summary>
    ///     Orders <paramref name="source"/> by specified property and direction
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source">Input source</param>
    /// <param name="request">Pagination request</param>
    /// <returns>
    ///     A re-ordered <see cref="IQueryable{T}"/>
    /// </returns>
    private static IQueryable<TEntity> OrderQuery<TEntity>(this IQueryable<TEntity> source, PaginatedRequest request) where TEntity : class
    {
        if (request.Ordering.IsEmpty())
        {
            return source;
        }

        source = source.OrderBy(request.Ordering.OrderBy);

        return request.Ordering.Direction == OrderingDirection.Descending ? source.Reverse() : source;
    }

    /// <summary>
    ///     Asynchronously calculates the amount of pages into which the <paramref name="source"/> will be divided according to <paramref name="request"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source">Input source</param>
    /// <param name="request">Pagination request</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the amount of pages into which the <paramref name="source"/> will be divided according to <paramref name="request"/>
    /// </returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    private static async ValueTask<int> GetQueryPagesAsync<TEntity>(this IQueryable<TEntity> source, PaginatedRequest request, CancellationToken cancellationToken = default) where TEntity : class
    {
        var recordsCount = await source.TryLongCountAsync(cancellationToken);

        var pages = (int)Math.Ceiling(recordsCount / (double)request.Size);

        return pages < 1 ? 1 : pages;
    }

    /// <summary>
    ///     Asynchronously retrieves a specific amount of items at specific page, specified at <paramref name="request"/>, from <paramref name="source"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source">Input source</param>
    /// <param name="request">Pagination request</param>
    /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains an <see cref="IQueryable{T}"/> of specific amount of items at specific page, specified at <paramref name="request"/>
    /// </returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="OperationCanceledException"></exception>
    private static async ValueTask<IQueryable<TEntity>> DivideQueryIntoPagesAsync<TEntity>(IQueryable<TEntity> source, PaginatedRequest request, CancellationToken cancellationToken = default) where TEntity : class
    {
        var pages = await source.GetQueryPagesAsync(request, cancellationToken);

        request.Page = pages < request.Page ? pages : request.Page;

        source = source.Skip((request.Page - 1) * request.Size).Take(request.Size);

        return source;
    }
}