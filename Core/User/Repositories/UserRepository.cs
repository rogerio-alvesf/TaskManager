using System.Data;
using Dapper;
using TaskManager.Config;
using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;
using TaskManager.Infrastructure.EncryptService;

namespace TaskManager.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDatabase _dataBase;
    private readonly IEncryptService _encryptService;
    public UserRepository(
        IDatabase dataBase,
        IEncryptService encryptService
    )
    {
        _dataBase = dataBase;
        _encryptService = encryptService;
    }

    public async Task RegisterUser(InRegisterUser input)
    {
        using var connection = _dataBase.GetConnection();

        DynamicParameters parametros = new DynamicParameters();
        parametros.Add("@NM_User", input.NM_User);
        parametros.Add("@Email_User", input.Email_User);
        parametros.Add("@DT_Birth", input.DT_Birth);
        parametros.Add("@User_Gender", input.User_Gender);
        parametros.Add("@Password_User", _encryptService.EncryptData(input.Password_User));

        await connection.ExecuteAsync(
            sql: @"INSERT INTO dbo.User(NM_User
                                           ,Email_User
                                           ,DT_Birth
                                           ,User_Gender
                                           ,Password_User)
                       VALUES(@NM_User
                             ,@Email_User
                             ,@DT_Birth
                             ,@User_Gender
                             ,@Password_User)",
            param: parametros,
            commandType: CommandType.Text
        );
    }
}