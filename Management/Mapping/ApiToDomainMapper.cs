using Management.DomainModels;
using Management.Enum;
using System;
using ApiUser = Management.ApiModels.User;
using DomainUser = Management.DomainModels.User;

namespace Management.Mapping
{
    public class ApiToDomainMapper
    {
        // TODO: @mli: Update DomainUser to use Country.
        public static DomainUser ToDomain(ApiUser api)
            => new DomainUser(
            userId: UserId.Wrap(api.UserId),
            fullName: FullName.Wrap(api.FullName),
            passportId: PassportId.Wrap(api.PassportId),
            createAt: DateTime.UtcNow,
            countryCode: GetCountryCode(api.CountryCode));

        public static Country toDomain(string countryCode) => Country.Wrap(countryCode);

        public static Location toDomain(string countryCode, string stateCode) => new Location(Country.Wrap(countryCode), State.Wrap(stateCode));

        public static Comment toDomain(string countryCode, string stateCode, string userId, string comment) => new Comment(toDomain(countryCode, stateCode), UserId.Wrap(userId), comment);

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
