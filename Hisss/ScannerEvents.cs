using AxFiScnLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hisss
{
    public static class ScannerEvents
    {
        public static void ScanToFile(object sender, _DFiScnEvents_ScanToFileEvent e)
        {
            LogWriter.Log("Scan successfull");
            LogWriter.Log("File name: " + e.fileName);
        }
    }
}
