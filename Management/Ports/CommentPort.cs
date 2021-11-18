using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Interface;
using Management.Mapping;
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

        public Task<IEnumerable<DomainComment>> GetCommentAsync(string userId, string countryCode, string state)
            => _commentRepository.GetAllCommentsAsync(UserId.Wrap(userId), ConstructLocation(countryCode, state));

        public Task AddCommentAsync(string countryCode, string state, ApiComment apiComment)
            => _commentRepository.AddCommentAsync(ApiToDomainMapper.ToDomain(ConstructLocation(countryCode, state), apiComment));

        private Location ConstructLocation(string countryCode, string state)
        {
            var validatedResult = CountryStateValidator.ValidateCountryState(countryCode, state);

            return new Location(validatedResult.validatedCountry, validatedResult.validatedState);
        }
    }
}
