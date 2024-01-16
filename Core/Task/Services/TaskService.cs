using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task AddTask(InAddTask input)
        {
            await _taskRepository.AddTask(input);
        }

        public async Task<IEnumerable<OutTask>> ConsultAllTasks()
        {
            return await _taskRepository.ConsultAllTasks();
        }

        public async Task DeleteTask(int id_task)
        {
            // return await
        }

        public async Task<OutTask> ConsultTaskById(int id_task)
        {
            return await _taskRepository.ConsultTaskById(id_task);
        }
    }
}