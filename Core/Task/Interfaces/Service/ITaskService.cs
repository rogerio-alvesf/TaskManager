using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Interfaces.Service
{
    public interface ITaskService
    {
        Task AddTask(InAddTask input);
        Task<IEnumerable<OutTask>> ConsultAllTasks();
        Task DeleteTask(int id_task);
        Task<OutTask> ConsultTaskById(int id_task);
    }
}