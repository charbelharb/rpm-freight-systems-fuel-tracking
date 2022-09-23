using System.Text.Json.Serialization;

namespace RpmFsServices.Models;

public class EiaSeriesModel
{
    // We only need to de-serialize this field in our case
    public IEnumerable<IEnumerable<dynamic>> Data { get; set; }
}