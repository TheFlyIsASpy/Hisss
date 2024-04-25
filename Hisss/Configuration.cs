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

        [Option("overrides_path", Required = false, Default = null, HelpText = "Changes the path to the hisss_overrides.json file")]
        public string? OverridesPath { get; set; }

        [Option("automatic_rotate_mode", Required = false, Default = (short)0, HelpText = "Sets a mode for detecting the orientation of an image when an image is automatically rotated.\n0 - Standard Standard\n1 - Custom Rotation based on the reference area\n")]
        public short AutomaticRotateMode { get; set; }
        
        [Option("feed_method", Required = false, Default = (short)2, HelpText = "Sets the paper feed method.\n0 - Flatbed \n1 - ADF (Face scan)")]
        public short PaperSupply { get; set; }

        [Option("format", Required = false, Default = (short)5, HelpText = "Sets the file format.\n0 - BMP Bitmap file\n1 - TIFF file\n2 - Multipage TIFF file\n3 - JPEG file\n4 - PDF file\n5 - Multipage PDF file\n6 - Multi Image Output\n(Black and white: TIFF file, Others: JPEG file)\n7 - Auto Color Detection\n(Black and white: TIFF file, Others: JPEG file)")]
        public short FileType { get; set; }

        [Option('i', Required = false, Default = false, HelpText = "Show the progress indicator while scanning.")]
        public bool Indicator { get; set; }

        [Option("overwrite", Required = false, Default = (short)1, HelpText = "Sets whether or not to overwrite files.\n\n0 - OFF (Mode0) Does not overwrite\n(When file type is TIFF, JPEG or BMP without using \"Image Processing Software Option\", processes the number of sheets specified for the ScanCount property up to the last sheet even if a file with the same name exists.)\n\n1 - ON Overwrites.\n\n2 - Confirm (Mode0) Displays the confirmation message box.\n(Displayed even in SilentMode.)\n\n3 - OFF (Mode1) Does not overwrite.\n(If a file with the same name exists, aborts scanning.)\n\n4 - Confirm (Mode1) Displays the confirmation message box.\n(Turned to the same operation as \"3 - OFF (Mode1)\" in SilentMode.)")]
        public short Overwrite { get; set; }

        [Option("path", Required = false, Default = null, HelpText = "Full output path and filename for the resulting scan. (Dont include extension on filename)")]
        public string? FileName { get; set; }

        [Option("file_counter", Required = false, Default = 1, HelpText = "\n-2 - a file serial number is not set for the file name, and only the file name is used.\n0 to 99999999 - sets the starting number for the serial number on the file")]
        public int FileCounterEx { get; set; }

        [Option("pixel_type", Required = false, Default = (short)1, HelpText = "Sets the pixel type.\n0 - Black & White Binary (Black and White)\n1 - Grayscale Grayscale\n2 - RGB RGB color\n3 - Automatic Auto color detection\n4 - SwitchByCodeSheet Switching by code sheets")]
        public short PixelType { get; set; }
        
        [Option("resolution", Required = false, Default = (short)2, HelpText = "Specifies the scan resolution.\n0 - 200x200 [dpi]\n1 - 240x240 [dpi]\n2 - 300x300 [dpi]\n3 - 400x400 [dpi]\n4 - 500x500 [dpi]\n5 - 600x600 [dpi]\n6 - 700x700 [dpi]\n7 - 800x800 [dpi]\n9 - 1200x1200 [dpi]")]
        public short Resolution { get; set; }

        [Option("ui", Required = false, Default = false, HelpText = "Display the full driver user interface to scan")]
        public bool ShowSourceUI { get; set; }

        [Option("auto_profile", Required = false, Default = (short)1, HelpText = "Sets whether to identify a scanned form and apply a profile associated with the form automatically.\n\n0 - Disabled Does not apply a profile automatically.\n1 - Enabled Applies a profile automatically.")]
        public short AutoProfile { get; set; }

        [Option("continuous", Required = false, Default = false, HelpText = "Activates continuous scanning")]
        public bool ScanContinue { get; set; }

        [Option("continuous_mode", Required = false, Default = (short)1, HelpText = "Sets continuous scanning mode.\n0 - Manual\n1 - Automatic")]
        public short ScanContinueMode { get; set; }

        [Option('b', Required = false, Default = false, HelpText = "Activates barcode detection")]
        public bool BarcodeDetection { get; set; }

        [Option("barcode_type", Required = false, Default = (short)512, HelpText = "Sets the barcode type.\n\nFor multiple types, add the values together\n\n1 - EAN 8\n2 - EAN 13\n4 - Code 3 of 9\n8 - Code 128\n16 - ITF\n32 - UPC-A\n64 - Codabar\n128 - PDF417\n256 - QR code\n512 - Data Matrix")]
        public short BarcodeType { get; set; }

        [Option("barcode_direction", Required = false, Default = (short)2, HelpText = "Sets the direction of the barcode that is detected.\n\n0 - Horizontal Horizontal direction\n1 - Vertical Vertical direction\n2 - Horizontal & Vertical Horizontal and vertical directions")]
        public short BarcodeDirection { get; set; }

        [Option("barcode_limit", Required = false, Default = (short)1, HelpText = "Sets the maximum number of barcodes to detect\nValue in the range from 1 to 20")]
        public short BarcodeMaxSearchPriorities { get; set; }

        [Option("barcode_not_found", Required = false, Default = false, HelpText = "Enables or Disables notification for not finding a barcode")]
        public bool BarcodeNotDetectionNotice { get; set; }

        [Option("barcode_region_left", Required = false, Default = (float)0, HelpText = "Sets the left edge position of the barcode detection area.\n 0 is the left edge of the page")]
        public float BarcodeRegionLeft { get; set; }

        [Option("barcode_region_top", Required = false, Default = (float)0, HelpText = "Sets the top edge position of the barcode detection area.\n 0 is the top edge of the page")]
        public float BarcodeRegionTop { get; set; }

        [Option("barcode_region_length", Required = false, Default = (float)0, HelpText = "Sets the vertical length of the barcode detection area.\n 0 is the entire page")]
        public float BarcodeRegionLength { get; set; }

        [Option("barcode_region_width", Required = false, Default = (float)0, HelpText = "Sets the horizonal width of the barcode detection area.\n 0 is the entire page")]
        public float BarcodeRegionWidth { get; set; }

        [Option("BlankPageSkipSensitivity", Required = false, Default = (short)6, HelpText = "Sets the sensitivity to scan by skipping blank pages during continuous ADF scanning.")]
        public short BlankPageSkip { get; set; }

        [Option("BlankPageSkipMode", Required = false, Default = (short)0, HelpText = "Sets a criteria for detecting blank pages.\nWhen \"0 - Sensitivity\" is set, the sensitivity set for the BlankPageSkip property is used to detect blank pages.\r\nWhen \"1 - Black & White Dots Ratio\" is set, the black and white dots ratios set for the SkipBlackPage property and the SkipWhitePage property are used to detect blank pages.")]
        public short BlankPageSkipMode { get; set; }

        [Option("BlankPageOutput", Required = false, Default = (short)0, HelpText = "Sets whether blank pages are output or not when scanning. 1 - Output blank pages (must have blank page detection sensitivity set > 0/n2 - Don't output blank pages")]
        public short BlankPageNotice { get; set; }

        [Option("guid", Required = false, Default = "", HelpText = "Sets the GUID identifier for the scan")]
        public string guid { get; set; }

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
            scanner_control.FileCounterEx = FileCounterEx;
            scanner_control.ScanContinue = ScanContinue;
            scanner_control.ScanContinueMode = ScanContinueMode;
            scanner_control.BarcodeDetection = BarcodeDetection;
            scanner_control.BarcodeType = BarcodeType;
            scanner_control.BarcodeDirection = BarcodeDirection;
            scanner_control.BarcodeMaxSearchPriorities = BarcodeMaxSearchPriorities;
            scanner_control.BarcodeNotDetectionNotice = BarcodeNotDetectionNotice;
            scanner_control.BarcodeRegionLeft = BarcodeRegionLeft;
            scanner_control.BarcodeRegionTop = BarcodeRegionTop;
            scanner_control.BarcodeRegionLength = BarcodeRegionLength;
            scanner_control.BarcodeRegionWidth = BarcodeRegionWidth;
            scanner_control.BlankPageSkip = BlankPageSkip;
            scanner_control.BlankPageSkipMode = BlankPageSkipMode;
            scanner_control.BlankPageNotice = BlankPageNotice;
        }
    }
}
