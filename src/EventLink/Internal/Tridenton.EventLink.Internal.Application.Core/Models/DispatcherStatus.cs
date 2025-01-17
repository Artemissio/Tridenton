using System.Text.Json.Serialization;
using Tridenton.Core.Utilities.Converters;

namespace Tridenton.EventLink.Internal.Application.Core.Models;

[JsonConverter(typeof(EnumerationJsonConverter<DispatcherStatus>))]
public sealed class DispatcherStatus : Enumeration
{
    private DispatcherStatus(int index, string value) : base(index, value) { }
    
    public static readonly DispatcherStatus NotStarted = new(0, "Not Started");
    public static readonly DispatcherStatus Starting = new(1, "Starting");
    public static readonly DispatcherStatus Started = new(2, "Started");
    public static readonly DispatcherStatus Pausing = new(3, "Pausing");
    public static readonly DispatcherStatus Paused = new(4, "Paused");
    public static readonly DispatcherStatus Stopping = new(5, "Stopping");
    public static readonly DispatcherStatus Stopped = new(6, "Stopped");
    public static readonly DispatcherStatus EmptyEventsStream = new(7, "Empty Events Stream");
}