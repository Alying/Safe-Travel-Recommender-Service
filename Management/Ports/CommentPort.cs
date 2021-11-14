using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Management.Mapping;
using Management.DomainModels;
using Management.Interface;

using ApiComment = Management.ApiModels.Comment;
using DomainComment = Management.DomainModels.Comment;


namespace Management.Ports
{
    public class CommentPort
    {
        private readonly ICommentRepository _commentRepository;

        public CommentPort(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        }

        public async Task<IEnumerable<DomainComment>> GetCommentAsync(UserId userId, Location location)
            => await _commentRepository.GetAllCommentsAsync(userId, location);
        public async Task AddCommentAsync(Location location, ApiComment apiComment)
            => await _commentRepository.AddCommentAsync(ApiToDomainMapper.toDomain(location, apiComment));
    }
}
