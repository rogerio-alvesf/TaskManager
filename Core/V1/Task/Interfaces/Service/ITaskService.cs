using TaskManager.Core.V1.Models.Input;
using TaskManager.Core.V1.Models.Output;

namespace TaskManager.Core.V1.Interfaces.Service
{
    public interface ITaskService
    {
        Task<OutAddTask> AddTask(InAddTask input);
    }
}