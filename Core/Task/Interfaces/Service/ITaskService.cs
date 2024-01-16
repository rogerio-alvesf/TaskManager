using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Interfaces.Service;
public interface ITaskService
{
    Task AddTask(InAddTask input);
    Task<IEnumerable<OutTask>> ConsultAllTasks();
    Task<OutTask> ConsultTaskById(int id_task);
    Task DeleteTaskById(int id_task);
    Task UpdateTaskById(int id_task, InUpdateTask input);
}