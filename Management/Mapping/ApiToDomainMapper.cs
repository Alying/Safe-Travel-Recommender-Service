using System;
using Management.DomainModels;
using Management.Enum;
using ApiComment = Management.ApiModels.Comment;
using ApiUser = Management.ApiModels.User;
using DomainUser = Management.DomainModels.User;

namespace Management.Mapping
{
    /// <summary>
    /// Maps api user to domain user
    /// </summary>
    public class ApiToDomainMapper
    {
        /// <summary>
        /// Maps domain api user to domain user
        /// </summary>
        /// <param name="api">api user.</param>
        /// <returns>domain user.</returns>
        // TODO: @mli: Update DomainUser to use Country.
        public static DomainUser ToDomain(ApiUser api)
            => new DomainUser(
            userId: UserId.Wrap(api.UserId),
            fullName: FullName.Wrap(api.FullName),
            passportId: PassportId.Wrap(api.PassportId),
            createAt: DateTime.UtcNow,
            countryCode: GetCountryCode(api.CountryCode));

        /// <summary>
        /// Maps api comment to domain comment
        /// </summary>
        /// <param name="location">location that comment made on.</param>
        /// <param name="apiComment">api comment.</param>
        /// <returns>domain user.</returns>
        public static Comment ToDomain(Location location, ApiComment apiComment)
            => new Comment(location, UserId.Wrap(apiComment.UserIdStr), apiComment.CommentStr);

        /// <summary>
        /// Get country code
        /// </summary>
        /// <param name="value">country string.</param>
        /// <returns>country code.</returns>
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
