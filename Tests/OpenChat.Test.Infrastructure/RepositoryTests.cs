using MartinCostello.Logging.XUnit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenChat.Persistence;
using OpenChat.Test.Infrastructure.Factories;
using TestSupport.Helpers;
using Xunit.Abstractions;

namespace OpenChat.Test.Infrastructure
{
    public class RepositoryTests
    {
        protected OpenChatDbContext DbContext { get; private set; }

        private readonly ILoggerFactory _loggerFactory;
        
        public RepositoryTests(ITestOutputHelper testOutputHelper)
        {
            _loggerFactory = LoggerFactory.Create(builder => {
                builder.ClearProviders();
                builder.AddConsole();
                builder.AddXUnit(testOutputHelper);
            });

            var providerType = GetProviderType();

            DbContext = OpenChatDbContextFactory.GetDbContext(providerType, _loggerFactory);
        }

        private ProviderType GetProviderType()
        {
            var config = AppSettings.GetConfiguration();

            return config.GetValue<ProviderType>("ProviderType");
        }
    }
}