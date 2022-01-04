using Logger.Models;
using Logger.Providers.Abstractions;
using Logger.Services.Abstractions;

namespace Logger.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigProvider _configProvider;

        public ConfigService(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public LoggerConfig LoggerConfig => _configProvider.Config.LoggerConfig;
    }
}