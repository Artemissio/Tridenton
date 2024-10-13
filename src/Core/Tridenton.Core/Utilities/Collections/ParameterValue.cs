namespace Tridenton.Core.Utilities.Collections;

/// <summary>
/// Abstract parameter value.
/// </summary>
public abstract record ParameterValue
{
}

/// <summary>
/// String parameter value.
/// </summary>
public sealed record StringParameterValue : ParameterValue
{
    /// <summary>
    /// String value of the parameter.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Constructs ParameterValue for a single string.
    /// </summary>
    /// <param name="value"></param>
    public StringParameterValue(string value)
    {
        Value = value;
    }
}

/// <summary>
/// String list parameter value.
/// </summary>
public sealed record StringListParameterValue : ParameterValue
{
    /// <summary>
    /// List of strings value of the parameter.
    /// </summary>
    public List<string> Values { get; }

    /// <summary>
    /// Constructs ParameterValue for a list of strings.
    /// </summary>
    /// <param name="values"></param>
    public StringListParameterValue(List<string> values)
    {
        Values = values;
    }
}

/// <summary>
/// Double list parameter value.
/// </summary>
public sealed record DoubleListParameterValue : ParameterValue
{
    /// <summary>
    /// List of doubles value of the parameter.
    /// </summary>
    public List<double> Values { get; }

    /// <summary>
    /// Constructs ParameterValue for a list of doubles.
    /// </summary>
    /// <param name="values"></param>
    public DoubleListParameterValue(List<double> values)
    {
        Values = values;
    }
}