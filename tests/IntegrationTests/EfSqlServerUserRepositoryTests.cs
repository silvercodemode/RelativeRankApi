using Xunit;
using RelativeRank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RelativeRank.Data;

namespace RelativeRankTests.IntegrationTests
{
    public class EfSqlServerUserRepositoryTests
    {
        private RelativeRankContext _context;
        private IOptions<AppSettings> _appSettingsOptions;

        public EfSqlServerUserRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<RelativeRankContext>()
                .UseInMemoryDatabase(databaseName: "EfSqlServerUserRepositoryTests")
                .Options;
            _context = new RelativeRankContext(dbContextOptions);

            var services = new ServiceCollection();
            services.AddTransient<IOptions<AppSettings>>(
                provider => Options.Create<AppSettings>(new AppSettings
                {
                    Secret = "secret with at least thirty two characters so an exception isn't thrown :)"
                }));
            var serviceProvider = services.BuildServiceProvider();
            _appSettingsOptions = serviceProvider.GetService<IOptions<AppSettings>>();
        }

        [Fact]
        public async void LoginWithValidCredentialsShouldReturnUserWithAToken()
        {
            var authenticationService = new AuthenticationService();
            var repository = new EfSqlServerUserRepository(_context, _appSettingsOptions, authenticationService);

            var username = "username";
            var password = "password";
            var hashedPassword = authenticationService.Hash(password);

            _context.User.Add(new RelativeRank.EntityFrameworkEntities.User
            {
                Username = username,
                Password = hashedPassword
            });
            _context.SaveChanges();

            var user = await repository.Login(username, password);

            Assert.NotNull(user);
            Assert.NotNull(user.Token);
            Assert.IsType<string>(user.Token);
        }
    }
}
