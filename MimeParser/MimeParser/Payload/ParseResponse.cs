namespace MimeParser.Payload;

public record ParseResponse(
    string Status,
    int ProcessedCount,
    object? Data
);
