namespace Tridenton.Core.Metadata.Tracing;

/// <summary>
/// 
/// </summary>
public sealed record TraceSegmentIssue : RecordMarker, IIndexUnique
{
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public int Id { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    public Error Error { get; }
    
    /// <summary>
    /// Initializes a new instance of <see cref="TraceSegmentIssue"/>
    /// </summary>
    /// <param name="error">Error</param>
    [JsonConstructor]
    public TraceSegmentIssue(Error error)
    {
        Error = error;
    }
}