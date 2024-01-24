using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Repositories;
using TaskManager.Core.Services;
using Newtonsoft.Json;
using TaskManager.Infrastructure.JsonConverterResolver;
using TaskManager.Config;
using TaskManager.Infrastructure.Middlewares;
using TaskManager.Infrastructure.EncryptService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManager.Infrastructure.Authenticate;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
builder.Services.AddControllers();

builder.Services.AddCors();

// Saiba mais sobre a configuração do Swagger/OpenAPI em https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWT AuthAuthentication", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
            });

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IEncryptService, EncryptService>();
builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
builder.Services.AddScoped<IDatabase, Database>();

var keyValue = builder.Configuration.GetSection("Jwt:SecretKey").Value;

if (keyValue == null)
    throw new Exception("Jwt:SecretKey não encontrada");

var key = Encoding.ASCII.GetBytes(keyValue);

builder.Services.AddAuthentication(x =>
   {
       x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
       x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   })
   .AddJwtBearer(x =>
   {
       x.RequireHttpsMetadata = false;
       x.SaveToken = true;
       x.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(key),
           ValidateIssuer = false,
           ValidateAudience = false
       };
   });

builder.Services.AddAuthorization();

builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new LowerCaseCoverterResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
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

app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
