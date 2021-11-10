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
        public Comment(Location location, UserId userId, string commentStr)
        {
            Location = location;
            UserId = userId;
            CommentStr = commentStr;
            CreatedAt = DateTime.UtcNow;
        }
        public Comment(Location location, UserId userId, string commentStr, DateTimeOffset createdAt)
        {
            Location = location;
            UserId = userId;
            CommentStr = commentStr;
            CreatedAt = createdAt;
        }
        
    }
}
