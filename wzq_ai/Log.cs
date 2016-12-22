using System;
using System.IO;

namespace wzq_ai
{
    public static class Log
    {
        private static readonly string Desktop = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "log.wzq");

        private static StreamWriter sw;
        static Log()
        {
            var fs = File.Open(Desktop, FileMode.Create);
            sw = new StreamWriter(fs);
        }

        public static void Info(string msg)
        {
            sw.WriteLine(msg);
        }
    }
}
