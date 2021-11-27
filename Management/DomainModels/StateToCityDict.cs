using System;
using System.Collections.Generic;
using System.Text;

namespace Management.DomainModels
{
    public class StateToCityDict
    {
        public StateToCityDict()
        {
            stateToCityDict = new Dictionary<string, List<City>>()
            {
                { "CA", new List<City>() { City.Wrap("Los Angeles"), City.Wrap("San Diego"), City.Wrap("San Jose"), City.Wrap("San Francisco"), City.Wrap("Fresno") } },
                { "GA", new List<City>() { City.Wrap("Atlanta"), City.Wrap("Savannah"), City.Wrap("Athens"), City.Wrap("Roswell"), City.Wrap("Marietta") } },
                { "IL", new List<City>() { City.Wrap("Chicago"), City.Wrap("Naperville"), City.Wrap("Joliet"), City.Wrap("Rockford"), City.Wrap("Springfield") } },
                { "MA", new List<City>() { City.Wrap("Boston"), City.Wrap("Worcester"), City.Wrap("Springfield"), City.Wrap("Brockton"), City.Wrap("Quincy") } },
                { "WA", new List<City>() { City.Wrap("Seattle"), City.Wrap("Spokane"), City.Wrap("Tacoma"), City.Wrap("Vancouver"), City.Wrap("Bellevue") } },
            };
        }

        public Dictionary<string, List<City>> stateToCityDict { get; }
    }
}
