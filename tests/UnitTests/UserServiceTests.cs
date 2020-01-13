using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RelativeRank.Config;
using RelativeRank.DataTransferObjects;
using Moq;
using Xunit;
using RelativeRank.Interfaces;
using System.Threading.Tasks;
using RelativeRank.Services;
using System;

namespace RelativeRankTests.UnitTests
{
    public class UserServiceTests
    {
        private IOptions<AppSettings> _appSettingsOptions;
        private Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<IOptions<AppSettings>>(
                provider => Options.Create<AppSettings>(new AppSettings
                {
                    Secret = "secret with at least thirty two characters so an exception isn't thrown :)"
                }));
            var serviceProvider = services.BuildServiceProvider();

            _appSettingsOptions = serviceProvider.GetService<IOptions<AppSettings>>();
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task CreateNewUser_CreatesANewUser_WhenPassedNonEmptyUsernameAndPassword()
        {
            // Arrange
            var username = "username";
            var password = "password";

            var fakeId = 1;

            _userRepositoryMock.Setup(mock => mock.CreateNewUser(It.IsAny<RelativeRank.EntityFrameworkEntities.User>()))
                .Returns(Task.FromResult(new RelativeRank.Entities.User()
                {
                    Id = fakeId,
                    Username = username
                }));

            var userService = new UserService(_userRepositoryMock.Object, null, _appSettingsOptions);

            // Act
            var createdUser = await userService.CreateNewUser(new SignUpModel
            {
                Username = username,
                Password = password
            });

            // Assert
            Assert.Equal(fakeId, createdUser.Id);
            Assert.Equal(username, createdUser.Username);
            Assert.Null(createdUser.Password);
        }

        [Fact]
        public async Task CreateNewUser_ReturnsNull_WhenPassedEmptyUsername()
        {
            // Arrange
            var username = "";
            var password = "password";

            var fakeId = 1;

            _userRepositoryMock.Setup(mock => mock.CreateNewUser(It.IsAny<RelativeRank.EntityFrameworkEntities.User>()))
                .Returns(Task.FromResult(new RelativeRank.Entities.User()
                {
                    Id = fakeId,
                    Username = username
                }));

            var userService = new UserService(_userRepositoryMock.Object, null, _appSettingsOptions);

            // Act
            var createdUser = await userService.CreateNewUser(new SignUpModel
            {
                Username = username,
                Password = password
            });

            // Assert
            Assert.Null(createdUser);
        }

        [Fact]
        public async Task CreateNewUser_ReturnsNull_WhenPassedEmptyPassword()
        {
            // Arrange
            var username = "username";
            var password = "";

            var fakeId = 1;

            _userRepositoryMock.Setup(mock => mock.CreateNewUser(It.IsAny<RelativeRank.EntityFrameworkEntities.User>()))
                .Returns(Task.FromResult(new RelativeRank.Entities.User()
                {
                    Id = fakeId,
                    Username = username
                }));

            var userService = new UserService(_userRepositoryMock.Object, null, _appSettingsOptions);

            // Act
            var createdUser = await userService.CreateNewUser(new SignUpModel
            {
                Username = username,
                Password = password
            });

            // Assert
            Assert.Null(createdUser);
        }

        [Fact]
        public void PasswordHash_CreatedFromPasswordAndSalt_IsSuccessfullyValidated()
        {
            // Arrange
            var password = "password";

            // Act
            byte[] passwordHash;
            byte[] passwordSalt;
            UserService.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var isValid = UserService.VerifyPasswordHash(password, passwordHash, passwordSalt);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void VerifyPasswordHash_WhenPassedEmptyPassword_ThrowsArgumentException()
        {
            // Arrange
            var password = "password";
            var emptyPassword = "";
            byte[] passwordHash;
            byte[] passwordSalt;
            UserService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => UserService.VerifyPasswordHash(emptyPassword, passwordHash, passwordSalt));
        }

        [Fact]
        public void VerifyPasswordHash_WhenPassedEmptyPasswordHash_ThrowsArgumentException()
        {
            // Arrange
            var password = "password";
            byte[] passwordHash;
            byte[] passwordSalt;
            UserService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => UserService.VerifyPasswordHash(password, null, passwordSalt));
        }

        [Fact]
        public void VerifyPasswordHash_WhenPassedEmptyPasswordSalt_ThrowsArgumentException()
        {
            // Arrange
            var password = "password";
            byte[] passwordHash;
            byte[] passwordSalt;
            UserService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => UserService.VerifyPasswordHash(password, passwordSalt, null));
        }
    }
}
