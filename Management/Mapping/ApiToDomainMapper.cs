using Management.DomainModels;
using Management.Enum;
using System;
using ApiUser = Management.ApiModels.User;
using DomainUser = Management.DomainModels.User;
using ApiComment = Management.ApiModels.Comment;

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

        public static Location toDomain(string countryCode, string stateCode)
            => new Location(Country.Wrap(countryCode), State.Wrap(stateCode));

        public static Comment toDomain(Location location, ApiComment apiComment)
            => new Comment(location, UserId.Wrap(apiComment.UserIdStr), apiComment.CommentStr);

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
