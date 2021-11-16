namespace Common.StorageModels
{
    /// <summary>
    /// A representation of an individual who is using this safe-travel service.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets each user is given a unique user id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the full name of this user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the country from which this user is from.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the time this individual created a user account in the system.
        /// </summary>
        public string CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the information about this user's passport 
        /// (for visa purposes -- may not be fully implemented).
        /// </summary>
        public string PassportId { get; set; }
    }
}
