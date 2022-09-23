using Microsoft.Extensions.Configuration;
using RestSharp;
using RpmFsServices.Models;

namespace RpmFsServices.EiaHttpClient;

public class EiaApiClient : IEiaApiClient
{
    #region Private Fields
    
    private readonly RestClient _restClient;
    private readonly string _eiaEndpoint;
    
    #endregion Private Fields
    
    public EiaApiClient(IConfiguration configuration)
    {
        _eiaEndpoint = configuration["EiaEndpoint"];
        _restClient = new RestClient(_eiaEndpoint);
    }

    public async Task<EiaFuelPriceResponse> GetFuelPricesAsync()
    {
        var request = GenerateRequest(_eiaEndpoint);
        var response = await _restClient.ExecuteAsync<EiaFuelPriceResponse>(request);
        if (response.IsSuccessful && response.Data != null)
            return response.Data;

        // we can add custom exception handling if needed
        throw new Exception();
    }
    
    private static RestRequest GenerateRequest(string url)
    {
        var request = new RestRequest(url);
        request.AddHeader("Content-Type", "application/json; charset=utf-8");
        request.AddHeader("Accept", "application/json");
        request.RequestFormat = DataFormat.Json;
        return request;
    }
}