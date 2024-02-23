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
      // connectionString = $"Server=taskmanager-db;Database=TaskManager;User Id=sa;Password=YourStrong@Passw0rd;MultipleActiveResultSets=true";
      connectionString = $"Server=taskmanager-sql;Database=TaskManager;User Id=SA;Password=YourStrong@Passw0rd;MultipleActiveResultSets=true;Trusted_Connection=False;Encrypt=False;";
    }

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
      connection.Open();
    }

    return new SqlConnection(connectionString);
  }
}