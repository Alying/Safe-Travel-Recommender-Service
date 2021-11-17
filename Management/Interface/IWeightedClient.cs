using Management.DomainModels;
using Management.Enum;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Management.Interface
{
    public interface IWeightedClient
    {
        Task<IEnumerable<City>> GetDefaultCitiesAsync(
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken);

        Task<Dictionary<City, int>> CalculateScoresAsync(
            IEnumerable<City> city,
            State state,
            CountryCode countryCode,
            CancellationToken cancellationToken);
    }
}
