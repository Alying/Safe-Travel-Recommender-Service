using System.Threading;
using System.Threading.Tasks;
using Management.Clients.Models;
using Management.DomainModels;
using Management.Enum;

namespace Management.Interface
{
    /// <summary>
    /// Interface for weather data client functions
    /// </summary>
    public interface IWeatherDataClient : IWeightedClient
    {
    }
}
