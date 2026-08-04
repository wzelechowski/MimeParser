using System.Text.Json.Serialization;

namespace MimeParser.Payload;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ParseType
{
    CSV, INTERNAL_JSON
}