using System;
using Management.DomainModels;
using Management.Enum;

namespace Management.Mapping
{
    /// <summary>
    /// Validate country and state
    /// </summary>
    public static class CountryStateValidator
    {
        /// <summary>
        /// Enumeration for all U.S. states
        /// </summary>
        /// <param name="countryCode">country code eg. US.</param>
        /// <param name="state">state code eg. NY.</param>
        /// <returns>The parsed country code and state code.</returns>
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

        /// <summary>
        /// Validate if the given country is supported by our service
        /// </summary>
        /// <param name="countryCode">country eg. US.</param>
        /// <returns>the enum country code.</returns>
        public static CountryCode ValidateCountry(string countryCode)
        {
            if (System.Enum.TryParse<CountryCode>(countryCode, out var enumCode))
            {
                return enumCode;
            }

            throw new Exception($"Country : {countryCode} not supported");
        }
    }
}
