using System;
using System.IO;

namespace BackupsExtra.Services
{
    [Serializable]
    public class FileLog : ILogger
    {
        private bool _turnPrefix;

        public FileLog(bool turnPrefix)
        {
            _turnPrefix = turnPrefix;
        }

        public void Log(string letter)
        {
            if (_turnPrefix)
            {
                File.AppendAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Log.txt"),  new[] { $"[{DateTime.Now}] {letter}" });
            }
            else
            {
                File.AppendAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Log.txt"),  new[] { $"{letter}" });
            }
        }
    }
}