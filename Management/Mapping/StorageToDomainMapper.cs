using System;
using Management.DomainModels;

namespace Management.Mapping
{
    /// <summary>
    /// Mapper class that maps storage user and comment to domain user and comment
    /// </summary>
    public class StorageToDomainMapper
    {
        /// <summary>
        /// Maps storage user to domain user
        /// </summary>
        /// <param name="storage">storage user.</param>
        /// <returns>domain user.</returns>
        public static User ToDomain(Common.StorageModels.User storage)
            => new User(
                userId: UserId.Wrap(storage.UserId),
                fullName: FullName.Wrap(storage.FullName),
                passportId: PassportId.Wrap(storage.PassportId),
                createAt: DateTimeOffset.TryParse(storage.CreatedAt, out var time) ? time : default(DateTimeOffset),
                countryCode: System.Enum.TryParse<Enum.CountryCode>(storage.CountryCode, out var country)
                ? country
                : Enum.CountryCode.Unknown);

        /// <summary>
        /// Maps storage comment to domain comment
        /// </summary>
        /// <param name="comment">storage comment.</param>
        /// <returns>domain comment.</returns>
        public static Comment ToDomain(StorageModels.Comment comment)
        {
            var validatedResult = CountryStateValidator.ValidateCountryState(comment.Country, comment.State);

            return new Comment(
                location: new Location(validatedResult.validatedCountry, validatedResult.validatedState),
                userId: UserId.Wrap(comment.UserId),
                commentStr: comment.CommentStr);
        }
    }
}
