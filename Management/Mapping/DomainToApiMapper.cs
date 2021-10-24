using ApiUser = Management.ApiModels.User;
using DomainUser = Management.DomainModels.User;

namespace Management.Mapping
{
    public class DomainToApiMapper
    {
        public static ApiUser ToApi(DomainUser domain)
            => new ApiUser
            {
                UserId = domain.UserId.Value,
                FullName = domain.FullName.Value,
                PassportId = domain.PassportId.ToString(),
                CreatedAt = domain.CreatedAt,
                CountryCode = domain.CountryCode.ToString(),
            };
    }
}
