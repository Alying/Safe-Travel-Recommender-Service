using System.Threading;
using System.Threading.Tasks;
using Management.Clients.Models;
using Management.DomainModels;

namespace Management.Interface
{
    /// <summary>
    /// Interface for covid data client
    /// </summary>
    public interface ICovidDataClient : IWeightedClient
    {
    }
}
