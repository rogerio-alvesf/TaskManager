using System.Data;
using Microsoft.Data.SqlClient;

namespace TaskManager.Config;
public class Database : IDatabase
{
  public IDbConnection GetConnection()
  {
    string connectionString;

    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
    {
      var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

      IConfiguration configuration = builder.Build();

      connectionString = configuration.GetConnectionString("MyDatabaseConnection");
    }
    else
    {
      connectionString = $"Server=taskmanager-db;Database=TaskManager;User Id=sa;Password=YourStrong@Passw0rd;MultipleActiveResultSets=true";
    }

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
      connection.Open();
    }

    return new SqlConnection(connectionString);
  }
}