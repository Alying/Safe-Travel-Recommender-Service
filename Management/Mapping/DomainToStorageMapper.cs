using Management.DomainModels;

namespace Management.Mapping
{
    /// <summary>
    /// Mapper class that maps domain user and comment to storage user and comment
   /// </summary>
    public class DomainToStorageMapper
    {
        /// <summary>
        /// Maps domain user to storage user
        /// </summary>
        /// <param name="domain">domain user.</param>
        /// <returns>storage user.</returns>
        public static Common.StorageModels.User ToStorage(User domain)
            => new Common.StorageModels.User
            {
                FullName = domain.FullName.Value,
                UserId = domain.UserId.Value,
                CountryCode = domain.CountryCode.ToString(),
                CreatedAt = domain.CreatedAt.ToString(),
                PassportId = domain.PassportId.Value,
            };

        /// <summary>
        /// Maps domain comment to storage comment
        /// </summary>
        /// <param name="domain">domain comment.</param>
        /// <returns>storage comment.</returns>
        public static StorageModels.Comment ToStorage(Comment domain) => new StorageModels.Comment
        {
            CommentStr = domain.CommentStr,
            UserId = domain.UserId.Value,
            Country = domain.Location.CountryCode.ToString(),
            State = domain.Location.State.Value,
            CreatedAt = domain.CreatedAt.ToString(),
            UniqueId = System.Guid.NewGuid().ToString(),
        };
    }
}
