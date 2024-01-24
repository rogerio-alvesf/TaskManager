using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Input;
using TaskManager.Infrastructure.Authenticate;
using TaskManager.Infrastructure.Token;

namespace TaskManager.Controllers;
[AllowAnonymous]
[ApiController]
[Route("user")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthenticateService _authenticateService;
    public UserController(
        IUserService userService,
        IAuthenticateService authenticateService
    )
    {
        _userService = userService;
        _authenticateService = authenticateService;
    }

    [HttpPost]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RegisterUser([FromBody] InRegisterUser input)
    {
        await _userService.RegisterUser(input);
        return Ok();
    }

    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(OutTokenUser), 200)]
    public async Task<IActionResult> AuthenticateUser([FromBody] InAuthenticateUser input)
    {

        var user = await _userService.AuthenticateUser(input);

        var token = _authenticateService.GenerateToken(user.ID_User, input.Email);

        var result = new OutTokenUser(token);

        return Ok(result);
    }
}