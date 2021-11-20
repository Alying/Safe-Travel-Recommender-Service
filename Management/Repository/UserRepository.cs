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
    /// <summary>
    /// Handles user related operations in service's database
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private const string _tableName = "user";

        private const string _keyColumnName = "UserId";

        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="repository">repository for user.</param>
        public UserRepository(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with the User.</returns>
        /// <param name="newUsers">new user.</param>
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
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a list of the users.</returns>
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

        /// <summary>
        /// Gets a user from the database using unique user id.
        /// </summary>
        /// <param name="userId">unique user id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
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

        /// <summary>
        /// Remove user from the database.
        /// </summary>
        /// <param name="userId">unique user id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
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

        /// <summary>
        /// Update user info in the database.
        /// </summary>
        /// <param name="userId">unique user id.</param>
        /// <param name="user">user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        public async Task<Option<User>> UpdateUserAsync(UserId userId, User user)
        {
            try
            {
                var updateDictionary = new Dictionary<string, string>
                {
                    { "FullName", user.FullName.Value },
                    { "PassportId", user.PassportId.Value },
                    { "CountryCode", user.CountryCode.ToString() },
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
