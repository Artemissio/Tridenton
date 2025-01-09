using System.Text.Json.Serialization;
using Tridenton.Core.Utilities.Converters;

namespace Tridenton.EventLink.Internal.Application.Core.Models;

[JsonConverter(typeof(EnumerationJsonConverter<DispatcherStatus>))]
public sealed class DispatcherStatus : Enumeration
{
    private DispatcherStatus(int index, string value) : base(index, value) { }
    
    public static readonly DispatcherStatus NotStarted = new(0, "Not Started");
    public static readonly DispatcherStatus Starting = new(1, "Starting");
    public static readonly DispatcherStatus FailedToStart = new(2, "Failed to Start");
    public static readonly DispatcherStatus Started = new(3, "Started");
    public static readonly DispatcherStatus Pausing = new(5, "Pausing");
    public static readonly DispatcherStatus Paused = new(6, "Paused");
    public static readonly DispatcherStatus Stopping = new(7, "Stopping");
    public static readonly DispatcherStatus Stopped = new(8, "Stopped");
}