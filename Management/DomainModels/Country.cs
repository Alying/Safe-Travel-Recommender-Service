using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Management.DomainModels
{
    public class Country: TaggedString<Country>
    {
        private enum CountryCode
        {
            UNKNOWN = 0,
            US = 1,
            CA = 2,
        }

        private Country(string countryCode) : base(countryCode)
        {
            if(!System.Enum.TryParse<CountryCode>(countryCode, out var _))
            {
                throw new ArgumentException("Invalid countryCode: " + countryCode);
            }
        }

        public static Country Wrap(string countryCode) => new Country(countryCode);
    }
}
