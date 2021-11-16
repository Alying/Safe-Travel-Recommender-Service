namespace Management.ApiModels
{
    /// <summary>
    /// A representation of a comment in the safe-travel service.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets the unique ID of the user who created this comment.
        /// </summary>
        // TODO: @mli: Remove UserId here when we can get userId from auth token.
        public string UserIdStr { get; set; }

        /// <summary>
        /// Gets or sets the actual comment that the user left in the system.
        /// </summary>
        public string CommentStr { get; set; }
    }
}
