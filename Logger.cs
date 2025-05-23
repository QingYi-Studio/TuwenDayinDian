using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TuwenDayinDian
{
    public class Logger
    {
        private static string LogDir = @"logs";
        private static string LogFileName = $"{DateTime.Now:yyyy-MM-dd} {DateTime.Now:mm}.log";
        private static string LogFilePath = Path.Combine(LogDir, LogFileName);

        public Logger()
        {
            if (!Directory.Exists(LogDir))
            {
                Directory.CreateDirectory(LogDir + "\n");
            }
        }


        public void Write(string text) => File.AppendAllText(LogFilePath, text);
    }
}
