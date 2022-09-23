using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RpmFsRepositories.Models;
using Dapper;
using RpmFsRepositories.Helpers;

namespace RpmFsRepositories;

public class FuelPricesRepository : IFuelPricesRepository
{
    private readonly string _connectionString;

    private const string UpsertQuery = "UpsertFuelPrices.sql";

    private const string QueryFolder = "Queries";
    
    // real-life scenario we can use a base repository class
    public FuelPricesRepository(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionStrings:FuelTrackingConnection"];
    }

    public async Task UpsertAsync(IEnumerable<FuelPrice> data)
    {
        var query = ResourcesUtility.LoadQuery(UpsertQuery, QueryFolder);
        await using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(query, data);
    }
}