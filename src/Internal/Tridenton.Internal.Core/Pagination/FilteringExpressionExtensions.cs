namespace Tridenton.Internal.Core.Pagination;

public static class FilteringExpressionExtensions
{
    public static bool IsEmpty(this FilteringExpression filteringExpression)
    {
        return string.IsNullOrWhiteSpace(filteringExpression.Property) || 
               string.IsNullOrWhiteSpace(filteringExpression.Value) || 
               filteringExpression.Operator == ExpressionOperator.None;
    }
    
    public static Result<string[]> GetValues(this FilteringExpression filteringExpression, bool verifyParametersCount = true)
    {
        var values = filteringExpression.Value
            .Split(PaginationConstants.FilteringValuesDelimiter);

        if (verifyParametersCount && !values.Length.Equals(filteringExpression.Operator.ParamsCount))
        {
            return new BadRequestError("Pagination.InvalidFilteringExpressionParametersAmount", $"Invalid amount of parameters for {filteringExpression.Property}");
        }

        return values;
    }
    
    public static string FormatExpression(this ExpressionOperator expressionOperator, string property, object value) => string.Format(expressionOperator.Expression, property, value);
}