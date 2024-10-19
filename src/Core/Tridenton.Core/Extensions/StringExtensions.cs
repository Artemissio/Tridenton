using System.Text.RegularExpressions;

namespace Tridenton.Core.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Defines whether <paramref name="value"/> to lowercase contains <paramref name="text"/> to lowercase
    /// </summary>
    /// <param name="value">String value which may contain <paramref name="text"/></param>
    /// <param name="text">String value to find</param>
    /// <returns><see langword="true"/> if <paramref name="value"/> to lowercase contains <paramref name="text"/> to lowercase; otherwise - <see langword="false"/></returns>
    public static bool ContainsIgnoreCase(this string value, string text) => value.Contains(text, StringComparison.CurrentCultureIgnoreCase);
    
    /// <summary>
    /// Defines whether <paramref name="value"/> matches to regular expression, defined by <paramref name="pattern"/>
    /// </summary>
    /// <param name="value">String value</param>
    /// <param name="pattern">Regular expression pattern</param>
    /// <returns><see langword="true"/> if <paramref name="value"/> matches to regular expression, defined by <paramref name="pattern"/>; otherwise - <see langword="false"/></returns>
    public static bool MatchesRegex(this string value, string pattern) => Regex.IsMatch(value, pattern);
}