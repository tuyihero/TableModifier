using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace KeyChanger
{
    public class coredll
    {
        public const int MAX_PATH = 260;
        public const int PROCESS_ALL_ACCESS = 0x000F0000 | 0x00100000 | 0xFFF;
        [DllImport("coredll.dll")]
        public extern static int GetWindowThreadProcessId(IntPtr hWnd, ref int lpdwProcessId);
        [DllImport("coredll.dll")]
        public extern static IntPtr OpenProcess(int fdwAccess, int fInherit, int IDProcess);
        [DllImport("coredll.dll")]
        public extern static bool TerminateProcess(IntPtr hProcess, int uExitCode);
        [DllImport("coredll.dll")]
        public extern static bool CloseHandle(IntPtr hObject);
        [DllImport("Coredll.dll", EntryPoint = "GetModuleFileName")]
        public static extern uint GetModuleFileName(IntPtr hModule, [Out] StringBuilder lpszFileName, int nSize);
        [DllImport("coredll.dll", SetLastError = true)]
        public static extern IntPtr ExtractIconEx(string fileName, int index, ref IntPtr hIconLarge, ref IntPtr hIconSmall, uint nIcons);
    }
}
