using System.IO;
using System.Threading.Tasks;
using Logger.Models;
using Logger.Services.Abstractions;
using Newtonsoft.Json;

namespace Logger.Services
{
    public class JsonConversionService : IFileConversionService
    {
        public async Task<Config> ConvertJsonToConfig(string jsonConfig)
        {
            return await Task.Run(async () =>
            {
                var configFile = await File.ReadAllTextAsync(jsonConfig);
                return JsonConvert.DeserializeObject<Config>(configFile);
            });
        }
    }
}