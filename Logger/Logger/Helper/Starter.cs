using Logger.Helper;
using Logger.Providers;
using Logger.Providers.Abstractions;
using Logger.Services;
using Logger.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Logger
{
    public class Starter
    {
        public void Run()
        {
            var starter = new ServiceCollection()
                .AddSingleton<ILoggerService, LoggerService>()
                .AddSingleton<ILoggerFileService, LoggerFileService>()
                .AddSingleton<IConfigService, ConfigService>()
                .AddSingleton<IConfigProvider, ConfigProvider>()
                .AddSingleton<IFileConversionService, JsonConversionService>()
                .AddTransient<IActionsService, ActionsService>()
                .AddScoped<App>()
                .BuildServiceProvider();
            var app = starter.GetService<App>();
            app.Run();
            starter.Dispose();
        }
    }
}