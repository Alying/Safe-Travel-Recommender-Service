using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Interface;
using Storage.Interface;
using DomainComment = Management.DomainModels.Comment;
using StorageComment = Management.StorageModels.Comment;

namespace Management.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private const string _tableName = "comment";

        private const string _keyColumnName = "uniqueId";

        private readonly IRepository _repository;

        public CommentRepository(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Gets all comments stored in database for this location.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a list of the comments. </returns>
        /// <param name="userId">user who wrote the comment.</param>
        /// <param name="location">location the comment is for.</param>
        public async Task<IEnumerable<DomainComment>> GetAllCommentsAsync(UserId userId, Location location)
        {
            var result = await _repository.GetSomeAsync<StorageComment>(_tableName, new Dictionary<string, string>()
            {
                { "userId", userId.Value },
                { "country", location.Country.Value },
                { "state", location.State.Value },
            });
            return result.Select(Mapping.StorageToDomainMapper.ToDomain);
        }

        /// <summary>
        /// Posts a comment to the database.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <param name="comment">comment.</param>
        public async Task AddCommentAsync(DomainComment comment)
        {
            var storageComment = Mapping.DomainToStorageMapper.ToStorage(comment);

            // uniqueId, userId, country, state, createdAt, commentStr.
            var fields = new List<string>() { storageComment.UniqueId, storageComment.UserId, storageComment.Country, storageComment.State, storageComment.CreatedAt, storageComment.CommentStr };
            await _repository.InsertAsync<StorageComment>(_tableName, new List<List<string>>() { fields });
        }
    }
}
