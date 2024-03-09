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

        public async Task<OutTask> ConsultTaskById(int id_task)
        {
            await IsTaskOwner(id_task, $"Permission denied to consult task id {id_task}");
            var task = await _taskRepository.ConsultTaskById(id_task);

            if (task == null)
                throw new NotFoundException("Unable to find task");

            return task;
        }

        public async Task DeleteTaskById(int id_task)
        {
            await ConsultTaskById(id_task);
            await IsTaskOwner(id_task);
            await _taskRepository.DeleteTaskById(id_task);
        }

        public async Task UpdateTaskById(int id_task, InUpdateTask input)
        {
            await ConsultTaskById(id_task);
            await IsTaskOwner(id_task);
            await _taskRepository.UpdateTaskById(id_task, input);
        }

        private async Task IsTaskOwner(int id_task)
        {
            if (!await _taskRepository.IsTaskOwner(id_task))
                throw new UnauthorizedException("User without permission");
        }

        private async Task IsTaskOwner(int id_task, string description_exception)
        {
            if (!await _taskRepository.IsTaskOwner(id_task))
                throw new UnauthorizedException(description_exception);
        }
    }
}