using Management.DomainModels;
using Management.Enum;
using System;
using ApiUser = Management.ApiModels.User;
using DomainUser = Management.DomainModels.User;

namespace Management.Mapping
{
    public class ApiToDomainMapper
    {
        public static DomainUser ToDomain(ApiUser api)
            => new DomainUser(
            userId: UserId.Wrap(api.UserId),
            fullName: FullName.Wrap(api.FullName),
            passportId: PassportId.Wrap(api.PassportId),
            createAt: DateTime.UtcNow,
            countryCode: GetCountryCode(api.CountryCode));

        private static CountryCode GetCountryCode(string value)
        {
            if (System.Enum.TryParse<CountryCode>(value, out var enumValue))
            {
                return enumValue;
            }

            return CountryCode.Unknown;
        }
    }
}
