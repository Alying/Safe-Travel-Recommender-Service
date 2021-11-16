using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.Repository;
using Management.DomainModels;
using Storage.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using Storage;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

using DomainComment = Management.DomainModels.Comment;
using StorageComment = Management.StorageModels.Comment;

namespace Test.Management.Unit
{
    public class CommentRepositoryTests
    {
        private Mock<IRepository> _mockRepository;
        private CommentRepository _commentRepository;
        private readonly ITestOutputHelper _output;

        public CommentRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            _mockRepository = new Mock<IRepository>();
            _commentRepository = new CommentRepository(_mockRepository.Object);
        }

        [Fact]
        public async Task GetAllCommentsAsync_Success()
        {
            var colVals = new Dictionary<string, string>()
            {
                { "userId", "newUser" },
                { "country", "US" },
                { "state", "NY" }
            };
            _mockRepository.Setup(t => t.GetSomeAsync<StorageComment>(It.Is<string>(arg => arg == "comment"), It.Is<IReadOnlyDictionary<string, string>>(arg => IsDictEqual(arg, colVals)))).ReturnsAsync(new List<StorageComment>() {
                new StorageComment() { 
                    CommentStr = "Hello World!",
                    UserId = "newUser",
                    Country = "US",
                    State = "NY",
                    CreatedAt = "11/14/2021 7:47:41 PM +00:00",
                    UniqueId = "somerandomuniquestuff"
                 }
                 });
            var comments = await _commentRepository.GetAllCommentsAsync(UserId.Wrap("newUser"), new Location(Country.Wrap("US"), State.Wrap("NY")));
            Assert.Equal(comments.Count(), 1);
            var retrievedComment = comments.First();
            Assert.Equal(retrievedComment.CommentStr, "Hello World!");
            Assert.Equal(retrievedComment.UserId.Value, "newUser");
            Assert.Equal(retrievedComment.Location.Country.Value, "US");
            Assert.Equal(retrievedComment.Location.State.Value, "NY");
            Assert.Equal(retrievedComment.CreatedAt.ToString(), "11/14/2021 7:47:41 PM +00:00");

            _mockRepository.VerifyAll();
        }

        [Fact]
        public async Task AddCommentAsync_Success()
        {
            var testComment = new DomainComment(new Location(Country.Wrap("US"), State.Wrap("NY")), UserId.Wrap("newUser"), "Hello World!", DateTimeOffset.Parse("11/14/2021 7:47:41 PM +00:00"));
            var fields = new List<List<string>>()
            {
                new List<string>() {
                    testComment.UserId.Value,
                    testComment.Location.Country.Value,
                    testComment.Location.State.Value,
                    testComment.CreatedAt.ToString(),
                    testComment.CommentStr
                }
            };
            _mockRepository.Setup(t => t.InsertAsync<StorageComment>(It.Is<string>(arg => arg == "comment"), It.Is<IEnumerable<IReadOnlyList<string>>>(arg => IsListContainElement(arg, fields))));

            await _commentRepository.AddCommentAsync(testComment);

            Assert.Equal(1, 1);

            _mockRepository.VerifyAll();
        }

        private bool IsListContainElement(IEnumerable<IReadOnlyList<string>> lhs, IEnumerable<IReadOnlyList<string>> rhs)
        {
            if(lhs.Count() != rhs.Count() || lhs.Count() != 1)
            {
                return false;
            }
            var lhsFirst = lhs.First();
            var rhsFirst = rhs.First();
            int count = 0;
            foreach(var rhsItem in rhsFirst)
            {
                foreach(var lhsItem in lhsFirst)
                {
                    if(rhsItem == lhsItem)
                    {
                        count ++;
                        break;
                    }
                }
            }
            return count == rhsFirst.Count();
        }

        private bool IsDictEqual(IReadOnlyDictionary<string, string> lhs, IReadOnlyDictionary<string, string> rhs)
        {
            if(lhs.Count != rhs.Count)
            {
                return false;
            }
            foreach(var key in lhs.Keys)
            {
                if(!rhs.ContainsKey(key) || (lhs[key] != rhs[key]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
