using Management.DomainModels;
using Optional;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DomainUser = Management.DomainModels.User;

namespace Management.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<DomainUser>> GetAllUsersAsync();

        Task<Option<DomainUser>> GetUserAsync(UserId userId);

        Task<IEnumerable<DomainUser>> AddUsersAsync(IEnumerable<DomainUser> newUsers);

        Task<Option<DomainUser>> UpdateUserAsync(UserId userId, DomainUser user);

        Task<Option<DomainUser>> RemoveUserAsync(UserId userId);
    }
}
