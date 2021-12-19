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
using Optional;
using DomainUser = Management.DomainModels.User;
using StorageUser = Common.StorageModels.User;

namespace Test.Management.Unit
{
    public class UserRepositoryTests
    {
        private Mock<IRepository> _mockRepository;
        private UserRepository _userRepository;
        private readonly DomainUser _dummyUser;

        private static bool IsDictEqual(IReadOnlyDictionary<string, string> lhs, IReadOnlyDictionary<string, string> rhs)
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

        private static bool IsListsEqual(IEnumerable<IReadOnlyList<string>> lhs, IEnumerable<IReadOnlyList<string>> rhs)
        {
            if (lhs.Count() != rhs.Count())
            {
                return false;
            }

            foreach (var lList in lhs)
            {
                bool foundMatch = false;
                
                foreach (var rList in rhs)
                {
                    if (lList.All(rList.Contains))
                    {
                        foundMatch = true;
                        break;
                    }
                }
                
                if (!foundMatch)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepositoryTests"/> class.
        /// </summary>
        /// <param name="output">test output helper.</param>
        public UserRepositoryTests(ITestOutputHelper output)
        {
            _mockRepository = new Mock<IRepository>();
            _userRepository = new UserRepository(_mockRepository.Object);
            _dummyUser = new DomainUser(UserId.Wrap(" "), FullName.Wrap(" "), PassportId.Wrap(" "), DateTimeOffset.Now, CountryCode.Unknown);
        }

        [Fact]
        public async Task AddUsersAsync_Success()
        {
            var newUser = new DomainUser(UserId.Wrap("TestId_1"), FullName.Wrap("Test User Name"), PassportId.Wrap("US-34567"), DateTimeOffset.Now, CountryCode.CA);

            IEnumerable<DomainUser> expectedNewUsers = new List<DomainUser>
            {
                newUser,
            };

            var expectedNewUsersList = new List<List<string>>
            {
                new List<string>
                {
                    newUser.FullName.Value,
                    newUser.UserId.Value,
                    newUser.PassportId.Value,
                    newUser.CreatedAt.ToString(),
                    newUser.CountryCode.ToString(),
                }, 
            };

            _mockRepository.Setup(t => t.InsertAsync<StorageUser>(It.Is<string>(arg => arg == "user"), It.Is<IEnumerable<IReadOnlyList<string>>>(arg => IsListsEqual(arg, expectedNewUsersList)))).Returns(Task.CompletedTask);

            var newUsersListReturned = await _userRepository.AddUsersAsync(expectedNewUsers);

            var newUserReturned = Assert.Single(newUsersListReturned);
            Assert.Equal("TestId_1", newUserReturned.UserId.Value);
            Assert.Equal("Test User Name", newUserReturned.FullName.Value);
            Assert.Equal(CountryCode.CA, newUserReturned.CountryCode);
            Assert.Equal("US-34567", newUserReturned.PassportId.Value);
        }

        [Fact]
        public async Task AddUsersAsync_Fail()
        {
            var newUser = new DomainUser(UserId.Wrap("TestId_1"), FullName.Wrap("Test User Name"), PassportId.Wrap("US-34567"), DateTimeOffset.Now, CountryCode.CA);

            IEnumerable<DomainUser> expectedNewUsers = new List<DomainUser>
            {
                newUser,
            };

            var expectedNewUsersList = new List<List<string>>
            {
                new List<string>
                {
                    newUser.FullName.Value,
                    newUser.UserId.Value,
                    newUser.PassportId.Value,
                    newUser.CreatedAt.ToString(),
                    newUser.CountryCode.ToString(),
                },
            };

            _mockRepository.Setup(t => t.InsertAsync<StorageUser>(It.Is<string>(arg => arg == "user"), It.Is<IEnumerable<IReadOnlyList<string>>>(arg => IsListsEqual(arg, expectedNewUsersList)))).Throws(new Exception());

            var newUsersListReturned = await _userRepository.AddUsersAsync(expectedNewUsers);

            Assert.Empty(newUsersListReturned);
        }

        [Fact]
        public async Task GetAllUsersAsync_Success()
        {
            _mockRepository.Setup(t => t.GetAllAsync<StorageUser>(It.Is<string>(arg => arg == "user"))).ReturnsAsync(new List<StorageUser>()
            {
                new StorageUser()
                {
                    UserId = "TestId",
                    FullName = "Some User",
                    CountryCode = "US",
                    CreatedAt = "11/14/2021 7:47:41 PM +00:00",
                    PassportId = "US-12345",
                },
            });
            
            var users = await _userRepository.GetAllUsersAsync();

            var retrievedUser = Assert.Single(users);
            Assert.Equal("TestId", retrievedUser.UserId.Value);
            Assert.Equal("Some User", retrievedUser.FullName.Value);
            Assert.Equal(CountryCode.US, retrievedUser.CountryCode);
            Assert.Equal("11/14/2021 7:47:41 PM +00:00", retrievedUser.CreatedAt.ToString());
            Assert.Equal("US-12345", retrievedUser.PassportId.Value);
        }

        [Fact]
        public async Task GetAllUsersAsync_Fail()
        {
            _mockRepository.Setup(t => t.GetAllAsync<StorageUser>(It.Is<string>(arg => arg == "user"))).Throws(new Exception());
            
            var users = await _userRepository.GetAllUsersAsync();

            Assert.Empty(users);
        }

        [Fact]
        public async Task GetUserAsync_Success()
        {
            _mockRepository.Setup(t => t.GetAsync<StorageUser>(It.Is<string>(arg => arg == "user"), It.Is<string>(arg => arg == "UserId"), It.Is<string>(arg => arg == "TestId"))).ReturnsAsync(new StorageUser()
            {
                    UserId = "TestId",
                    FullName = "Some User",
                    CountryCode = "US",
                    CreatedAt = "11/14/2021 7:47:41 PM +00:00",
                    PassportId = "US-12345",
            });

            var user = await _userRepository.GetUserAsync(UserId.Wrap("TestId"));

            Assert.True(user.HasValue);

            var actualUser = user.ValueOr(_dummyUser);
            Assert.Equal("TestId", actualUser.UserId.Value);
            Assert.Equal("Some User", actualUser.FullName.Value);
            Assert.Equal(CountryCode.US, actualUser.CountryCode);
            Assert.Equal("11/14/2021 7:47:41 PM +00:00", actualUser.CreatedAt.ToString());
            Assert.Equal("US-12345", actualUser.PassportId.Value);
        }

        [Fact]
        public async Task GetUserAsync_Fail()
        {
            _mockRepository.Setup(t => t.GetAsync<StorageUser>(It.Is<string>(arg => arg == "user"), It.Is<string>(arg => arg == "UserId"), It.Is<string>(arg => arg == "TestId"))).Throws(new Exception());

            var user = await _userRepository.GetUserAsync(UserId.Wrap("TestId"));

            Assert.False(user.HasValue);
        }

        [Fact]
        public async Task RemoveUserAsync_Success()
        {
            _mockRepository.Setup(t => t.GetAsync<StorageUser>(It.Is<string>(arg => arg == "user"), It.Is<string>(arg => arg == "UserId"), It.Is<string>(arg => arg == "TestId"))).ReturnsAsync(new StorageUser()
            {
                UserId = "TestId",
                FullName = "Some User",
                CountryCode = "US",
                CreatedAt = "11/14/2021 7:47:41 PM +00:00",
                PassportId = "US-12345",
            });

            _mockRepository.Setup(t => t.DeleteAsync<StorageUser>(It.Is<string>(arg => arg == "user"), It.Is<string>(arg => arg == "UserId"), It.Is<string>(arg => arg == "TestId"))).Returns(Task.CompletedTask);

            var user = await _userRepository.RemoveUserAsync(UserId.Wrap("TestId"));

            Assert.True(user.HasValue);

            var actualUser = user.ValueOr(_dummyUser);
            Assert.Equal("TestId", actualUser.UserId.Value);
            Assert.Equal("Some User", actualUser.FullName.Value);
            Assert.Equal(CountryCode.US, actualUser.CountryCode);
            Assert.Equal("11/14/2021 7:47:41 PM +00:00", actualUser.CreatedAt.ToString());
            Assert.Equal("US-12345", actualUser.PassportId.Value);
        }

        [Fact]
        public async Task RemoveUserAsync_GetFail()
        {
            _mockRepository.Setup(t => t.GetAsync<StorageUser>(It.Is<string>(arg => arg == "user"), It.Is<string>(arg => arg == "UserId"), It.Is<string>(arg => arg == "TestId"))).Throws(new Exception());

            var user = await _userRepository.RemoveUserAsync(UserId.Wrap("TestId"));

            Assert.False(user.HasValue);
        }

        [Fact]
        public async Task RemoveUserAsync_RemoveFail()
        {
            _mockRepository.Setup(t => t.DeleteAsync<StorageUser>(It.Is<string>(arg => arg == "user"), It.Is<string>(arg => arg == "UserId"), It.Is<string>(arg => arg == "TestId"))).Throws(new Exception());

            var user = await _userRepository.RemoveUserAsync(UserId.Wrap("TestId"));

            Assert.False(user.HasValue);
        }

        [Fact]
        public async Task UpdateUserAsync_Success()
        {
            var updatedUser = new DomainUser(UserId.Wrap("TestId"), FullName.Wrap("New Some User"), PassportId.Wrap("US-23456"), DateTimeOffset.Now, CountryCode.CA);
            var expectedUpdatedUserDict = new Dictionary<string, string>
            {
                { "FullName", updatedUser.FullName.Value },
                { "PassportId", updatedUser.PassportId.Value },
                { "CountryCode", updatedUser.CountryCode.ToString() },
            };

            _mockRepository.Setup(t => t.UpdateAsync(It.Is<string>(arg => arg == "user"), It.Is<string>(arg => arg == "UserId"), It.Is<string>(arg => arg == "TestId"), It.Is<IReadOnlyDictionary<string, string>>(arg => IsDictEqual(arg, expectedUpdatedUserDict)))).Returns(Task.CompletedTask);

            var user = await _userRepository.UpdateUserAsync(UserId.Wrap("TestId"), updatedUser);

            Assert.True(user.HasValue);

            var actualUser = user.ValueOr(_dummyUser);
            Assert.Equal("TestId", actualUser.UserId.Value);
            Assert.Equal(updatedUser.FullName.Value, actualUser.FullName.Value);
            Assert.Equal(updatedUser.CountryCode, actualUser.CountryCode);
            Assert.Equal(updatedUser.PassportId.Value, actualUser.PassportId.Value);
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateFail()
        {
            var updatedUser = new DomainUser(UserId.Wrap("TestId"), FullName.Wrap("New Some User"), PassportId.Wrap("US-23456"), DateTimeOffset.Now, CountryCode.CA);
            var expectedUpdatedUserDict = new Dictionary<string, string>
            {
                { "FullName", updatedUser.FullName.Value },
                { "PassportId", updatedUser.PassportId.Value },
                { "CountryCode", updatedUser.CountryCode.ToString() },
            };

            _mockRepository.Setup(t => t.UpdateAsync(It.Is<string>(arg => arg == "user"), It.Is<string>(arg => arg == "UserId"), It.Is<string>(arg => arg == "TestId"), It.Is<IReadOnlyDictionary<string, string>>(arg => IsDictEqual(arg, expectedUpdatedUserDict)))).Throws(new Exception());

            var user = await _userRepository.UpdateUserAsync(UserId.Wrap("TestId"), updatedUser);

            Assert.False(user.HasValue);
        }
    }
}
