using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;

namespace KeyChanger
{

    [StructLayout(LayoutKind.Sequential)]
    public class KeyboardHookStruct
    {
        public int vkCode;   //表示一个在1到254间的虚似键盘码 
        public int scanCode;   //表示硬件扫描码 
        public int flags;
        public int time;
        public int dwExtraInfo;
    }

    public class KeyboardHookArgs : EventArgs
    {
        public Keys Key;
    }

    public class KeyHook
    {
        #region 事件
        /// <summary>
        /// 按钮被弹起
        /// </summary>
        public event EventHandler<KeyboardHookArgs> KeyUp;
        public bool IsKeyUpEmpty()
        {
            return KeyUp == null;
        }
        /// <summary>
        /// 按钮已被按下
        /// </summary>
        public event EventHandler<KeyboardHookArgs> KeyDown;
        public bool IsKeyDownEmpty()
        {
            return KeyDown == null;
        }

        /// <summary>
        /// 触发按钮弹起事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnKeyUp(KeyboardHookArgs e)
        {
            EventHandler<KeyboardHookArgs> h = this.KeyUp;
            if (h != null) h(this, e);
        }
        /// <summary>
        /// 触发按钮按下事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnKeyDown(KeyboardHookArgs e)
        {
            EventHandler<KeyboardHookArgs> h = this.KeyDown;
            if (h != null) h(this, e);
        }
        #endregion

        #region Hook

        private user32.HookProc KeyboardHookProcedure = null;
        private int HookID = 0;
        public bool IsCallNextHook { get; set; }

        public void SetWinHook()
        {
            KeyboardHookProcedure = new user32.HookProc(KeyboardHookProc);

            IsCallNextHook = true;

            HookID = user32.SetWindowsHookEx(user32.WH_KEYBOARD_ALL,
                KeyboardHookProcedure,
                Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().ManifestModule),
                0);
        }

        public void RemoveWinHook()
        {
            if (HookID > 0)
            {
                user32.UnhookWindowsHookEx(HookID);
                HookID = 0;
            }
        }

        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KeyboardHookStruct keyboardStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                KeyboardHookArgs args = new KeyboardHookArgs();
                args.Key = (Keys)keyboardStruct.vkCode;
                if (wParam == user32.WM_KEYDOWN || wParam == user32.WM_SYSKEYDOWN)
                {
                    OnKeyDown(args);
                }
                else if (wParam == user32.WM_KEYUP || wParam == user32.WM_SYSKEYUP)
                {
                    OnKeyUp(args);
                }

            }

            if (IsCallNextHook)
            {
                return user32.CallNextHookEx(HookID, nCode, wParam, lParam);
            }
            else
            {
                IsCallNextHook = true;
                return 1;
            }
        }

        #endregion
    }
}
