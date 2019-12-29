using Xunit;
using RelativeRank.Config;
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
    }
}
