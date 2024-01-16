using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Infrastructure.ResultHandler
{
    public interface IResultHandler
    {
       IActionResult DataResult(object data);
    }
}