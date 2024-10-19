namespace Tridenton.Core;

public record PaginatedResponse<TEntity>
{
    /// <summary>
    /// Maximum amount of items in current result
    /// </summary>
    public int Size { get; init; }

    /// <summary>
    /// Index of page. By default - 1
    /// </summary>
    public int Page { get; init; }

    /// <summary>
    /// Total amount of elements in the database, which correspond to filtering conditions 
    /// </summary>
    public long TotalRecordsCount { get; init; }

    /// <summary>
    /// The size of <see cref="Items"/> collection
    /// </summary>
    [JsonInclude]
    public int ItemsCount => Items.Length;

    /// <summary>
    /// An amount of pages due to total elements count and page size
    /// </summary>
    [JsonInclude]
    public uint Pages
    {
        get
        {
            if (Size == 0 || TotalRecordsCount == 0)
            {
                return 0;
            }
            
            return (uint)Math.Ceiling(TotalRecordsCount / (double)Size);
        }
    }

    /// <summary>
    /// Index of first item of current page collection within the query. Default value is 1; if collection is empty - 0
    /// </summary>
    [JsonInclude]
    public long StartRowIndex
    {
        get
        {
            var index = (Page - 1) * (long)Size;

            if (Any())
            {
                index++;
            }

            return index;
        }
    }

    /// <summary>
    /// Index of last item of current page`s collection within the query
    /// </summary>
    [JsonInclude]
    public long EndRowIndex => HasNextPage ? Page * Size : TotalRecordsCount;

    /// <summary>
    /// Defines whether current page index is more than 1
    /// </summary>
    [JsonInclude]
    public bool HasPreviousPage => Page > PaginationConstants.DefaultPageIndex;

    /// <summary>
    /// Defines whether current page index is less than total amount of pages
    /// </summary>
    [JsonInclude]
    public bool HasNextPage => Page < Pages;

    /// <summary>
    /// Results collection of current page
    /// </summary>
    public TEntity[] Items { get; init; }

    /// <summary>
    /// Initializes new instance of <see cref="PaginatedResponse{TEntity}"/>
    /// </summary>
    public PaginatedResponse()
    {
        Size = 0;
        Page = 0;
        TotalRecordsCount = 0;

        Items = [];
    }

    /// <summary>
    /// Defines whether <see cref="Items"/> collection is not empty
    /// </summary>
    /// <returns>
    /// <see langword="true" /> if <see cref="Items"/> collection is not empty; otherwise, <see langword="false" />
    /// </returns>
    public bool Any() => ItemsCount != 0;
}