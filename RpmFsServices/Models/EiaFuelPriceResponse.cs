namespace RpmFsServices.Models;

public class EiaFuelPriceResponse
{
    // We only need to de-serialize this field in our case
    public IEnumerable<EiaSeriesModel> Series { get; set; }
}