namespace Tridenton.Core.Models;

/// <summary>
/// 
/// </summary>
public readonly struct Result
{
    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public static readonly Result Success = new();

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public bool Successful { get; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public bool Failed { get; }

    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Error? Error { get; }

    [JsonConstructor]
    private Result(Error? error = null)
    {
        Error = error;
        Successful = Error is null;
        Failed = !Successful;
    }

    public static implicit operator Result(Error error) => new(error);
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly struct Result<T>
{
    [JsonIgnore]
    public bool Successful { get; }

    [JsonIgnore]
    public bool Failed { get; }

    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Error? Error { get; }

    [JsonInclude]
    public T Value { get; }

    [JsonConstructor]
    private Result(T value, Error? error = null)
    {
        Value = value;
        Error = error;
        Successful = Error is null;
        Failed = !Successful;
    }

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Error error) => new(default!, error);
}