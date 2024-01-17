using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Config;
using TaskManager.Core.Services;
using TaskManager.Core.Repositories;
using TaskManager.Infrastructure.EncryptService;

namespace TaskManager;
public static class DomainCoreModule
{
    public static void AddDomain(this IServiceCollection service)
    {
        service.AddScoped<ITaskRepository, TaskRepository>();
        service.AddScoped<IEncryptService, EncryptService>();
        service.AddScoped<ITaskService, TaskService>();
        service.AddScoped<IDatabase, Database>();
    }
}