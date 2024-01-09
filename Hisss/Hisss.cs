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
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
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

            var parser_result = Parser.Default.ParseArguments<Configuration>(args);

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
                    if (c.FileName == null)
                    {
                        c.FileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp\\scan";
                    }
                    
                    try
                    {
                        string jstring = File.ReadAllText("%userprofile%\\hisss_overrides.json");
                        JsonConvert.PopulateObject(jstring, c);
                    }catch (Exception ex) {
                        Console.WriteLine("Failed to read json configuration. If you don't have one, ignore this error. Otherwise: \n\n" +  ex.ToString());    
                    }
                    var form = new MainForm(c);
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