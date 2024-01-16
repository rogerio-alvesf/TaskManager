using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TaskManager.Config;
using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDatabase _dataBase;
        public TaskRepository(
            IDatabase dataBase
        ) => _dataBase = dataBase;

        public async Task AddTask(InAddTask input)
        {
            using var connection = _dataBase.GetConnection();

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@Description_Task", input.Description_Task);
            parametros.Add("@NM_User_Inclusion", input.NM_User_Inclusion);

            connection.Execute(
                sql: @"INSERT INTO dbo.Task (Description_Task
                                          ,DT_Created
                                          ,NM_User_Inclusion)
                       VALUES(@Description_Task
                             ,GetDate()
                             ,@NM_User_Inclusion)",
                param: parametros,
                commandType: CommandType.Text
            );
        }

        public async Task<IEnumerable<OutTask>> ConsultAllTasks()
        {
            using var connection = _dataBase.GetConnection();

             var result = await connection.QueryAsync<OutTask>(
                sql: @"SELECT ID_Task
                             ,Description_Task
                             ,DT_Created
                             ,NM_User_Inclusion
                       FROM dbo.Task",
                commandType: CommandType.Text
            );

            return result;
        }

        public async Task<OutTask> ConsultTaskById(int id_task)
        {
             using var connection = _dataBase.GetConnection();

             var parameters = new DynamicParameters();
             parameters.Add("@ID_Task", id_task);

             var result = await connection.QueryFirstOrDefaultAsync<OutTask>(
                sql: @"SELECT ID_Task
                             ,Description_Task
                             ,DT_Created
                             ,NM_User_Inclusion
                       FROM dbo.Task
                       WHERE ID_Task = @ID_Task",
                commandType: CommandType.Text,
                param: parameters
            );

            return result;
        }
    }
}