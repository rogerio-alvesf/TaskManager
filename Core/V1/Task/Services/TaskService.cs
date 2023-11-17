using TaskManager.Core.V1.Interfaces.Repository;
using TaskManager.Core.V1.Interfaces.Service;
using TaskManager.Core.V1.Models.Input;
using TaskManager.Core.V1.Models.Output;

namespace TaskManager.Core.V1.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        
        public async Task<OutAddTask> AddTask(InAddTask input)
        {
            // await _taskRepository.AddTask(input)
            return new OutAddTask { ID_Task = 1, DT_Created = DateTime.Now, Description = input.Description, NM_User_Inclusion = input.NM_User_Inclusion};
        }
    }
}