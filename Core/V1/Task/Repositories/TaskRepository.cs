using TaskManager.Core.V1.Interfaces.Repository;
using TaskManager.Core.V1.Models.Input;
using TaskManager.Core.V1.Models.Output;

namespace TaskManager.Core.V1.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public TaskRepository()
        {

        }

        public async Task<OutAddTask> AddTask(InAddTask input)
        {
            return new OutAddTask {};
        }
    }
}