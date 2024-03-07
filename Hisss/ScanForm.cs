//Copyright(c) 2024 Nicolas "Fly" Sheridan
//This code is licensed under MIT license (see LICENSE.txt for details)
using AxFiScnLib;
using System;
using System.Configuration;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows;
namespace Hisss
{
    public partial class ScanForm : Form
    {
        public ScanForm(Configuration config)
        {
            LogWriter.Log("Initializing scanner control form");
            InitializeComponent();
            Setup_Scanner(config);
            Scan();
        }
        private void Setup_Scanner(Configuration config)
        {
            LogWriter.Log("Opening scanner...");
            axFiScn1.OpenScanner2(this.Handle.ToInt32());

            LogWriter.Log("Applying arguments and default settings to scanner...");
            config.Apply(axFiScn1);

            LogWriter.Log("Adding events to scanner...");
            axFiScn1.ScanToFile += ScannerEvents.ScanToFile;
            axFiScn1.DetectBarcode += ScannerEvents.DetectBarcode;

            LogWriter.Log("GUID: " + config.guid);
            int extIndex = axFiScn1.FileName.IndexOf(".");
            if (extIndex != -1)
            {
                string ext = axFiScn1.FileName.Substring(axFiScn1.FileName.IndexOf("."));
                string filename = axFiScn1.FileName.Replace(ext, "_" + config.guid + "_" + ext);
                axFiScn1.FileName = filename;
            }
            else
            {
                axFiScn1.FileName = axFiScn1.FileName + "_" + config.guid + "_";
            }       
        }

        private void Scan()
        {
            int status;

            LogWriter.Log("Starting Scan...");
            status = axFiScn1.StartScan(this.Handle.ToInt32());

            if (status == -1)
            {
                LogWriter.Log("Scan failed, translating error code");
                ResourceManager rm = new ResourceManager(typeof(ErrorCodes));
                string? ErrorMessage = rm.GetString(axFiScn1.ErrorCode.ToString());
                ErrorMessage ??= "Error message not found";
                LogWriter.Log("Error code: 0x" + axFiScn1.ErrorCode.ToString("X8"));
                LogWriter.Log("Error translated message: " + ErrorMessage);
                LogWriter.Log("Generating error popup");
                MessageBox.Show(ErrorMessage + "\nErrorCode: 0x" + axFiScn1.ErrorCode.ToString("X8"), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            
            LogWriter.Log("Closing scanner");
            axFiScn1.CloseScanner(this.Handle.ToInt32());
        }
    }
}
