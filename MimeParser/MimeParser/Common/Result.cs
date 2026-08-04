namespace MimeParser.Common;

public record Result<T>(bool IsSuccess, T? Value, string? ErrorMessage)
{
    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(string errorMessage) => new(false, default, errorMessage);
}