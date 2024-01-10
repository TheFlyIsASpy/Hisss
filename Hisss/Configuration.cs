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

        [Option('h', Required = false, Default = false)]
        public bool help { get; set; }

        [Option("automatic_rotate_mode", Required = false, Default = (short)0, HelpText = "Sets a mode for detecting the orientation of an image when an image is automatically\r\nrotated.\n0 - Standard Standard\r\n1 - Custom Rotation based on the reference area\n")]
        public short AutomaticRotateMode { get; set; }
        
        [Option("feed_method", Required = false, Default = (short)1, HelpText = "Sets the paper feed method.\n0 - Flatbed Flatbed\r\n1 - ADF ADF(Face scan)")]
        public short PaperSupply { get; set; }

        [Option("format", Required = false, Default = (short)4, HelpText = "Sets the file format.\n0 - BMP Bitmap file\r\n1 - TIFF TIFF file\r\n2 - Multipage TIFF Multipage TIFF file\r\n3 - JPEG JPEG file\r\n4 - PDF PDF file\r\n5 - Multipage PDF Multipage PDF file\r\n6 - Multi Image Output Multi-image output(Black and white: TIFF file, Others: JPEG\r\nfile)\r\n7 - Auto Color Detection Auto color detection(Black and white: TIFF file, Others: JPEG\r\nfile)")]
        public short FileType { get; set; }

        [Option('i', Required = false, Default = false, HelpText = "Show the progress indicator while scanning.")]
        public bool Indicator { get; set; }

        [Option("overwrite", Required = false, Default = (short)1, HelpText = "Sets whether or not to overwrite files.\n0 - OFF(Mode0) Does not overwrite\r\n(When file type is TIFF, JPEG or BMP without using “Image\r\nProcessing Software Option “, processes the number of sheets\r\nspecified for the ScanCount property up to the last sheet even if\r\na file with the same name exists.)\r\n1 - ON Overwrites.\r\n2 - Confirm(Mode0) Displays the confirmation message box. (Displayed even in\r\nSilentMode.)\r\n3 - OFF(Mode1) Does not overwrite. (If a file with the same name exists, aborts\r\nscanning.)\r\n4 - Confirm(Mode1) Displays the confirmation message box.\r\n(Turned to the same operation as \"3 - OFF(Mode1)\" in\r\nSilentMode.)")]
        public short Overwrite { get; set; }
        
        [Option("path", Required = false, Default = null, HelpText = "Sets the path and file name for storing the image. (Extension not included)")]
        public string FileName { get; set; }

        [Option("pixel_type", Required = false, Default = (short)1, HelpText = "Sets the pixel type.\n0 - Black & White Binary (Black and White)\r\n1 - Grayscale Grayscale\r\n2 - RGB RGB color\r\n3 - Automatic Auto color detection\r\n4 - SwitchByCodeSheet Switching by code sheets")]
        public short PixelType { get; set; }
        
        [Option("resolution", Required = false, Default = (short)2, HelpText = "Specifies the scan resolution.\n0 - 200x200 [dpi]\r\n1 - 240x240 [dpi]\r\n2 - 300x300 [dpi]\r\n3 - 400x400 [dpi]\r\n4 - 500x500 [dpi]\r\n5 - 600x600 [dpi]\r\n6 - 700x700 [dpi]\r\n7 - 800x800 [dpi]\r\n9 - 1200x1200 [dpi]")]
        public short Resolution { get; set; }

        [Option("ui", Required = false, Default = false, HelpText = "Display the source user interface (UI).")]
        public bool ShowSourceUI { get; set; }

        [Option("AutoProfile", Required = false, Default = (short)1, HelpText = "Sets whether to identify a scanned form and apply a profile associated with the form automatically.\n0 - Disabled Does not apply a profile automatically.\r\n1 - Enabled Applies a profile automatically.")]
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
