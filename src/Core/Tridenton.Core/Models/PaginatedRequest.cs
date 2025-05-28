using System.ComponentModel;

namespace Tridenton.Core;

public readonly struct PaginationConstants
{
    public const int DefaultPageIndex = 1;
    public const int DefaultPageSize = 100;
    public const char FilteringValuesDelimiter = ',';
    public const string Or = " OR ";
    public const string And = " AND ";
    
    public static readonly int[] PageSizeOptions = [25, 50, 100];
}

public record PaginatedRequest
{
    /// <summary>
    /// Index of page to display
    /// </summary>
    [ValidateProperty(Required = true, Min = PaginationConstants.DefaultPageIndex, Max = int.MaxValue, ErrorMessage = $"Page is required and should be between 1 and 2147483647")]
    public int Page { get; set; }

    /// <summary>
    /// Amount of elements to display per one page
    /// </summary>
    [ValidateProperty(Required = true, Min = 1, Max = int.MaxValue, ErrorMessage = "Size is required and should be between 1 and 2147483647")]
    public int Size { get; set; }

    /// <summary>
    /// Ordering specification
    /// </summary>
    public Ordering Ordering { get; init; }

    /// <summary>
    /// Filtering expressions
    /// </summary>
    public FilteringExpression[] Filtering { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="PaginatedRequest"/>
    /// </summary>
    public PaginatedRequest()
    {
        Page = PaginationConstants.DefaultPageIndex;
        Size = PaginationConstants.DefaultPageSize;

        Ordering = new();
        Filtering = [];
    }
}

/// <summary>
/// Ordering specification
/// </summary>
public sealed record Ordering
{
    /// <summary>
    /// Name of property to order by
    /// </summary>
    public string OrderBy { get; init; }

    /// <summary>
    /// Ordering direction. Default value - <see cref="OrderingDirection.Ascending"/>
    /// </summary>
    public OrderingDirection Direction { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="Ordering"/>
    /// </summary>
    public Ordering()
    {
        OrderBy = string.Empty;
        Direction = OrderingDirection.Ascending;
    }

    public override string ToString()
    {
        return string.IsNullOrWhiteSpace(OrderBy)
            ? string.Empty
            : $"{OrderBy} {Direction}";
    }
}

/// <summary>
/// Filtering expression
/// </summary>
public sealed record FilteringExpression
{
    /// <summary>
    /// Name of property
    /// </summary>
    public string Property { get; init; }

    /// <summary>
    /// Filtering value
    /// </summary>
    public string Value { get; init; }

    /// <summary>
    /// Expression operator
    /// </summary>
    public ExpressionOperator Operator { get; init; }

    /// <summary>
    /// Initializes a new instance of <see cref="FilteringExpression"/>
    /// </summary>
    public FilteringExpression()
    {
        Property = Value = string.Empty;
        Operator = ExpressionOperator.None;
    }

    public override string ToString()
    {
        if (string.IsNullOrWhiteSpace(Property) || Operator == ExpressionOperator.None)
        {
            return string.Empty;
        }

        return $"{Property} {Operator} '{Value}'";
    }
}

[TypeConverter(typeof(EnumerationTypeConverter<OrderingDirection>))]
[JsonConverter(typeof(EnumerationJsonConverter<OrderingDirection>))]
public sealed class OrderingDirection : Enumeration
{
    private OrderingDirection(int index, string value) : base(index, value) { }
    
    public static readonly OrderingDirection Ascending = new(1, "Ascending");
    public static readonly OrderingDirection Descending = new(2, "Descending");
}

[TypeConverter(typeof(EnumerationTypeConverter<ExpressionOperator>))]
[JsonConverter(typeof(EnumerationJsonConverter<ExpressionOperator>))]
public sealed class ExpressionOperator : Enumeration
{
    [JsonIgnore]
    public readonly string Expression;
    
    [JsonIgnore]
    public readonly int ParamsCount;

    private ExpressionOperator(int index, string value, string expression, int paramsCount)
        : base(index, value)
    {
        Expression = expression;
        ParamsCount = paramsCount;
    }

    public static readonly ExpressionOperator None = new(1, nameof(None), string.Empty, 0);
    public static readonly ExpressionOperator Is = new(2, nameof(Is), "{0} = @{1}", 1);
    public static readonly ExpressionOperator Not = new(3, nameof(Not), "{0} != @{1}", 1);
    public static readonly ExpressionOperator Equal = new(4, nameof(Equal), "{0} = @{1}", 1);
    public static readonly ExpressionOperator NotEqual = new(5, nameof(NotEqual), "{0} != @{1}", 1);
    public static readonly ExpressionOperator Greater = new(6, nameof(Greater), "{0} > @{1}", 1);
    public static readonly ExpressionOperator GreaterOrEqual = new(7, nameof(GreaterOrEqual), "{0} >= @{1}", 1);
    public static readonly ExpressionOperator Less = new(8, nameof(Less), "{0} < @{1}", 1);
    public static readonly ExpressionOperator LessOrEqual = new(9, nameof(LessOrEqual), "{0} <= @{1}", 1);
    public static readonly ExpressionOperator Contains = new(10, nameof(Contains), "{0}.ToLower().Contains(@{1}.ToLower())", 1);
    public static readonly ExpressionOperator Between = new(11, nameof(Between), "{0} >= @{1} AND {0} <= @{2}", 2);
    
    public static ExpressionOperator[] GetBooleanOperators()
    {
        return
        [
            Is,
            Not
        ];
    }

    public static ExpressionOperator[] GetStringOperators()
    {
        return
        [
            Equal,
            NotEqual,
            Contains
        ];
    }

    public static ExpressionOperator[] GetEnumOperators()
    {
        return
        [
            Equal,
            NotEqual
            //Contains,
        ];
    }

    public static ExpressionOperator[] GetNumericOperators()
    {
        return
        [
            Equal,
            NotEqual,
            Greater,
            GreaterOrEqual,
            Less,
            LessOrEqual,
            Between
        ];
    }

    public static ExpressionOperator[] GetDateTimeOperators()
    {
        return
        [
            Equal,
            NotEqual,
            Greater,
            GreaterOrEqual,
            Less,
            LessOrEqual,
            Between
        ];
    }
}