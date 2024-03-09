using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Repositories;
using TaskManager.Core.Services;
using TaskManager.Infrastructure.EncryptService;
using TaskManager.Infrastructure.Authenticate;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaskManager.Config;
using TaskManager.Infrastructure.Validators;
using TaskManager.Infrastructure.Validators.Intefaces;

namespace TaskManager
{
    public static class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IApplicationSessionService, ApplicationSessionService>();

            services.AddScoped<IEncryptService, EncryptService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IDatabase, Database>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IBase64ValidatorService, Base64ValidatorService>();
        }
    }
}
