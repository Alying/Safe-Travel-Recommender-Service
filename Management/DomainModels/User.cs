using System;
using Management.Enum;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of a user for the safe-travel service.
    /// </summary>
    public class User
    {
        public UserId UserId { get; }

        public FullName FullName { get; }

        public PassportId PassportId { get; }

        public DateTimeOffset CreatedAt { get; }

        public CountryCode CountryCode { get; }

        public User(
        UserId userId,
        FullName fullName,
        PassportId passportId,
        DateTimeOffset createAt,
        CountryCode countryCode)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            PassportId = passportId;
            CreatedAt = createAt;
            CountryCode = countryCode;
        }
    }
}
