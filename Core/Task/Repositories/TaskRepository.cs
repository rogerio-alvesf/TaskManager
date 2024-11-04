using System.Data;
using Dapper;
using TaskManager.Config;
using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDatabase _dataBase;
        private readonly IApplicationSessionService _applicationSessionService;
        public TaskRepository(
            IDatabase dataBase,
            IApplicationSessionService applicationSessionService
        )
        {
            _dataBase = dataBase;
            _applicationSessionService = applicationSessionService;
        }

        public async Task AddTask(InAddTask input)
        {
            using var connection = _dataBase.GetConnection();

            var applicationSession = _applicationSessionService.Obtain();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Description_Task", input.Description_Task);
            parameters.Add("@ID_User", applicationSession.ID_User);

            await connection.ExecuteAsync(
                sql: @"INSERT INTO dbo.Task(ID_User
                                           ,Description_Task
                                           ,DT_Created)
                       VALUES(@ID_User
                             ,@Description_Task
                             ,GetDate())",
                param: parameters,
                commandType: CommandType.Text
            );
        }

        public async Task<IEnumerable<OutTask>> ConsultAllTasks()
        {
            using var connection = _dataBase.GetConnection();

            var result = await connection.QueryAsync<OutTask>(
               sql: @"SELECT ID_Task           = Task.ID_Task
                            ,Description_Task  = Task.Description_Task
                            ,DT_Created        = Task.DT_Created
                            ,DT_Change         = Task.DT_Change
                       FROM dbo.Task WITH (NOLOCK)
                       INNER JOIN dbo.User_System WITH (NOLOCK)
                       ON User_System.ID_User = Task.ID_User",
               commandType: CommandType.Text
           );

            return result;
        }

        public async Task<OutTask> ConsultTaskById(int id_task)
        {
            using var connection = _dataBase.GetConnection();

            return await connection.QueryFirstAsync<OutTask>(
               sql: @"SELECT ID_Task           = Task.ID_Task
                            ,Description_Task  = Task.Description_Task
                            ,DT_Created        = Task.DT_Created
                            ,NM_User_Inclusion = User_System.NM_User
                            ,DT_Change         = Task.DT_Change
                       FROM dbo.Task WITH (NOLOCK)
                       INNER JOIN dbo.User_System WITH (NOLOCK)
                       ON User_System.ID_User = Task.ID_User
                       WHERE ID_Task = @ID_Task",
               commandType: CommandType.Text,
               param: new { id_task }
           );
        }

        public async Task DeleteTaskById(int id_task)
        {
            using var connection = _dataBase.GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@ID_Task", id_task);

            await connection.ExecuteAsync(
              sql: @"DELETE
                     FROM dbo.Task
                     WHERE ID_Task = @ID_Task",
              commandType: CommandType.Text,
              param: parameters
          );
        }

        public async Task UpdateTaskById(int id_task, InUpdateTask input)
        {
            using var connection = _dataBase.GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@ID_Task", id_task);
            parameters.Add("@Description_Task", input.Description_Task);

            await connection.ExecuteAsync(
              sql: @"UPDATE dbo.Task
                     SET Description_Task = @Description_Task
                        ,DT_Change        = GETDATE()
                     WHERE ID_Task = @ID_Task",
              commandType: CommandType.Text,
              param: parameters
          );
        }

        public async Task<bool> IsTaskOwner(int id_task)
        {
            using var connection = _dataBase.GetConnection();

            var applicationSession = _applicationSessionService.Obtain();

            var parameters = new DynamicParameters();
            parameters.Add("@ID_Task", id_task);
            parameters.Add("@ID_User", applicationSession.ID_User);

            return await connection.QueryFirstOrDefaultAsync<bool>(
                sql: @"SELECT 1
                       FROM dbo.Task
                       WHERE ID_Task = @ID_Task
                         AND ID_User = @ID_User",
                commandType: CommandType.Text,
                param: parameters
            );
        }

        public async Task<bool> IsExistsTask(int id_task)
        {
            using var connection = _dataBase.GetConnection();

            return await connection.QueryFirstOrDefaultAsync<bool>(
                sql: @"SELECT 1
                       FROM dbo.Task
                       WHERE ID_Task = @ID_Task",
                commandType: CommandType.Text,
                param: new { id_task }
            );
        }
    }
}