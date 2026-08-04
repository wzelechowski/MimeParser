using MimeParser.Common;
using MimeParser.Payload;

namespace MimeParser.Services;

public interface IProcessingService
{
    Result<ParseResponse> ParseAndProcess(ParseRequest request);
}