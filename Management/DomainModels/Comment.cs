using System;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of a comment in the safe-travel service.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets the location to make comment on
        /// </summary>
        public Location Location { get; }

        /// <summary>
        /// Gets the user's unique id
        /// </summary>
        public UserId UserId { get; }

        /// <summary>
        /// Gets the location to make comment on
        /// </summary>
        public string CommentStr { get; }

        /// <summary>
        /// Gets the timestamp of the comment
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="location">the location that the comment was made on</param>
        /// <param name="userId">unique user id</param>
        /// <param name="commentStr">comment string</param>
        /// <param name="createdAt">timestamp of the comment</param>
        public Comment(Location location, UserId userId, string commentStr, DateTimeOffset? createdAt = null)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            CommentStr = commentStr ?? throw new ArgumentNullException(nameof(commentStr));
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }
    }
}
