using System.Text.Json.Serialization;
using Tridenton.Core.Utilities.Converters;

namespace Tridenton.EventLink.Internal.Application.Core.Models;

[JsonConverter(typeof(EnumerationJsonConverter<StreamStatus>))]
public sealed class StreamStatus : Enumeration
{
    private StreamStatus(int index, string value) : base(index, value) { }

    public static readonly StreamStatus Empty = new(0, "Empty");
    public static readonly StreamStatus Ok = new(1, "OK");
    public static readonly StreamStatus Full = new(2, "Full");
}