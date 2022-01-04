using System;
using System.Threading.Tasks;

namespace Logger.Services.Abstractions
{
    public interface ILoggerFileService : IDisposable
    {
        public Task WriteAsync(string data);
        public Task WriteBackupAsync(string data);
        public void CreateDirectory(string directoryName);
        public void CreateFile(string fileName);
    }
}