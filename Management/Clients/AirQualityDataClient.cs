using Management.Clients.Models;
using Management.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Management.Clients
{
    public class AirQualityDataClient : IAirQualityDataClient
    {
        private IRestClient _restClient;

        private readonly IConfiguration _configuration;

        private string _apiKey => _configuration.GetConnectionString("airApiKey");

        public AirQualityDataClient(IConfiguration configuration)
        {
            _restClient = new RestClient("http://api.airvisual.com");
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<AirQualityCityResponse> GetAirQualityAsync(string city, string state, string countryCode, CancellationToken cancellationToken)
        {
            var request = new RestRequest("v2/city", Method.GET);
            request
                .AddQueryParameter("city", city)
                .AddQueryParameter("state", state)
                .AddQueryParameter("country", countryCode)
                .AddQueryParameter("key", _apiKey);

            var response = await _restClient.ExecuteAsync(request, cancellationToken);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return  JsonConvert.DeserializeObject<AirQualityCityResponse>(response.Content);
            }

            throw new Exception("Retrieve Data Failed");
        }

        public int CalculateScoreAsync()
            => throw new NotImplementedException();
    }
}
