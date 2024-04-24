namespace Hisss
{
    public static class LogWriter
    {
        private static string path;
        private static StreamWriter sw;
        public static bool Initialize(string log_output_path)
        {
            path = log_output_path;

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            try
            {
                sw = File.CreateText(path);
            }catch (Exception ex)
            {
                return false;
            }

            sw.AutoFlush = true;
            sw.WriteLine("Logger Initialized");
            return true;
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
