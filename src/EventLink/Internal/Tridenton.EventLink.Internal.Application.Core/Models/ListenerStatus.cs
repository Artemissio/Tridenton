using System.Text.Json.Serialization;
using Tridenton.Core.Utilities.Converters;

namespace Tridenton.EventLink.Internal.Application.Core.Models;

[JsonConverter(typeof(EnumerationJsonConverter<ListenerStatus>))]
public sealed class ListenerStatus : Enumeration
{
    private ListenerStatus(int index, string value) : base(index, value) { }
    
    public static readonly ListenerStatus NotStarted = new(0, "Not Started");
    public static readonly ListenerStatus Starting = new(1, "Starting");
    public static readonly ListenerStatus FailedToStart = new(2, "Failed to Start");
    public static readonly ListenerStatus Started = new(3, "Started");
    public static readonly ListenerStatus LimitExceeded = new(4, "Limit Exceeded");
    public static readonly ListenerStatus Pausing = new(5, "Pausing");
    public static readonly ListenerStatus Paused = new(6, "Paused");
    public static readonly ListenerStatus Stopping = new(7, "Stopping");
    public static readonly ListenerStatus Stopped = new(8, "Stopped");
}