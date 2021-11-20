using ApiUser = Management.ApiModels.User;
using DomainUser = Management.DomainModels.User;

namespace Management.Mapping
{
    /// <summary>
    /// Mapper class that maps domain user to api user
    /// </summary>
    public class DomainToApiMapper
    {
        /// <summary>
        /// Maps domain user to api user
        /// </summary>
        /// <param name="domain">domain user.</param>
        /// <returns>api user.</returns>
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
