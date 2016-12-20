using System;
using System.IO;

namespace TestWzq
{
    public static class Log
    {
        private static readonly string DESKTOP = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "log.wzq");

        private static StreamWriter sw;
        static Log()
        {
            var fs = File.Open(DESKTOP, FileMode.Create);
            sw = new StreamWriter(fs);
        }

        public static void Info(string msg)
        {
            sw.WriteLine(msg);
        }
    }
}
