using RpmFsServices.Models;

namespace RpmFsServices.Helpers;

public static class DataExtension
{
    // This is a dummy extension to demonstrate real-life scenario
    // where mapping between the layers is needed.
    // For simplicity sakes, no third-party is used (like AutoMapper)
    
    public static RpmFsRepositories.Models.FuelPrice ToRepo(this FuelPrice fp)
    {
        return new RpmFsRepositories.Models.FuelPrice
        {
            PriceDate = fp.PriceDate,
            Price = fp.Price
        };
    }
}