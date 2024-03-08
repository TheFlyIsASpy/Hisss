//Copyright(c) 2024 Nicolas "Fly" Sheridan
//This code is licensed under MIT license (see LICENSE.txt for details)

using CommandLine;
using CommandLine.Text;
using Hisss.Properties;
using Microsoft.Win32;
using System.Diagnostics;

namespace Hisss
{
    internal static class Hisss
    {

        static string DEFAULT_LOG_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\hisss.log";
        static string DEFAULT_OUTPUT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\hisss_scan";
        static string DEFAULT_OVERRIDES_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\hisss_overrides.json";
        const string VERSION = "2.1.2";

        [STAThread]
        static void Main(String[] args)
        {

            ApplicationConfiguration.Initialize();

            string version = "2.1.2";
            
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

                if (c.guid.Equals(""))
                {
                    c.guid = Guid.NewGuid().ToString();
                }

                c.guid = c.guid.Trim();

                BuildLogWriter(c);
                if (!CheckRuntime(c))
                {
                    LogWriter.Log("Runtime failed to validate or install, exiting");
                    return;
                }
                CheckOutputPath(c);
                ReadConfigJson(c);
                
                var form = new ScanForm(c);
                LogWriter.Log("Finished executing");
                LogWriter.Close();
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

        private static bool CheckRuntime(Configuration c)
        {
            string InstallReg = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Fujitsu Scanner Control Runtime x64";

            LogWriter.Log("Checking for runtime registry key");
            if (Registry.LocalMachine.OpenSubKey(InstallReg) != null)
            {
                LogWriter.Log("Runtime found");
                return true;
            }

            LogWriter.Log("Runtime not found");
            LogWriter.Log("Downloading install files");
            DirectoryInfo d = System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\eng");
            File.WriteAllBytes(d.FullName + "\\LICENSE.TXT", Resources.LICENSE);
            File.WriteAllBytes(d.FullName + "\\README.TXT", Resources.README);
            File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\FiScn.ini", Resources.FiScn);
            File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\FiScnRun.exe", Resources.FiScnRun);

            LogWriter.Log("Starting silent install");
            Process installer = new Process();
            installer.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\FiScnRun.exe";
            installer.StartInfo.ArgumentList.Add("-silent");
            installer.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp";
            installer.StartInfo.UseShellExecute = true;
            installer.StartInfo.Verb = "runas";
            installer.Start();
            installer.WaitForExit(20000);
            
            LogWriter.Log("Install process finished");
            LogWriter.Log("Cleaning up install files");
            File.Delete(d.FullName + "\\LICENSE.TXT");
            File.Delete(d.FullName + "\\README.TXT");
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\FiScn.ini");
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\FiScnRun.exe");

            if (installer.ExitCode != 0)
            {
                LogWriter.Log("Install exited with an error: " +  installer.ExitCode);
                return false;
            }

            LogWriter.Log("Install successful");
            return true;
        }

        private static void ReadConfigJson(Configuration c)
        {
            if (c.OverridesPath == null)
            {
                LogWriter.Log("Defaulting overrides path to: " + DEFAULT_OVERRIDES_PATH);
                c.OverridesPath = DEFAULT_OVERRIDES_PATH;
            }
            else
            {
                try
                {
                    _ = new FileInfo(c.OverridesPath).Directory;
                }
                catch (Exception e)
                {
                    LogWriter.Log("Provided Path and Filename is invalid, check the path and permissions. Error: " + e.Message);
                    LogWriter.Log("Path Given: " + c.OverridesPath);
                    LogWriter.Log("Defaulting to: " + DEFAULT_OVERRIDES_PATH);
                    c.OverridesPath = DEFAULT_OVERRIDES_PATH;
                }
            }
        }

        private static void CheckOutputPath(Configuration c)
        {
            if (c.FileName == null)
            {
                LogWriter.Log("Defaulting output path to: " + DEFAULT_OUTPUT_PATH);
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
                    LogWriter.Log("Provided Path and Filename is invalid, check the path and permissions. Error: " + e.Message);
                    LogWriter.Log("Path Given: " + c.FileName);
                    LogWriter.Log("Defaulting to: " + DEFAULT_OUTPUT_PATH);
                    c.FileName = DEFAULT_OUTPUT_PATH;
                }
            }
        }

        private static void BuildLogWriter(Configuration c)
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
                    c.LogPath = DEFAULT_LOG_PATH;
                }
            }
            int ext_index = c.LogPath.IndexOf(".");
            string ext = ".log";
            if (ext_index < 0)
            {
                c.LogPath = c.LogPath + "_" + c.guid + ext;
            }
            else
            {
                ext = c.LogPath.Substring(ext_index);
                c.LogPath = c.LogPath.Replace(ext, "_" + c.guid + ext);
            }
            LogWriter.Initialize(c.LogPath);
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