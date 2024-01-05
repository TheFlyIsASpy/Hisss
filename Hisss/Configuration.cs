using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hisss
{
    public class Configuration
    {
        public bool ShowSourceUI = false;
        public bool Indicator = true;

        public short Resolution = 2;
        public short FileType = 4;
        public short PixelType = 1;
        public Configuration()
        {
        }

        public void Apply(AxFiScnLib.AxFiScn scanner_control)
        {
            scanner_control.Resolution = Resolution;
            scanner_control.FileType = FileType;
            scanner_control.PixelType = PixelType;

            scanner_control.ShowSourceUI = ShowSourceUI;
            scanner_control.Indicator = Indicator;
        }
    }
}
