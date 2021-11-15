using System;
using Management.Interface;

namespace Management.Clients
{
    /// <summary>
    /// Representation of the client for the COVID-19 data API.
    /// </summary>
    public class CovidDataClient : ICovidDataClient
    {
        public int CalculateScoreAsync()
            => throw new NotImplementedException();
    }
}
