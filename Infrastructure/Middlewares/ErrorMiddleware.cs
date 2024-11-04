using System.Net;
using Newtonsoft.Json;
using TaskManager.Infrastructure.Validators;

namespace TaskManager.Infrastructure.Middlewares;

public class ErrorMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public ErrorMiddleware(RequestDelegate requestDelegate) 
        => _requestDelegate = requestDelegate;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        var requestError = new RequestError(statusCode.ToString(), ex.Message);

        var exceptionResponseType = new Dictionary<string, HttpStatusCode>()
        {
            { "DBConcurrencyException", HttpStatusCode.BadRequest },
            { "NotFoundException", HttpStatusCode.NotFound },
            { "ConflictException", HttpStatusCode.Conflict },
            { "UnauthorizedException", HttpStatusCode.Unauthorized },
            { "ForbiddenException", HttpStatusCode.Forbidden },
            { "UnsupportedMediaTypeException", HttpStatusCode.UnsupportedMediaType },
            { "UnprocessableEntityException", HttpStatusCode.UnprocessableEntity }
        };

        var typeException = ex.GetType().ToString().Replace("System.", "");

        if (exceptionResponseType.TryGetValue(typeException, out HttpStatusCode statusCodeEx))
        {
            statusCode = statusCodeEx;
            requestError.Errors.TraceId = statusCodeEx.ToString();
        }

        var result = JsonConvert.SerializeObject(requestError);
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(result);
    }
}

public static class ErrorMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<ErrorMiddleware>();
}