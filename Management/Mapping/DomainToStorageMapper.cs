using Management.DomainModels;

namespace Management.Mapping
{
    public class DomainToStorageMapper
    {
        public static Common.StorageModels.User ToStorage(User domain)
            => new Common.StorageModels.User
            {
                FullName = domain.FullName.Value,
                UserId = domain.UserId.Value,
                CountryCode = domain.CountryCode.ToString(),
                CreatedAt = domain.CreatedAt.ToString(),
                PassportId = domain.PassportId.Value,
            };

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
