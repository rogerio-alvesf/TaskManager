namespace TaskManager.Infrastructure.Validators;

 class RequestError
{
    public string TraceId { get; set; }
    public ErrorDetails Errors { get; set; }

    public RequestError(string logref, string detail)
    {
        TraceId = Guid.NewGuid().ToString();
        Errors = new ErrorDetails(logref, detail);
    }
}

public class ErrorDetails
{
    public string Detail { get; set; }
    public string TraceId { get; set; }

    public ErrorDetails(string logref, string detail)
    {
        TraceId = logref;
        Detail = detail;
    }
}