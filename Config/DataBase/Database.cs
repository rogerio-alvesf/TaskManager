

using System.Data;
using Microsoft.Data.SqlClient;

namespace TaskManager.Config;
public class Database : IDatabase
{
  public IDbConnection GetConnection()
  {
    var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json");

    IConfiguration configuration = builder.Build();

    // Obtém a string de conexão do arquivo de configuração
    string connectionString = configuration.GetConnectionString("MyDatabaseConnection");

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
      try
      {
        connection.Open();
        Console.WriteLine("Conexão bem-sucedida!");

        // Aqui você pode realizar outras operações no banco de dados, se necessário.

      }
      catch (Exception ex)
      {
        Console.WriteLine("Erro ao tentar conectar-se ao banco de dados: " + ex.Message);
      }
    }
    return new SqlConnection(connectionString);

  }
}