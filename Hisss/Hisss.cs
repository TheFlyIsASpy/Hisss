//Copyright(c) 2024 Nicolas "Fly" Sheridan
//This code is licensed under MIT license (see LICENSE.txt for details)

using CommandLine;
using CommandLine.Text;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace Hisss
{
    internal static class Hisss
    {

        static string DEFAULT_LOG_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\hisss.log";
        static string DEFAULT_OUTPUT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\hisss_scan";
        const string VERSION = "1.1.0";

        [STAThread]
        static void Main(String[] args)
        {

            ApplicationConfiguration.Initialize();

            string version = "1.1.0";
            
            for(int i = 0; i < args.Length; i++) 
            {
                args[i] = args[i].Trim();
                args[i] = args[i].ToLower();
                if (args[i].Equals("--help"))
                {
                    args[i] = "-h";
                }
            }
            
            var parser = new Parser(s =>
            {
                s.AllowMultiInstance = true;
                s.AutoHelp = true;
                s.CaseSensitive = false;
                s.IgnoreUnknownArguments = true;
            });

            var parser_result = parser.ParseArguments<Configuration>(args);

            parser_result.WithParsed(c =>
            {
                ConsoleRegister.RegisterGUIConsoleWriter();

                if (c.help)
                {
                    PrintHelp(parser_result);
                    return;
                }

                LogWriter lw = BuildLogWriter(c);
                CheckOutputPath(c, lw);
                ReadConfigJson(c, lw);

                var form = new ScanForm(c, lw);
                lw.Log("Finished executing");
                lw.Close();
                return;
            });

            parser_result.WithNotParsed<Configuration>(errs =>
            {
                var help_text = HelpText.AutoBuild(parser_result, h =>
                {
                    h.AdditionalNewLineAfterOption = false;
                    return HelpText.DefaultParsingErrorsHandler(parser_result, h);
                }, e => e);
                help_text.Heading = "HISSS (Health Industry Shitty Scanner Software) " + version;
                help_text.Copyright = "\nCopyright (c) 2024 Nicolas \"Fly\" Sheridan\nThis code is licensed under MIT license (see LICENSE.txt for details)";
                Console.WriteLine(help_text);
            });
        }

        private static void ReadConfigJson(Configuration c, LogWriter lw)
        {
            try
            {
                string jstring = File.ReadAllText("%userprofile%\\hisss_overrides.json");
                JsonConvert.PopulateObject(jstring, c);
            }
            catch (Exception ex)
            {
                lw.Log("Failed to read json configuration. If you don't have one, ignore this error. Otherwise, Error:\n" + ex.ToString());
            }
        }

        private static void CheckOutputPath(Configuration c, LogWriter lw)
        {
            if (c.FileName == null)
            {
                lw.Log("Defaulting output path to: " + DEFAULT_OUTPUT_PATH);
                c.FileName = DEFAULT_OUTPUT_PATH;
            }
            else
            {
                try
                {
                    _ = new FileInfo(c.FileName).Directory;
                }
                catch (Exception e)
                {
                    lw.Log("Provided Path and Filename is invalid, check the path and permissions. Error: " + e.Message);
                    lw.Log("Path Given: " + c.FileName);
                    lw.Log("Defaulting to: " + DEFAULT_OUTPUT_PATH);
                    c.FileName = DEFAULT_OUTPUT_PATH;
                }
            }
        }

        private static LogWriter BuildLogWriter(Configuration c)
        {
            if (c.LogPath == null)
            {
                Console.WriteLine("Defaulting log path to: " + DEFAULT_LOG_PATH);
                c.LogPath = DEFAULT_LOG_PATH;
            }
            else
            {
                try
                {
                    _ = new FileInfo(c.LogPath).Directory;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Provided LogPath was invalid. Error: " + e.Message);
                    Console.WriteLine("Path Given: " + c.LogPath);
                    Console.WriteLine("Defaulting log path to: " + DEFAULT_LOG_PATH);
                    c.LogPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\hisss.log";
                }
            }

            LogWriter lw = new LogWriter(c.LogPath);
            return lw;
        }

        private static void PrintHelp(ParserResult<Configuration> parser_result)
        {
            var help_text = HelpText.AutoBuild(parser_result, h =>
            {
                h.AdditionalNewLineAfterOption = false;
                return HelpText.DefaultParsingErrorsHandler(parser_result, h);
            }, e => e);
            help_text.Heading = "HISSS (Health Industry Shitty Scanner Software) " + VERSION;
            help_text.Copyright = "\nCopyright (c) 2024 Nicolas \"Fly\" Sheridan\nThis code is licensed under MIT license (see LICENSE.txt for details)";
            Console.WriteLine(help_text);
        }
    }
}