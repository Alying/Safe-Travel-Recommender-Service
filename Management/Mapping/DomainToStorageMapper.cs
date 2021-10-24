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
    }
}
