using System;

namespace BackupsExtra.Services
{
    [Serializable]
    public class ConsoleLog : ILogger
    {
        private bool _turnPrefix;

        public ConsoleLog(bool turnPrefix)
        {
            _turnPrefix = turnPrefix;
        }

        public void Log(string letter)
        {
            if (_turnPrefix)
            {
                Console.WriteLine($"[{DateTime.Now}] {letter}");
            }
            else
            {
                Console.WriteLine($"{letter}");
            }
        }
    }
}