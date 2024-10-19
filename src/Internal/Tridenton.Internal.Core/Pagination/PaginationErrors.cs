namespace Tridenton.Internal.Core.Pagination;

public sealed record InvalidExpressionOperatorError : BadRequestError
{
    public InvalidExpressionOperatorError(FilteringExpression expression)
        : base("Pagination.InvalidExpressionOperator", $"Operator '{expression.Operator}' is not valid for property {expression.Property}") { }
}

public sealed record InvalidFilterArgumentError : BadRequestError
{
    public InvalidFilterArgumentError(string value, FilteringExpression expression)
        : base("Pagination.InvalidFilterArgument",$"Value '{value}' is not valid for {expression.Property}")
    {
        
    }
}