using System.Net;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;
using TaskManager.Infrastructure.ResultHandler;

namespace TaskManager.Controllers;

[ApiController]
[Route("task")]
[Produces("application/json")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IResultHandler _resultHandler;

    public TaskController(
        ITaskService taskService,
        IResultHandler resultHandler
    )
    {
        _taskService = taskService;
        _resultHandler = resultHandler;
    }

    [HttpPost]
    public IActionResult Add([FromBody] InAddTask input)
    {
        _taskService.AddTask(input);
        return Ok();
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<OutTask>), 200)]
    public async Task<IActionResult> ConsultAllTasks()
    {
        var result = await _taskService.ConsultAllTasks();
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(OutTask), 200)]
    public async Task<IActionResult> ConsultTaskById([FromQuery] int id_task)
    {
        var task = await _taskService.ConsultTaskById(id_task);

        if (task == null)
            throw new NotFoundException("Unable to find task");

        return Ok(task);
    }
}