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
        [STAThread]
        static void Main(String[] args)
        {

            ApplicationConfiguration.Initialize();

            string version = "1.0";
            
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

            parser_result.WithParsed<Configuration>(c =>
            {
                ConsoleRegister.RegisterGUIConsoleWriter();
                if (c.help)
                {
                    var help_text = HelpText.AutoBuild(parser_result, h => {
                        h.AdditionalNewLineAfterOption = false;
                        return h;
                    }, e => e);
                    help_text.Heading = "HISSS (Health Industry Shitty Scanner Software) " + version;
                    help_text.Copyright = "\nCopyright (c) 2024 Nicolas \"Fly\" Sheridan\nThis code is licensed under MIT license (see LICENSE.txt for details)";
                    Console.WriteLine(help_text);
                }
                else
                {                    
                    
                    if (c.LogPath == null)
                    {
                        c.LogPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\hisss.log";
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
                            c.LogPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\hisss.log";
                        }
                    }

                    LogWriter lw = new LogWriter(c.LogPath);

                    if (c.FileName == null)
                    {
                        c.FileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\scan";
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
                            c.FileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\scan";
                        }
                    }
                    
                    try
                    {
                        string jstring = File.ReadAllText("%userprofile%\\hisss_overrides.json");
                        JsonConvert.PopulateObject(jstring, c);
                    }catch (Exception ex) {
                        lw.Log("Failed to read json configuration. If you don't have one, ignore this error. Otherwise, Error:\n" +  ex.ToString());    
                    }

                    var form = new ScanForm(c, lw);
                    lw.Log("Finished executing");
                    lw.Close();
                    return;
                }
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
    }
}