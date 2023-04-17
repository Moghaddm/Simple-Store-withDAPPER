using Microsoft.Extensions.Configuration.Json;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace Cart.Infrastructure;

public class StoreContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public StoreContext(IConfiguration configuration) =>
        (_connectionString, _configuration) = (
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ConnectionStrings:PostgreSQL")
                .Value,
            configuration
        );

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
