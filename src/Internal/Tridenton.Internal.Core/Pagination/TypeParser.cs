using System.Globalization;

namespace Tridenton.Internal.Core.Pagination;

internal readonly struct TypeParser
{
    internal static bool TryGetTypeHandler(TypeCode typeCode,
        out Func<Type, FilteringExpression, Result<List<object>>>? handler)
    {
        return TypeHandlers.TryGetValue(typeCode, out handler);
    }
    
    private static readonly Dictionary<TypeCode, Func<Type, FilteringExpression, Result<List<object>>>> TypeHandlers = new()
    {
        { TypeCode.Boolean,  (property, expression) => ParseBool(new (property, expression, ExpressionOperator.GetBooleanOperators())) },
        { TypeCode.Byte,     (property, expression) => Parse<byte>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
        { TypeCode.Char,     (property, expression) => Parse<char>(new (property, expression, ExpressionOperator.GetEnumOperators())) },
        { TypeCode.DateTime, (property, expression) => Parse<DateTime>(new (property, expression, ExpressionOperator.GetDateTimeOperators())) },
        { TypeCode.Decimal,  (property, expression) => Parse<decimal>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
        { TypeCode.Double,   (property, expression) => Parse<double>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
        { TypeCode.Int16,    (property, expression) => Parse<short>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
        { TypeCode.Int32,    (property, expression) => Parse<int>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
        { TypeCode.Int64,    (property, expression) => Parse<long>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
        { TypeCode.SByte,    (property, expression) => Parse<sbyte>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
        { TypeCode.Single,   (property, expression) => Parse<float>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
        { TypeCode.UInt16,   (property, expression) => Parse<ushort>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
        { TypeCode.UInt32,   (property, expression) => Parse<uint>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
        { TypeCode.UInt64,   (property, expression) => Parse<ulong>(new (property, expression, ExpressionOperator.GetNumericOperators())) },
    };

    internal static Result<List<object>> Parse<TValue>(ParsingConfig config) where TValue : IParsable<TValue>
    {
        var valuesResult = config.GetValues();

        if (valuesResult.Failed)
        {
            return valuesResult.Error!;
        }

        var result = new List<object>();

        foreach (var value in valuesResult.Value)
        {
            if (!TValue.TryParse(value, CultureInfo.InvariantCulture, out var parsed))
            {
                return new InvalidFilterArgumentError(value, config.Expression);
            }

            result.Add(parsed);
        }

        return result;
    }

    internal static Result<List<object>> ParseBool(ParsingConfig config)
    {
        var valuesResult = config.GetValues();

        if (valuesResult.Failed)
        {
            return valuesResult.Error!;
        }

        var result = new List<object>();

        foreach (var value in valuesResult.Value)
        {
            if (!bool.TryParse(value, out var parsed))
            {
                return new InvalidFilterArgumentError(value, config.Expression);
            }

            result.Add(parsed);
        }

        return result;
    }

    internal static Result<List<object>> ParseEnum(ParsingConfig config)
    {
        var valuesResult = config.GetValues(false);

        if (valuesResult.Failed)
        {
            return valuesResult.Error!;
        }

        var result = new List<object>();

        foreach (var value in valuesResult.Value)
        {
            var enumeration = Enumeration.GetValue(config.Property, value);

            if (enumeration is null)
            {
                return new InvalidFilterArgumentError(value, config.Expression);
            }

            result.Add(enumeration);
        }

        return result;
    }
}

internal record ParsingConfig(Type Property, FilteringExpression Expression, ExpressionOperator[] ValidOperations)
{
    internal Result<string[]> GetValues(bool verifyParametersCount = true)
    {
        if (!ValidOperations.Contains(Expression.Operator))
        {
            return new InvalidExpressionOperatorError(Expression);
        }

        return Expression.GetValues(verifyParametersCount);
    }
}