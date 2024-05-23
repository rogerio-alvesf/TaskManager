using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Input;
using TaskManager.Infrastructure.Authenticate;
using TaskManager.Infrastructure.Token;

namespace TaskManager.Controllers;
[Authorize]
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

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RegisterUser([FromBody] InRegisterUser input)
    {
        await _userService.RegisterUser(input);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(OutTokenUser), 200)]
    public async Task<IActionResult> AuthenticateUser([FromBody] InAuthenticateUser input)
    {

        var user = await _userService.AuthenticateUser(input);

        var token = _authenticateService.GenerateToken(user.ID_User, input.Email);

        var result = new OutTokenUser(token);

        return Ok(result);
    }

    [HttpPut("profile-picture")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateProfilePicture([FromBody] string picture_user)
    {
        await _userService.UpdateProfilePicture(picture_user);

        return Ok();
    }

    [HttpPost("send-email-reset-password")]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> SendPasswordResetEmail([FromQuery] string email_user)
    {   
        return Ok(await _userService.SendPasswordResetEmail(email_user));
    }
}