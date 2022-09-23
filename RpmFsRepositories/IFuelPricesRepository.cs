using RpmFsRepositories.Models;

namespace RpmFsRepositories;

public interface IFuelPricesRepository
{
    /// <summary>
    /// Do an upsert/merge operation
    /// </summary>
    /// <param name="data">Data</param>
    /// <returns></returns>
    Task UpsertAsync(IEnumerable<FuelPrice> data);
}