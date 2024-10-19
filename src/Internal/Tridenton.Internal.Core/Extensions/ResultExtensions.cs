namespace Tridenton.Internal.Core.Extensions;

public static class ResultExtensions
{
    public static Result<T> ExplicitSuccess<T>(T value)
    {
        return value;
    }
}