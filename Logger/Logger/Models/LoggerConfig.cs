namespace Logger.Models
{
    public class LoggerConfig
    {
        public string TimeFormat { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string DirectoryPath { get; set; }
        public string BackupDirectoryPath { get; set; }
        public int BackupLine { get; set; }
        public string BackupFileName { get; set; }
    }
}