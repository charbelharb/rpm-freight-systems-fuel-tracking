using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RpmFsRepositories;
using RpmFsServices.EiaHttpClient;
using RpmFsServices.Helpers;
using RpmFsServices.Models;

namespace RpmFsServices.Jobs;

public class WeeklyFuelTracker
{
    private readonly IFuelPricesRepository _fuelPricesRepository;
    private readonly IEiaApiClient _eiaApiClient;
    private readonly int _maxDay;
    
    public WeeklyFuelTracker(IEiaApiClient eiaApiClient,
           IFuelPricesRepository fuelPricesRepository,
           IConfiguration configuration)
    {
        _eiaApiClient = eiaApiClient;
        _fuelPricesRepository = fuelPricesRepository;
        _maxDay = Convert.ToInt32(configuration["MaxDay"]);
    }
    
    public async Task ExecuteAsync()
    {
        var fuelDataResponse = await _eiaApiClient.GetFuelPricesAsync();
        var firstSeries = fuelDataResponse.Series.FirstOrDefault();
        
        // We can do something else if the series is null
        // like sending notification and so on
        if(firstSeries == null)
            return;
        
        var parsedData = ParseResponse(firstSeries.Data);
        var maxDate = DateTime.UtcNow.AddDays(-_maxDay).Date;
        parsedData = parsedData.Where(x => x.Date >= maxDate);
        var mappedData = parsedData.Select(x => x.ToRepo());
        await _fuelPricesRepository.UpsertAsync(mappedData);
    }

    private static IEnumerable<FuelPrice> ParseResponse(IEnumerable<IEnumerable<dynamic>> data)
    {
        return data.Select(row => row as dynamic[] ?? row.ToArray()).Select(enumerable =>
        {
            string pd = JsonSerializer.Deserialize<string>(enumerable.FirstOrDefault());
            return new FuelPrice
            {
                PriceDate = pd,
                Price = JsonSerializer.Deserialize<decimal>(enumerable.LastOrDefault()),
                Date = DateTime.ParseExact(pd,"yyyyMMdd", CultureInfo.InvariantCulture)
            };
        });
    }
}