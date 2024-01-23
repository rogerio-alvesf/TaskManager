using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Repositories;
using TaskManager.Core.Services;
using Newtonsoft.Json;
using TaskManager.Infrastructure.JsonConverterResolver;
using TaskManager.Config;
using TaskManager.Infrastructure.Middlewares;
using TaskManager.Infrastructure.EncryptService;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllers();

// Saiba mais sobre a configuração do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IEncryptService, EncryptService>();
builder.Services.AddScoped<IDatabase, Database>();

builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new LowerCaseCoverterResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    // options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

var app = builder.Build();

// Configura o pipeline de solicitações HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
