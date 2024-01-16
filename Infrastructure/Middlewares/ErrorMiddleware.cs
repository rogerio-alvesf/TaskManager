using System.Data;
using System.Net;
using Newtonsoft.Json;
using TaskManager.Infrastructure.Validators;

namespace TaskManager.Infrastructure.Middlewares;

public class ErrorMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public ErrorMiddleware(RequestDelegate requestDelegate) => _requestDelegate = requestDelegate;

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
        RequestError requestError;

        HttpStatusCode statusCode;

        if (ex.GetType() == typeof(DBConcurrencyException))
        {
            statusCode = HttpStatusCode.BadRequest;
            requestError = new RequestError(statusCode.ToString(), "Sorry, an error occurred while running");
        }
        else if (ex.GetType() == typeof(NotFoundException))
        {
            statusCode = HttpStatusCode.NotFound;
            requestError = new RequestError(statusCode.ToString(), "Couldn't find");
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                requestError = new RequestError(statusCode.ToString(), $"{ex.Message} {ex?.InnerException?.Message}");
            }
            else
            {
                requestError = new RequestError(statusCode.ToString(), "An internal server error has ocurred");
            }
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
    {
        return builder.UseMiddleware<ErrorMiddleware>();
    }
}