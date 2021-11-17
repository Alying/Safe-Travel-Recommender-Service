using Management.DomainModels;
using Management.Enum;
using System;

namespace Management.Mapping
{
    public static class CountryStateValidator
    {
        public static (CountryCode validatedCountry, State validatedState) ValidateCountryState(string countryCode, string state)
        {
            if (string.IsNullOrEmpty(countryCode) || string.IsNullOrEmpty(state))
            {
                throw new Exception("Invalid country and/or state");
            }

            if (System.Enum.TryParse<CountryCode>(countryCode, out var usCountryCode))
            {
                if (System.Enum.TryParse<UsState>(state, out _))
                {
                    return (usCountryCode, State.Wrap(state));
                }

                throw new Exception($"State: {state} is not valid in USA");
            }

            if (System.Enum.TryParse<CountryCode>(countryCode, out var caCountryCode))
            {
                if (System.Enum.TryParse<CaState>(state, out _))
                {
                    return (caCountryCode, State.Wrap(state));
                }

                throw new Exception($"State: {state} is not valid in Canada");
            }

            throw new Exception($"Country: {countryCode} not supported");
        }
    }
}
