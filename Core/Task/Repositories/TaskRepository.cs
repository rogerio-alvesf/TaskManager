using System.Data;
using Dapper;
using TaskManager.Config;
using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDatabase _dataBase;
        public TaskRepository(IDatabase dataBase) => _dataBase = dataBase;

        public async Task AddTask(InAddTask input)
        {
            using var connection = _dataBase.GetConnection();

            DynamicParameters parametros = new DynamicParameters();
            parametros.Add("@Description_Task", input.Description_Task);
            parametros.Add("@NM_User_Inclusion", input.NM_User_Inclusion);

            await connection.ExecuteAsync(
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
                            ,DT_Change
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
                            ,DT_Change
                       FROM dbo.Task
                       WHERE ID_Task = @ID_Task",
               commandType: CommandType.Text,
               param: parameters
           );

            return result;
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
    }
}