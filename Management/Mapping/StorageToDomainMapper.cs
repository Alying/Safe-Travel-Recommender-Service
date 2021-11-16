using System;
using Management.DomainModels;

namespace Management.Mapping
{
    public class StorageToDomainMapper
    {
        public static User ToDomain(Common.StorageModels.User storage)
            => new User(
                userId: UserId.Wrap(storage.UserId),
                fullName: FullName.Wrap(storage.FullName),
                passportId: PassportId.Wrap(storage.PassportId),
                createAt: DateTimeOffset.TryParse(storage.CreatedAt, out var time) ? time : default(DateTimeOffset),
                countryCode: System.Enum.TryParse<Enum.CountryCode>(storage.CountryCode, out var country)
                ? country
                : Enum.CountryCode.Unknown);

        public static Comment ToDomain(Management.StorageModels.Comment comment) => new Comment(
            location: new Location(Country.Wrap(comment.Country), State.Wrap(comment.State)),
            userId: UserId.Wrap(comment.UserId),
            commentStr: comment.CommentStr,
            createdAt: DateTimeOffset.Parse(comment.CreatedAt));
    }
}
