using System.Collections.Generic;
using System.Threading.Tasks;
using Management.DomainModels;
using Optional;
using DomainUser = Management.DomainModels.User;

namespace Management.Interface
{
    /// <summary>
    /// Interface for user repository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with a list of the users.</returns>
        Task<IEnumerable<DomainUser>> GetAllUsersAsync();

        /// <summary>
        /// Gets a user from the database using unique user id.
        /// </summary>
        /// <param name="userId">unique user id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        Task<Option<DomainUser>> GetUserAsync(UserId userId);

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation with the User.</returns>
        /// <param name="newUsers">new user.</param>
        Task<IEnumerable<DomainUser>> AddUsersAsync(IEnumerable<DomainUser> newUsers);

        /// <summary>
        /// Update user info in the database.
        /// </summary>
        /// <param name="userId">unique user id.</param>
        /// <param name="user">user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        Task<Option<DomainUser>> UpdateUserAsync(UserId userId, DomainUser user);

        /// <summary>
        /// Remove user from the database.
        /// </summary>
        /// <param name="userId">unique user id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        Task<Option<DomainUser>> RemoveUserAsync(UserId userId);
    }
}
