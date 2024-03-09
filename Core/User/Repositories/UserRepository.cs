using System.Data;
using Dapper;
using TaskManager.Config;
using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;
using TaskManager.Infrastructure.EncryptService;

namespace TaskManager.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDatabase _dataBase;
    private readonly IEncryptService _encryptService;
    private readonly IApplicationSessionService _applicationSessionService;

    public UserRepository(
        IDatabase dataBase,
        IEncryptService encryptService,
        IApplicationSessionService applicationSessionService
    )
    {
        _dataBase = dataBase;
        _encryptService = encryptService;
        _applicationSessionService = applicationSessionService;
    }

    public async Task<OutUser> AuthenticateUser(InAuthenticateUser input)
    {
        using var connection = _dataBase.GetConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Email_User", input.Email);
        parameters.Add("@Password_User", _encryptService.EncryptData(input.Password));

        var result = await connection.QueryFirstOrDefaultAsync<OutUser>(
            sql: @"SELECT ID_User
                         ,NM_User
                         ,Email_User
                         ,DT_Birth
                         ,User_Gender
                        ,Avatar_User
                   FROM dbo.User_System
                   WHERE Email_User = @Email_User
                     AND Password_User = CONVERT(BINARY(64), @Password_User)",
            param: parameters,
            commandType: CommandType.Text
        );

        return result;
    }

    public async Task RegisterUser(InRegisterUser input)
    {
        using var connection = _dataBase.GetConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@NM_User", input.NM_User);
        parameters.Add("@Email_User", input.Email_User);
        parameters.Add("@DT_Birth", input.DT_Birth, DbType.DateTime, size: 8);
        parameters.Add("@User_Gender", input.User_Gender);
        parameters.Add("@Password_User", _encryptService.EncryptData(input.Password_User));

        await connection.ExecuteAsync(
            sql: @"INSERT INTO dbo.User_System(NM_User
                                              ,Email_User
                                              ,DT_Birth
                                              ,User_Gender
                                              ,Password_User)
                   VALUES(@NM_User
                         ,@Email_User
                         ,@DT_Birth
                         ,@User_Gender
                         ,CONVERT(BINARY(64), @Password_User))",
            param: parameters,
            commandType: CommandType.Text
        );
    }

    public async Task<bool> UserExists(string email)
    {
        using var connection = _dataBase.GetConnection();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Email_User", email);

        var result = await connection.QueryFirstOrDefaultAsync<bool>(
            sql: @"SELECT 1
                   FROM dbo.User_System WITH (NOLOCK)
                   WHERE Email_User = @Email_User",
            param: parameters,
            commandType: CommandType.Text
        );

        return result;
    }

    public async Task UpdateProfilePicture(string picture_user)
    {
        using var connection = _dataBase.GetConnection();

        var applicationSession = _applicationSessionService.Obtain();

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Avatar_User", picture_user);
        parameters.Add("@ID_User", applicationSession.ID_User);

        await connection.ExecuteAsync(
            sql: @"UPDATE dbo.User_System
                   SET Avatar_User = @Avatar_User
                   WHERE ID_User = @ID_User",
            param: parameters,
            commandType: CommandType.Text
        );
    }
}