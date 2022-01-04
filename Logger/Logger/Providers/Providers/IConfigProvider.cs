using Logger.Models;

namespace Logger.Providers.Abstractions
{
    public interface IConfigProvider
    {
        public Config Config { get; }
    }
}