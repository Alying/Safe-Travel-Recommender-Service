using System;

namespace Management.StorageModels
{
    public class Comment
    {
        public string CommentStr { get; set; }

        public string UserId { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string CreatedAt { get; set; }

        public string UniqueId { get; set; }
    }
}
