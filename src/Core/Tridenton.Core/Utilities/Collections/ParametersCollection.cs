using System.Globalization;

namespace Tridenton.Core.Utilities.Collections;

public sealed class ParametersCollection : SortedDictionary<string, ParameterValue>
{
    /// <summary>
    /// Constructs empty ParameterCollection.
    /// </summary>
    public ParametersCollection() : base(comparer: StringComparer.Ordinal) { }

    /// <summary>
    /// Adds a parameter with a string value.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Add(string key, string value)
    {
        Add(key, new StringParameterValue(value));
    }

    /// <summary>
    /// Adds a parameter with a list-of-strings value.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public void Add(string key, List<string> values)
    {
        switch (values.Count)
        {
            case 0:
                return;
            case 1:
                Add(key, values[0]);
                return;
            default:
                Add(key, new StringListParameterValue(values));
                break;
        }
    }

    /// <summary>
    /// Adds a parameter with a list-of-doubles value.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public void Add(string key, List<double> values)
    {
        Add(key, new DoubleListParameterValue(values));
    }

    /// <summary>
    /// Converts the current parameters into a list of key-value pairs.
    /// </summary>
    /// <returns></returns>
    public KeyValuePair<string, string>[] GetSortedParametersList()
    {
        return GetParametersEnumerable().ToArray();
    }

    private IEnumerable<KeyValuePair<string, string>> GetParametersEnumerable()
    {
        foreach (var kvp in this)
        {
            var name = kvp.Key;
            var value = kvp.Value;

            switch (value)
            {
                case StringParameterValue stringParameterValue:
                    yield return new KeyValuePair<string, string>(name, stringParameterValue.Value);
                    break;

                case StringListParameterValue stringListParameterValue:
                    var sortedStringListParameterValue = stringListParameterValue.Values;
                    sortedStringListParameterValue.Sort(StringComparer.Ordinal);
                    foreach (var listValue in sortedStringListParameterValue)
                    {
                        yield return new KeyValuePair<string, string>(name, listValue);
                    }
                    break;

                case DoubleListParameterValue doubleListParameterValue:
                    var sortedDoubleListParameterValue = doubleListParameterValue.Values;
                    sortedDoubleListParameterValue.Sort();
                    foreach (var listValue in sortedDoubleListParameterValue)
                    {
                        yield return new KeyValuePair<string, string>(name, listValue.ToString(CultureInfo.InvariantCulture));
                    }
                    break;
                default:
                    continue;
            }
        }
    }
}