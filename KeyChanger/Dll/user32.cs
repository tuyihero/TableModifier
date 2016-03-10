using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace KeyChanger
{
    public class user32
    {
        //窗口
        public delegate bool EnumWindowsProc(int hWnd, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowsProc ewp, int lParam);       

        [DllImport("user32.dll")]
        public static extern int GetWindowText(int hWnd, StringBuilder title, int size);
        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(int hWnd);
        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(int hWnd);
        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("USER32.DLL")]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        [DllImport("user32.dll")]
        public static extern Int32 SendMessage(int hwnd, UInt32 Msg, int wParam, int lParam);

        //钩子
        [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll ", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
        [DllImport("user32.dll ")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);
        [DllImport("user32.dll ")]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("user32.dll ")]
        public static extern int SetKeyboardState(byte[] pbKeyState);
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        [DllImport("user32.dll ")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, int lpdwProcessID);
        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, long lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, long lParam);
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        //
        [DllImport("Kernel32.dll")]
        public static extern int GetLastError();
        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        public const int WH_KEYBOARD = 2;
        public const int WH_KEYBOARD_ALL = 13; 

        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;

        public const int KEYEVENTF_KEYUP = 0x0002;
        #region 

        public const UInt32 KEY_Z = 0x5A;
        #endregion
    }
}
