using System;
using Management.Enum;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of a user for the safe-travel service.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets user id
        /// </summary>
        public UserId UserId { get; }

        /// <summary>
        /// Gets user's full name
        /// </summary>
        public FullName FullName { get; }

        /// <summary>
        /// Gets user's passport id
        /// </summary>
        public PassportId PassportId { get; }

        /// <summary>
        /// Gets when the user account was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Gets country code eg. "US"
        /// </summary>
        public CountryCode CountryCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userId">user's unique id.</param>
        /// <param name="fullName">user's full name.</param>
        /// <param name="passportId">user's passport id.</param>
        /// <param name="createAt">when the user account was created.</param>
        /// <param name="countryCode">country code eg. "US".</param>
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
