using System.Text;
using MimeParser.Common;
using MimeParser.Payload;

namespace MimeParser.Services;

public class ProcessingService(IServiceProvider serviceProvider) : IProcessingService
{
    public Result<ParseResponse> ParseAndProcess(ParseRequest request)
    {
        byte[] decodedBytes = Convert.FromBase64String(request.Content);
        string decodedString = Encoding.UTF8.GetString(decodedBytes);
        
        var parser = serviceProvider.GetKeyedService<IParseService>(request.Type);
        
        if (parser == null)
        {
            return Result<ParseResponse>.Failure($"{request.Type} type is not supported.");
        }

        var (count, processedData) = parser.Parse(decodedString);

        var response = new ParseResponse("Success", count, processedData);
        return Result<ParseResponse>.Success(response);
    }
}