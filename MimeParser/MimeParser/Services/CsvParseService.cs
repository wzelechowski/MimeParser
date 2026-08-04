namespace MimeParser.Services;

public class CsvParseService : IParseService
{
    public (int Count, object Data) Parse(string decodedContent)
    {
        var lines = decodedContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length <= 1) return (0, Array.Empty<object>());

        var headers = lines[0].Split(',');
        var rowsData = new List<Dictionary<string, string>>();

        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');
            var rowDict = new Dictionary<string, string>();
            for (int j = 0; j < headers.Length; j++)
            {
                rowDict[headers[j]] = j < values.Length ? values[j] : string.Empty;
            }

            rowsData.Add(rowDict);
        }

        return (rowsData.Count, rowsData);
    }
}