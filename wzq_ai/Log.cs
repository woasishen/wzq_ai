using System;
using System.IO;

namespace wzq_ai
{
    public static class Log
    {
        private static readonly string Desktop = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "log.wzq");

        private static readonly FileStream fs;
        static Log()
        {
            fs = File.Open(Desktop, FileMode.Create);
            
        }

        public static void Info(string msg)
        {
            using (var sw = new StreamWriter(fs))
            {
                sw.WriteLine(msg);
            }
        }
    }
}
