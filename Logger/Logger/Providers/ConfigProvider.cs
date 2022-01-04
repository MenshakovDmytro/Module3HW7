using Logger.Models;
using Logger.Providers.Abstractions;
using Logger.Services.Abstractions;

namespace Logger.Providers
{
    public class ConfigProvider : IConfigProvider
    {
        private const string ConfigFile = "config.json";

        private readonly Config _config;
        private readonly IFileConversionService _fileConversionService;

        public ConfigProvider(IFileConversionService fileConversionService)
        {
            _fileConversionService = fileConversionService;
            _config = LoadConfig();
        }

        public Config Config => _config;

        private Config LoadConfig()
        {
            return _fileConversionService.ConvertJsonToConfig(ConfigFile).GetAwaiter().GetResult();
        }
    }
}