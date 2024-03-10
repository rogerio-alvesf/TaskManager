using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;
using TaskManager.Infrastructure.Smtp;
using TaskManager.Infrastructure.Validators.Intefaces;

namespace TaskManager.Core.Services;
public class UserService : IUserService
{
    private readonly ISmtpService _smtpService;
    private readonly IUserRepository _userRepository;
    private readonly IBase64ValidatorService _base64ValidatorService;

    public UserService(
        ISmtpService smtpService,
        IUserRepository userRepository,
        IBase64ValidatorService base64ValidatorService,
        IApplicationSessionService applicationSessionService
    )
    {
        _smtpService = smtpService;
        _userRepository = userRepository;
        _base64ValidatorService = base64ValidatorService;
    }

    public async Task<OutUser> AuthenticateUser(InAuthenticateUser input)
    {
        if (!await _userRepository.UserExists(input.Email))
            throw new UnauthorizedException("Authentication error");

        var user = await _userRepository.AuthenticateUser(input);

        if (user == null)
            throw new NotFoundException("Authentication error");

        return user;
    }

    public async Task RegisterUser(InRegisterUser input)
    {
        if (await _userRepository.UserExists(input.Email_User))
            throw new ConflictException("Sorry, something went wrong");

        await _userRepository.RegisterUser(input);
    }

    public async Task UpdateProfilePicture(string picture_user)
    {
        if (!_base64ValidatorService.IsBase64String(picture_user))
            throw new NotFoundException("Invalid Base64. Make sure the string provided is a valid Base64 code.");

        await _userRepository.UpdateProfilePicture(picture_user);
    }

    public async Task<string> SendPasswordResetEmail(string email_user)
    {
        if(!await _userRepository.UserExists(email_user))
            throw new BadHttpRequestException("Invalid email");

        string subject = "Reset your password ";
        string body = @"
                        <!DOCTYPE html>
                        <html lang=""en"">
                        <head>
                            <meta charset=""UTF-8"">
                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                            <title>Password Reset</title>
                            <style>
                                body {
                                    font-family: Arial, sans-serif;
                                    background-color: #f4f4f4;
                                    padding: 20px;
                                    text-align: center;
                                }
                        
                                .container {
                                    max-width: 600px;
                                    margin: 0 auto;
                                    background-color: #fff;
                                    padding: 40px;
                                    border-radius: 8px;
                                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                }
                        
                                h1 {
                                    color: #333;
                                }
                        
                                p {
                                    color: #666;
                                    margin-bottom: 20px;
                                }
                        
                                .button {
                                    display: inline-block;
                                    background-color: #007bff;
                                    color: #fff;
                                    padding: 10px 20px;
                                    border-radius: 4px;
                                    text-decoration: none;
                                    transition: background-color 0.3s ease;
                                }
                        
                                .button:hover {
                                    background-color: #0056b3;
                                }
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <h1>Password Reset</h1>
                                <p>You've requested a password reset. Click the button below to reset your password:</p>
                                <a href=""#"" class=""button"">Reset Password</a>
                            </div>
                        </body>
                        </html>
                    ";
        
        return _smtpService.SendEmail(email_user, subject, body);
    }

    public async Task UpdateUser(InUpdateUser input)
    {
        await _userRepository.UpdateUser(input);
    } 
}