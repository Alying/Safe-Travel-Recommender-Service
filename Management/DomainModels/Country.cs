using Common;
using System;

namespace Management.DomainModels
{
    public class Country: TaggedString<Country>
    {
        private enum CountryCode
        {
            US,
            CA,
        }

        private Country(string countryCode) : base(countryCode)
        {
            if(!System.Enum.TryParse<CountryCode>(countryCode, out var _))
            {
                throw new ArgumentException($"Invalid countryCode: {countryCode}");
            }
        }

        public static Country Wrap(string countryCode) => new Country(countryCode);
    }
}
