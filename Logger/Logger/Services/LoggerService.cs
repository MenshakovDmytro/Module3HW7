using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Logger.Models;
using Logger.Services.Abstractions;

namespace Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly ILoggerFileService _fileService;
        private readonly IConfigService _configService;
        private readonly StringBuilder _logData = new (string.Empty);
        private readonly SemaphoreSlim _semaphore;
        private string _timeFormat;
        private int _counter = 1;
        private int _backupLine;

        public LoggerService(ILoggerFileService fileService, IConfigService configService)
        {
            _fileService = fileService;
            _configService = configService;
            _semaphore = new SemaphoreSlim(1);
            _timeFormat = _configService.LoggerConfig.TimeFormat;
            _backupLine = _configService.LoggerConfig.BackupLine;
        }

        public event Action<SemaphoreSlim, string> OnBackup;

        public async Task LogError(string message)
        {
            await LogAsync(LogType.Error, message);
        }

        public async Task LogInfo(string message)
        {
            await LogAsync(LogType.Info, message);
        }

        public async Task LogWarning(string message)
        {
            await LogAsync(LogType.Warning, message);
        }

        public async Task LogAsync(LogType logType, string log)
        {
            var message = $"{DateTime.UtcNow.ToString(_timeFormat)}: {logType}: {log}";

            Console.WriteLine(message);
            await _semaphore.WaitAsync();
            _logData.AppendLine(message);
            await _fileService.WriteAsync(message);
            await BackupLogs();
        }

        public async Task BackupLogs()
        {
            await Task.Run(() =>
            {
                if (_counter++ % _backupLine == 0)
                {
                    _logData.AppendLine(new string('=', 119));
                    OnBackup.Invoke(_semaphore, _logData.ToString());
                    return;
                }

                _semaphore.Release();
            });
        }
    }
}