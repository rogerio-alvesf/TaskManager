using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Repositories;
using TaskManager.Core.Services;
using Newtonsoft.Json;
using TaskManager.Infrastructure.JsonConverterResolver;
using TaskManager.Config;
using TaskManager.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllers();

// Saiba mais sobre a configuração do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
