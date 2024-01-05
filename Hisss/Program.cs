using Newtonsoft.Json;

namespace Hisss
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            Configuration config;
            try
            {
                string jstring = File.ReadAllText("Configuration.json");
                config = JsonConvert.DeserializeObject<Configuration>(jstring);
            } catch (Exception ex)
            {
                config = new Configuration();
            }

            Application.Run(new MainForm(config));
        }
    }
}