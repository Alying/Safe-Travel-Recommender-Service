using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Interface;

namespace Management
{
    public class DecisionEngine : IDecisionEngine
    {
        private readonly ICovidDataClient _covidDataClient;
        private readonly IWheatherDataClient _wheatherDataClient;
        private readonly IAirQualityDataClient _airQualityDataClient;

        public DecisionEngine(
            ICovidDataClient covidDataClient,
            IWheatherDataClient wheatherDataClient,
            IAirQualityDataClient airQualityDataClient)
        {
            _covidDataClient = covidDataClient ?? throw new ArgumentNullException(nameof(covidDataClient));
            _wheatherDataClient = wheatherDataClient ?? throw new ArgumentNullException(nameof(covidDataClient));
            _airQualityDataClient = airQualityDataClient ?? throw new ArgumentNullException(nameof(airQualityDataClient));
        }

        public Task<IEnumerable<Recommendation>> CalculateDesiredLocationAsync()
        {
            var result = 0.3 * _covidDataClient.CalculateScoreAsync() + 0.3 * _wheatherDataClient.CalculateScoreAsync() + 0.3 * _airQualityDataClient.CalculateScoreAsync();

            return Task.FromResult(Enumerable.Empty<Recommendation>());
        }
    }
}
