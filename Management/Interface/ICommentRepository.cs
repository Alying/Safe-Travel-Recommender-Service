using System.Collections.Generic;
using System.Threading.Tasks;
using Management.DomainModels;
using DomainComment = Management.DomainModels.Comment;

namespace Management.Interface
{
    /// <summary>
    /// Interface for comment repository functions
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Gets all comments stored in database for this location.
        /// </summary>
        /// <param name="location">location the comment is for.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a list of the comments. </returns>
        Task<IEnumerable<DomainComment>> GetAllCommentsAsync(Location location);

        /// <summary>
        /// Posts a comment to the database.
        /// </summary>
        /// <param name="newComment">comment.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddCommentAsync(DomainComment newComment);
    }
}
