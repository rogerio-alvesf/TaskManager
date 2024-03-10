using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Controllers;
[Authorize]
[ApiController]
[Route("task")]
[Produces("application/json")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(
        ITaskService taskService
    )
    {
        _taskService = taskService;
    }

    [HttpPost]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Add([FromBody] InAddTask input)
    {
        await _taskService.AddTask(input);
        return Ok();
    }

    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<OutTask>), 200)]
    public async Task<IActionResult> ConsultAllTasks()
    {
        var tasks = await _taskService.ConsultAllTasks();
        return Ok(tasks);
    }

    [HttpGet]
    [ProducesResponseType(typeof(OutTask), 200)]
    public async Task<IActionResult> ConsultTaskById([FromQuery] int id_task)
    {
        var task = await _taskService.ConsultTaskById(id_task);
        return Ok(task);
    }

    [HttpDelete]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteTaskById([FromQuery] int id_task)
    {
        await _taskService.DeleteTaskById(id_task);

        return Ok();
    }

    [HttpPatch]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateTaskById([FromQuery] int id_task, [FromBody] InUpdateTask input)
    {
        await _taskService.UpdateTaskById(id_task, input);

        return Ok();
    }
}