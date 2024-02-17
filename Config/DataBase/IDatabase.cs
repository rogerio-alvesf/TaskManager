using System.Data;

namespace TaskManager.Config;

public interface IDatabase
{
    IDbConnection GetConnection();
}