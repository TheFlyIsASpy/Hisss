//Copyright(c) 2024 Nicolas "Fly" Sheridan
//This code is licensed under MIT license (see LICENSE.txt for details)
using System;
using System.Configuration;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows;
namespace Hisss
{
    public partial class ScanForm : Form
    {

        LogWriter lw;
        public ScanForm(Configuration config, LogWriter lw)
        {
            InitializeComponent();
            this.lw = lw;
            Setup_Scanner(config);
            Scan();
        }
        private void Setup_Scanner(Configuration scanner_config)
        {
            lw.Log("Opening scanner...");
            //Open the scanner connection
            axFiScn1.OpenScanner2(this.Handle.ToInt32());

            lw.Log("Applying arguments and default settings to scanner...");
            //Apply default, commandline, and json config settings
            scanner_config.Apply(axFiScn1);
        }

        private void Scan()
        {
            int status;

            //start the scan
            lw.Log("Starting Scan...");
            status = axFiScn1.StartScan(this.Handle.ToInt32());

            //An error occurred during a scan
            if (status == -1)
            {
                lw.Log("Scan failed, translating error code");
                //translate error code from documentation and display
                ResourceManager rm = new ResourceManager(typeof(ErrorCodes));
                lw.Log("Generating error popup");
                MessageBox.Show(rm.GetString(axFiScn1.ErrorCode.ToString()) + "\nErrorCode: 0x" + axFiScn1.ErrorCode.ToString("X8"), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                lw.Log("Scan successfull");
            }


            //Close the scanner (method)
            lw.Log("Closing scanner");
            axFiScn1.CloseScanner(this.Handle.ToInt32());
        }

    }
}
