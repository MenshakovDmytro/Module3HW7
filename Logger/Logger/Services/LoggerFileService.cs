using System;
using System.IO;
using System.Threading.Tasks;
using Logger.Services.Abstractions;

namespace Logger.Services
{
    public class LoggerFileService : ILoggerFileService
    {
        private readonly IConfigService _configService;

        private StreamWriter _streamWriter;
        private string _logFilePath;
        private string _backupFilePath;

        public LoggerFileService(IConfigService configService)
        {
            _configService = configService;
            Init();
        }

        public async Task WriteAsync(string data)
        {
            if (_streamWriter == null)
            {
                _streamWriter = new StreamWriter(new FileStream(_logFilePath, FileMode.Append, FileAccess.Write));
            }

            await _streamWriter.WriteLineAsync(data);
            await _streamWriter.FlushAsync();
        }

        public async Task WriteBackupAsync(string data)
        {
            var path = $"{_backupFilePath}{DateTime.UtcNow.ToString(_configService.LoggerConfig.BackupFileName)}{_configService.LoggerConfig.FileExtension}";
            await File.WriteAllTextAsync(path, data);
        }

        public void CreateDirectory(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }

        public void CreateFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }
        }

        public void Dispose()
        {
            _streamWriter.Flush();
            _streamWriter.Close();
            _streamWriter.Dispose();
        }

        private void Init()
        {
            var directoryPath = _configService.LoggerConfig.DirectoryPath;
            var backupDirectoryPath = _configService.LoggerConfig.BackupDirectoryPath;
            var fileName = DateTime.UtcNow.ToString(_configService.LoggerConfig.FileName);
            var fileExtension = _configService.LoggerConfig.FileExtension;
            var file = $"{fileName}{fileExtension}";
            _logFilePath = $"{Directory.GetCurrentDirectory()}{directoryPath}{file}";
            _backupFilePath = $"{Directory.GetCurrentDirectory()}{backupDirectoryPath}";
            CreateDirectory(directoryPath);
            CreateDirectory(backupDirectoryPath);
            CreateFile(_logFilePath);
        }
    }
}