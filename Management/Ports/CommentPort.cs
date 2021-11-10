using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Management.Mapping;
using Management.DomainModels;
using Management.Interface;


namespace Management.Ports
{
    public class CommentPort
    {
        private readonly ICommentRepository _commentRepository;

        public CommentPort(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        public async Task<IEnumerable<Comment>> GetCommentAsync(UserId userId, Location location) => await _commentRepository.GetAllCommentsAsync(userId, location);
        public async Task AddCommentAsync(UserId userId, Location location, string commentStr) => await _commentRepository.AddCommentAsync(new Comment(location, userId, commentStr));
    }
}
