using Management.DomainModels;
using Management.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management
{
    public class UserPort
    {
        private readonly IUserRepository _userRepository;

        public UserPort(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<IEnumerable<ApiModels.User>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return users.Select(Mapping.DomainToApiMapper.ToApi);
        }

        public Task InsertManyUsers(IEnumerable<ApiModels.User> users)
            => _userRepository.AddUsersAsync(users.Select(Mapping.ApiToDomainMapper.ToDomain));

        public async Task<ApiModels.User> GetUser(string userId)
        {
            var maybeUser = await _userRepository.GetUserAsync(UserId.Wrap(userId));

            return maybeUser.Match(
                some => Mapping.DomainToApiMapper.ToApi(some),
                () => throw new Exception("UserNotFound"));
        }

        public async Task UpdateUserAsync(ApiModels.User updatedUser)
        {
            var domainUser = Mapping.ApiToDomainMapper.ToDomain(updatedUser);

            var result = await _userRepository.UpdateUserAsync(domainUser.UserId, domainUser);

            result.MatchNone(() => throw new Exception("Failed to update."));
        }

        public async Task DeleteUserAsync(string userId)
        {
            var result = await _userRepository.RemoveUserAsync(UserId.Wrap(userId));

            result.MatchNone(() => throw new Exception("Failed to update."));
        }
    }
}
