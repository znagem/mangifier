namespace Mangifier.Api.DataAccess;

public sealed class ServiceError(string message, Exception? exception = null)
{
    public Exception? Exception { get; set; } = exception;
    public string Message { get; set; } = message;
}