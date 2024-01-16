namespace TaskManager.Infrastructure.Validators;

public class RequestError
{
    public RequestError()
    {
        TraceId = Guid.NewGuid().ToString();
        Errors = new List<ErrorDetails>();
    }

    public RequestError(string logref, string message)
    {
        TraceId = Guid.NewGuid().ToString();
        Errors = new List<ErrorDetails>();
        AddError(logref, message);
    }

    public string TraceId { get; set; }
    public List<ErrorDetails> Errors { get; set; }

    public class ErrorDetails
    {
        public ErrorDetails(string logref, string message)
        {
            TraceId = logref;
            Message = message;    
        }

        public string Message { get; set; }
        public string TraceId { get; set; } 
    }

    public void AddError(string logref, string message)
    {
        Errors.Add(new ErrorDetails(logref, message));
    }
}