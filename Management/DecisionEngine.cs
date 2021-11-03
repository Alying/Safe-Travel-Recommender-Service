using Management.DomainModels;
using Management.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public Task<IReadOnlyList<Recommendation>> CalculateDesiredLocationAsync()
        => throw new NotImplementedException();
    }
}
