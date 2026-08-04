namespace MimeParser.Services;

public interface IParseService
{
    (int Count, object Data) Parse(string decodedContent);
}