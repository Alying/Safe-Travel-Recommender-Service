using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Interface;
using Optional;
using Storage.Interface;
using StorageUser = Common.StorageModels.User;

namespace Management.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string _tableName = "user";

        private const string _keyColumnName = "UserId";

        private readonly IRepository _repository;

        public UserRepository(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        public async Task<IEnumerable<User>> AddUsersAsync(IEnumerable<User> newUsers)
        {
            try
            {
                var userToInsert = newUsers.Select(user
                => new List<string>
                {
                    user.FullName.Value,
                    user.UserId.Value,
                    user.PassportId.Value,
                    user.CreatedAt.ToString(),
                    user.CountryCode.ToString(),
                 });

                await _repository.InsertAsync<StorageUser>(_tableName, userToInsert);
                return newUsers;
            }
            catch (Exception)
            {
                return Enumerable.Empty<User>();
            }
        }

        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync<StorageUser>(_tableName);

                return result.Select(user => Mapping.StorageToDomainMapper.ToDomain(user));
            }
            catch (Exception)
            {
                return Enumerable.Empty<User>();
            }
        }

        public async Task<Option<User>> GetUserAsync(UserId userId)
        {
            try
            {
                var result = await _repository.GetAsync<StorageUser>(_tableName, _keyColumnName, userId.Value);
                return Option.Some(Mapping.StorageToDomainMapper.ToDomain(result));
            }
            catch (Exception)
            {
                return Option.None<User>();
            }
        }

        public async Task<Option<User>> RemoveUserAsync(UserId userId)
        {
            try
            {
                var userToRemove = await _repository.GetAsync<StorageUser>(_tableName, _keyColumnName, userId.Value);
                await _repository.DeleteAsync<StorageUser>(_tableName, _keyColumnName, userId.Value);

                return Option.Some(Mapping.StorageToDomainMapper.ToDomain(userToRemove));
            }
            catch (Exception)
            {
                return Option.None<User>();
            }
        }

        public async Task<Option<User>> UpdateUserAsync(UserId userId, User user)
        {
            try
            {
                var updateDictionary = new Dictionary<string, string>
                {
                    { "FullName", user.FullName.Value},
                    { "PassportId", user.PassportId.Value},
                    { "CountryCode", user.CountryCode.ToString()},
                };

                await _repository.UpdateAsync(_tableName, _keyColumnName, userId.Value, updateDictionary);
                return Option.Some(user);
            }
            catch (Exception)
            {
                return Option.None<User>();
            }
        }
    }
}
