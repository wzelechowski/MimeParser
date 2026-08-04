namespace MimeParser.Payload;

public record ParseRequest(
    ParseType Type,
    string Content
    );