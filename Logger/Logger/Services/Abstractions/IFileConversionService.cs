using System.Threading.Tasks;
using Logger.Models;

namespace Logger.Services.Abstractions
{
    public interface IFileConversionService
    {
        public Task<Config> ConvertJsonToConfig(string jsonConfig);
    }
}