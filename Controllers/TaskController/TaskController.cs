using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.V1.Interfaces.Service;
using TaskManager.Core.V1.Models.Input;

namespace TaskManager.Controllers;

[ApiController]
[Route("task")]
public class TaskController : ControllerBase
{
    
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost()]
    public IActionResult Add([FromBody] InAddTask input)
    {
        var result = _taskService.AddTask(input);
        return Ok(result);
    }
}
