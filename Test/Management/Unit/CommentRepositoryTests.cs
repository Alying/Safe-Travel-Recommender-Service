using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management.DomainModels;
using Management.Enum;
using Management.Repository;
using Moq;
using Storage.Interface;
using Xunit;
using Xunit.Abstractions;
using DomainComment = Management.DomainModels.Comment;
using StorageComment = Management.StorageModels.Comment;

namespace Test.Management.Unit
{
    /// <summary>
    /// Unit test for comment repository
    /// </summary>
    public class CommentRepositoryTests
    {
        private Mock<IRepository> _mockRepository;
        private CommentRepository _commentRepository;
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentRepositoryTests"/> class.
        /// </summary>
        /// <param name="output">test output helper.</param>
        public CommentRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            _mockRepository = new Mock<IRepository>();
            _commentRepository = new CommentRepository(_mockRepository.Object);
        }

        /// <summary>
        /// Test to see if successfully get all comments
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [Fact]
        public async Task GetAllCommentsAsync_Success()
        {
            var colVals = new Dictionary<string, string>()
            {
                { "userId", "newUser" },
                { "country", "US" },
                { "state", "CA" },
            };
            _mockRepository.Setup(t => t.GetSomeAsync<StorageComment>(It.Is<string>(arg => arg == "comment"), It.Is<IReadOnlyDictionary<string, string>>(arg => IsDictEqual(arg, colVals)))).ReturnsAsync(new List<StorageComment>()
            {
                new StorageComment()
                {
                    CommentStr = "Hello World!",
                    UserId = "newUser",
                    Country = "US",
                    State = "CA",
                    CreatedAt = "11/14/2021 7:47:41 PM +00:00",
                    UniqueId = "somerandomuniquestuff",
                },
            });
            var comments = await _commentRepository.GetAllCommentsAsync(UserId.Wrap("newUser"), new Location(CountryCode.US, State.Wrap("CA")));

            var retrievedComment = Assert.Single(comments);
            Assert.Equal("Hello World!", retrievedComment.CommentStr);
            Assert.Equal("newUser", retrievedComment.UserId.Value);
            Assert.Equal("US", retrievedComment.Location.CountryCode.ToString());
            Assert.Equal("CA", retrievedComment.Location.State.Value);

            _mockRepository.VerifyAll();
        }

        /// <summary>
        /// Test to see if successfully post comment
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation, with a status code.</returns>
        [Fact]
        public async Task AddCommentAsync_Success()
        {
            var testComment = new DomainComment(
                                  new Location(CountryCode.US, State.Wrap("NY")),
                                  UserId.Wrap("newUser"),
                                  "Hello World!",
                                  DateTimeOffset.Parse("11/14/2021 7:47:41 PM +00:00"));

            var fields = new List<List<string>>()
            {
                new List<string>()
                {
                    testComment.UserId.Value,
                    testComment.Location.CountryCode.ToString(),
                    testComment.Location.State.Value,
                    testComment.CreatedAt.ToString(),
                    testComment.CommentStr,
                },
            };
            _mockRepository.Setup(t => t.InsertAsync<StorageComment>(It.Is<string>(arg => arg == "comment"), It.Is<IEnumerable<IReadOnlyList<string>>>(arg => IsListContainElement(arg, fields))));

            await _commentRepository.AddCommentAsync(testComment);

            Assert.Equal(1, 1);

            _mockRepository.VerifyAll();
        }

        private bool IsListContainElement(IEnumerable<IReadOnlyList<string>> lhs, IEnumerable<IReadOnlyList<string>> rhs)
        {
            if (lhs.Count() != rhs.Count() || lhs.Count() != 1)
            {
                return false;
            }

            var lhsFirst = lhs.First();
            var rhsFirst = rhs.First();
            int count = 0;
            foreach (var rhsItem in rhsFirst)
            {
                foreach (var lhsItem in lhsFirst)
                {
                    if (rhsItem == lhsItem)
                    {
                        count++;
                        break;
                    }
                }
            }

            return count == rhsFirst.Count();
        }

        private bool IsDictEqual(IReadOnlyDictionary<string, string> lhs, IReadOnlyDictionary<string, string> rhs)
        {
            if (lhs.Count != rhs.Count)
            {
                return false;
            }

            foreach (var key in lhs.Keys)
            {
                if (!rhs.ContainsKey(key) || (lhs[key] != rhs[key]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
