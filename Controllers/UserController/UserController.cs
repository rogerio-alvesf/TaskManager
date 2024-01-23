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
    private readonly IUserService _userService;
    public UserController(IUserService userService) => _userService = userService;

    [HttpPost]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RegisterUser([FromBody] InRegisterUser input)
    {
        await _userService.RegisterUser(input);
        return Ok();
    }
}