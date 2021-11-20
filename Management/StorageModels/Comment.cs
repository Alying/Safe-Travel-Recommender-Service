using System;

namespace Management.StorageModels
{
    /// <summary>
    /// Representation of a comment in the safe-travel service.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets the actual comment that the user left in the system.
        /// </summary>
        public string CommentStr { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the user who created this comment.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the country to make comment on
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state to make comment on
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the comment
        /// </summary>
        public string CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the user's unique id
        /// </summary>
        public string UniqueId { get; set; }
    }
}
