using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Enum;
using Management.Interface;

namespace Management.Clients
{
    public class WeatherDataClient : IWeatherDataClient
    {
        public Task<Dictionary<City, int>> CalculateScoresAsync(IEnumerable<City> city, State state, CountryCode countryCode, CancellationToken cancellationToken)
            => Task.FromResult(new Dictionary<City, int>());

        public Task<IEnumerable<City>> GetDefaultCitiesAsync(State state, CountryCode countryCode, CancellationToken cancellationToken)
            => Task.FromResult(Enumerable.Empty<City>());
    }
}
