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
        private static int BarcodeCount = 0;
        public static void ScanToFile(object sender, _DFiScnEvents_ScanToFileEvent e)
        {
            LogWriter.Log("Scan successfull");
            LogWriter.Log("File Name: " + e.fileName);
            BarcodeCount = 0;
        }

        public static void DetectBarcode(object sender, _DFiScnEvents_DetectBarcodeEvent e)
        {
            LogWriter.Log("Barcode " + BarcodeCount + ": " + e.barcodeText);
            BarcodeCount++;
        }
    }
}
