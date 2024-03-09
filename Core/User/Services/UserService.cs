using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;
using TaskManager.Infrastructure.Validators.Intefaces;

namespace TaskManager.Core.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IBase64ValidatorService _base64ValidatorService;

    public UserService(
        IUserRepository userRepository,
        IBase64ValidatorService base64ValidatorService
    )
    {
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
}