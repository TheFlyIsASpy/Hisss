using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hisss
{
    public class ConsoleRegister
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;
        public static void RegisterGUIConsoleWriter()
        {
            AttachConsole(ATTACH_PARENT_PROCESS);
        }
    }
}
