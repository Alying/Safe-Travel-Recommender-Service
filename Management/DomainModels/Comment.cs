using System;

namespace Management.DomainModels
{
    /// <summary>
    /// Representation of a comment in the safe-travel service.
    /// </summary>
    public class Comment
    {
        public Location Location { get; }

        public UserId UserId { get; }

        public string CommentStr { get; }

        public DateTimeOffset CreatedAt { get; }

        public Comment(Location location, UserId userId, string commentStr, DateTimeOffset? createdAt = null)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            CommentStr = commentStr ?? throw new ArgumentNullException(nameof(commentStr));
            CreatedAt = createdAt ?? DateTime.UtcNow;
        }
    }
}
