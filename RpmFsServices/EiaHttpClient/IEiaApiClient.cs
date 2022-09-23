using RpmFsServices.Models;

namespace RpmFsServices.EiaHttpClient;

public interface IEiaApiClient
{
    /// <summary>
    /// Get Fuel Price from EIA
    /// </summary>
    /// <returns></returns>
    Task<EiaFuelPriceResponse> GetFuelPricesAsync();
}