using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TaskManager.Infrastructure.Validators;

namespace TaskManager.Infrastructure.ResultHandler;

public class ResultHandler : IResultHandler
{
    public IActionResult DataResult(object? data)
    {
        RequestError error;

        if (data != null)
        {
            return new ObjectResult(data);
        }

        if (data == null)
        {
            error = new RequestError("2", "Not Found Response");
            return new ObjectResult(error) { StatusCode = 400 };
        }

        error = new RequestError("3", "Not Found Response");
        return new ObjectResult(error) { StatusCode = 500 };
    }
}