using System.Text.Json;

namespace MimeParser.Services;

public class InternalJsonParseService : IParseService
{
    public (int Count, object Data) Parse(string decodedContent)
    {
        using var jsonDoc = JsonDocument.Parse(decodedContent);
        var root = jsonDoc.RootElement;

        if (root.ValueKind == JsonValueKind.Array)
        {
            var parsedData = JsonSerializer.Deserialize<object>(decodedContent) ?? new object();
            return (root.GetArrayLength(), parsedData);
        }

        var singleObject = JsonSerializer.Deserialize<object>(decodedContent) ?? new object();
        return (1, singleObject);
    }
}