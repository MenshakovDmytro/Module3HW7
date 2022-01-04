using Logger.Models;

namespace Logger.Services.Abstractions
{
    public interface IConfigService
    {
        public LoggerConfig LoggerConfig { get; }
    }
}