using System;
using System.Threading;
using System.Threading.Tasks;
using Logger.Models;

namespace Logger.Services.Abstractions
{
    public interface ILoggerService
    {
        public event Action<SemaphoreSlim, string> OnBackup;
        public Task LogError(string message);
        public Task LogInfo(string message);
        public Task LogWarning(string message);
        public Task LogAsync(LogType logType, string log);
    }
}