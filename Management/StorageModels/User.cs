using System;

namespace Management.StorageModels
{
    /// <summary>
    /// Representation of a user for the safe-travel service.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets user's name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets each user's given a unique user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets user's role.
        /// </summary>
        public string UserRole { get; set; }

        /// <summary>
        /// Gets or sets the time this individual created a user account in the system.
        /// </summary>
        public string CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the country from which this user is from.
        /// </summary>
        public string CountryCode { get; set; }
    }
}
