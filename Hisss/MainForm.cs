//Copyright(c) 2024 Nicolas "Fly" Sheridan
//This code is licensed under MIT license (see LICENSE.txt for details)
using System.Configuration;

namespace Hisss
{
    public partial class MainForm : Form
    {
        public MainForm(Configuration config)
        {
            InitializeComponent();
            Setup_Scanner(config);
        }

        private void On_Form_Load(object sender, EventArgs e)
        {
            int status;
            int ErrorCode;
            //Open the scanner (method)
            status = axFiScn1.StartScan(this.Handle.ToInt32());
            //An error occurred during a scan
            if (status == -1)
            {
                //Display the error information
                ErrorCode = axFiScn1.ErrorCode;
                MessageBox.Show("An error occurred during a scan.\nError code: 0x" + ErrorCode.ToString("X8"));
            }
            //Close the scanner (method)
            axFiScn1.CloseScanner(this.Handle.ToInt32());
        }

        private void Setup_Scanner(Configuration scanner_config)
        {
            axFiScn1.OpenScanner2(this.Handle.ToInt32());

            scanner_config.Apply(axFiScn1);
        }
    }
}
