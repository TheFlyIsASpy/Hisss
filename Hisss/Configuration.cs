//Copyright(c) 2024 Nicolas "Fly" Sheridan
//This code is licensed under MIT license (see LICENSE.txt for details)

using CommandLine;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hisss
{
    public class Configuration
    {

        [Option('h', Required = false, Default = false, HelpText = "Prints this help menu")]
        public bool help { get; set; }

        [Option("log_path", Required = false, Default = null, HelpText = "Changes the path to save logs to.\nWARNING: STARLIMS reads this log file to determine if scanning is done, change in starlims if you change here.")]
        public string? LogPath { get; set; }

        [Option("automatic_rotate_mode", Required = false, Default = (short)0, HelpText = "Sets a mode for detecting the orientation of an image when an image is automatically rotated.\n0 - Standard Standard\n1 - Custom Rotation based on the reference area\n")]
        public short AutomaticRotateMode { get; set; }
        
        [Option("feed_method", Required = false, Default = (short)1, HelpText = "Sets the paper feed method.\n0 - Flatbed \n1 - ADF (Face scan)")]
        public short PaperSupply { get; set; }

        [Option("format", Required = false, Default = (short)4, HelpText = "Sets the file format.\n0 - BMP Bitmap file\n1 - TIFF file\n2 - Multipage TIFF file\n3 - JPEG file\n4 - PDF file\n5 - Multipage PDF file\n6 - Multi Image Output\n(Black and white: TIFF file, Others: JPEG file)\n7 - Auto Color Detection\n(Black and white: TIFF file, Others: JPEG file)")]
        public short FileType { get; set; }

        [Option('i', Required = false, Default = false, HelpText = "Show the progress indicator while scanning.")]
        public bool Indicator { get; set; }

        [Option("overwrite", Required = false, Default = (short)1, HelpText = "Sets whether or not to overwrite files.\n\n0 - OFF (Mode0) Does not overwrite\n(When file type is TIFF, JPEG or BMP without using \"Image Processing Software Option\", processes the number of sheets specified for the ScanCount property up to the last sheet even if a file with the same name exists.)\n\n1 - ON Overwrites.\n\n2 - Confirm (Mode0) Displays the confirmation message box.\n(Displayed even in SilentMode.)\n\n3 - OFF (Mode1) Does not overwrite.\n(If a file with the same name exists, aborts scanning.)\n\n4 - Confirm (Mode1) Displays the confirmation message box.\n(Turned to the same operation as \"3 - OFF (Mode1)\" in SilentMode.)")]
        public short Overwrite { get; set; }

        [Option("path", Required = false, Default = null, HelpText = "Full output path and filename for the resulting scan. (Dont include extension on filename)")]
        public string? FileName { get; set; }

        [Option("pixel_type", Required = false, Default = (short)1, HelpText = "Sets the pixel type.\n0 - Black & White Binary (Black and White)\n1 - Grayscale Grayscale\n2 - RGB RGB color\n3 - Automatic Auto color detection\n4 - SwitchByCodeSheet Switching by code sheets")]
        public short PixelType { get; set; }
        
        [Option("resolution", Required = false, Default = (short)2, HelpText = "Specifies the scan resolution.\n0 - 200x200 [dpi]\n1 - 240x240 [dpi]\n2 - 300x300 [dpi]\n3 - 400x400 [dpi]\n4 - 500x500 [dpi]\n5 - 600x600 [dpi]\n6 - 700x700 [dpi]\n7 - 800x800 [dpi]\n9 - 1200x1200 [dpi]")]
        public short Resolution { get; set; }

        [Option("ui", Required = false, Default = false, HelpText = "Display the full driver user interface to scan.")]
        public bool ShowSourceUI { get; set; }

        [Option("AutoProfile", Required = false, Default = (short)1, HelpText = "Sets whether to identify a scanned form and apply a profile associated with the form automatically.\n\n0 - Disabled Does not apply a profile automatically.\n1 - Enabled Applies a profile automatically.")]
        public short AutoProfile { get; set; }

        public void Apply(AxFiScnLib.AxFiScn scanner_control)
        {
            scanner_control.AutomaticRotateMode = AutomaticRotateMode;
            scanner_control.PaperSupply = PaperSupply;
            scanner_control.FileType = FileType;
            scanner_control.Indicator = Indicator;
            scanner_control.Overwrite = Overwrite;
            scanner_control.FileName = FileName;
            scanner_control.PixelType = PixelType;
            scanner_control.Resolution = Resolution;
            scanner_control.ShowSourceUI = ShowSourceUI;
            scanner_control.AutoProfile = AutoProfile;
        }
    }
}
