using System.Collections.Generic;
using System.Threading.Tasks;
using Management.DomainModels;
using DomainComment = Management.DomainModels.Comment;

namespace Management.Interface
{
    public interface ICommentRepository
    {
        Task<IEnumerable<DomainComment>> GetAllCommentsAsync(UserId userId, Location location);

        Task AddCommentAsync(DomainComment newComment);
    }
}
