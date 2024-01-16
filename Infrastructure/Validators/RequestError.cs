namespace TaskManager.Infrastructure.Validators;

public class RequestError
{
    public RequestError()
    {
        TraceId = Guid.NewGuid().ToString();
        Errors = new List<ErrorDetails>();
    }

    public RequestError(string logref, string detail)
    {
        TraceId = Guid.NewGuid().ToString();
        Errors = new List<ErrorDetails>();
        AddError(logref, detail);
    }

    public string TraceId { get; set; }
    public List<ErrorDetails> Errors { get; set; }

    public class ErrorDetails
    {
        public ErrorDetails(string logref, string detail)
        {
            TraceId = logref;
            Detail = detail;    
        }

        public string Detail { get; set; }
        public string TraceId { get; set; } 
    }

    public void AddError(string logref, string detail)
    {
        Errors.Add(new ErrorDetails(logref, detail));
    }
}