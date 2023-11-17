using TaskManager.Core.V1.Models.Input;
using TaskManager.Core.V1.Models.Output;

namespace TaskManager.Core.V1.Interfaces.Repository
{
    public interface ITaskRepository
    {
        Task<OutAddTask> AddTask(InAddTask input);
    }
}