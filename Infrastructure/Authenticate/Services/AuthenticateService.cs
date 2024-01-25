using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Authenticate;

public class AuthenticateService : IAuthenticateService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthenticateService(
        IConfiguration configuration,
        IUserRepository userRepository
    )
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<bool> AuthenticateAsync(InAuthenticateUser input)
    {
        if (await UserExists(input.Email))
        {
            var user = await _userRepository.AuthenticateUser(input);
            return user.ID_User != 0 ? true : false;
        }

        return false;
    }
    public async Task<bool> UserExists(string email)
    {
        return await _userRepository.UserExists(email);
    }

    public string GenerateToken(int id, string email)
    {

        var tokenHandler = new JwtSecurityTokenHandler();

        var keyValue = _configuration.GetSection("Jwt:SecretKey").Value;

        if (keyValue == null)
            throw new Exception("An error occurred in server authentication");

        var key = Encoding.ASCII.GetBytes(keyValue);

        var claims = new[]
        {
            new Claim("id", id.ToString()),
            new Claim("email", email),
        };

        var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var claimsPrincipal = new ClaimsPrincipal(identity);
        Thread.CurrentPrincipal = claimsPrincipal;

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}