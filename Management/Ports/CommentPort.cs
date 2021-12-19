using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Interface;
using Management.Mapping;
using ApiComment = Management.ApiModels.Comment;
using DomainComment = Management.DomainModels.Comment;

namespace Management.Ports
{
    /// <summary>
    /// Port that calls functions that handle comments (get and post)
    /// </summary>
    public class CommentPort
    {
        private readonly ICommentRepository _commentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentPort"/> class.
        /// </summary>
        /// <param name="commentRepository">repository for comments.</param>
        public CommentPort(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        /// <summary>
        /// Gets all comments stored in database for this location.
        /// </summary>
        /// <param name="userId">user who wrote the comment.</param>
        /// <param name="countryCode">country code eg. US.</param>
        /// <param name="state">state code eg. NY.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a list of the comments. </returns>
        public Task<IEnumerable<DomainComment>> GetCommentAsync(string userId, string countryCode, string state)
            => _commentRepository.GetAllCommentsAsync(ConstructLocation(countryCode, state));

        /// <summary>
        /// Posts a comment to the database.
        /// </summary>
        /// <param name="countryCode">country code eg. US.</param>
        /// <param name="state">state code eg. NY.</param>
        /// <param name="apiComment">comment.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task AddCommentAsync(string countryCode, string state, ApiComment apiComment)
            => _commentRepository.AddCommentAsync(ApiToDomainMapper.ToDomain(ConstructLocation(countryCode, state), apiComment));

        private static Location ConstructLocation(string countryCode, string state)
        {
            var validatedResult = CountryStateValidator.ValidateCountryState(countryCode, state);

            return new Location(validatedResult.validatedCountry, validatedResult.validatedState);
        }
    }
}
