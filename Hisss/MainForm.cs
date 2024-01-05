namespace Hisss
{
    public partial class MainForm : Form
    {
        public MainForm(Configuration config)
        {
            config.apply(axFiScn1);
            InitializeComponent();
        }

        private void On_Form_Load(object sender, EventArgs e)
        {
            int status;
            int ErrorCode;
            //Open the scanner (method)
            axFiScn1.OpenScanner2(this.Handle.ToInt32()); //OpenScanner2 is recommended
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
    }
}
