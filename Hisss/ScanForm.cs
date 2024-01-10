//Copyright(c) 2024 Nicolas "Fly" Sheridan
//This code is licensed under MIT license (see LICENSE.txt for details)
using System;
using System.Configuration;
using System.Resources;
using System.Windows;
namespace Hisss
{
    public partial class ScanForm : Form
    {
        public ScanForm(Configuration config)
        {
            InitializeComponent();
            Setup_Scanner(config);
            Scan();
        }
        private void Setup_Scanner(Configuration scanner_config)
        {
            //Open the scanner connection
            axFiScn1.OpenScanner2(this.Handle.ToInt32());

            //Apply default, commandline, and json config settings
            scanner_config.Apply(axFiScn1);
        }

        private void Scan()
        {
            int status;

            //start the scan
            status = axFiScn1.StartScan(this.Handle.ToInt32());

            //An error occurred during a scan
            if (status == -1)
            {
                //translate error code from documentation and display
                ResourceManager rm = new ResourceManager(typeof(ErrorCodes));
                MessageBox.Show(rm.GetString(axFiScn1.ErrorCode.ToString()) + "\nErrorCode: 0x" + axFiScn1.ErrorCode.ToString("X8"), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }

            //Close the scanner (method)
            axFiScn1.CloseScanner(this.Handle.ToInt32());
        }

    }
}
