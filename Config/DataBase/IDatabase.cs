using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Config;

public interface IDatabase
{
    IDbConnection GetConnection();
}