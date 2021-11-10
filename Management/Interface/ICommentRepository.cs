using Management.DomainModels;
using Optional;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DomainComment = Management.DomainModels.Comment;

namespace Management.Interface
{
    public interface ICommentRepository
    {
        Task<IEnumerable<DomainComment>> GetAllCommentsAsync(UserId userId, Location location);

        Task AddCommentAsync(DomainComment newComment);
    }
}
