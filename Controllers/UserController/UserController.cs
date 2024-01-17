using System.Net;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Controllers;

[ApiController]
[Route("user")]
[Produces("application/json")]
public class UserController : ControllerBase
{

    public UserController(
    )
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OutTask>), 200)]
    public async Task<IActionResult> ConsultAllTasks()
    {
        return Ok();
    }
}