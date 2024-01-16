using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Interfaces.Repository
{
    public interface ITaskRepository
    {
        Task AddTask(InAddTask input);
        Task<IEnumerable<OutTask>> ConsultAllTasks();
        Task<OutTask> ConsultTaskById(int id_task);
    }
}