using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Management.DomainModels
{
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
