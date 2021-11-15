namespace Management.ApiModels
{
    public class Comment
    {
        // TODO: @mli: Remove UserId here when we can get userId from auth token.
        public string UserIdStr { get; set; }

        public string CommentStr { get; set; }
    }
}
