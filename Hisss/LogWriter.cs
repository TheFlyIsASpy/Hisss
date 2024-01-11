using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hisss
{
    public static class LogWriter
    {
        private static string path;
        private static StreamWriter sw;
        public static void Initialize(string log_output_path)
        {
            path = log_output_path;

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            sw = File.CreateText(path);

            sw.AutoFlush = true;
            sw.WriteLine("Logger Initialized");
        }

        public static void Log(string m)
        {
            sw.WriteLine(m);
        }

        public static void Close()
        {
            sw.Close();
        }
    }
}
