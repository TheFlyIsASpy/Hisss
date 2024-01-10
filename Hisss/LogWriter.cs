using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hisss
{
    public class LogWriter
    {
        private string path;
        private StreamWriter sw;
        public LogWriter(string path)
        {
            this.path = path;

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            sw = File.CreateText(path);

            sw.AutoFlush = true;
            sw.WriteLine("Logger Initialized");
        }

        public void Log(string m)
        {
            sw.WriteLine(m);
        }

        public void Close()
        {
            sw.Close();
        }
    }
}
