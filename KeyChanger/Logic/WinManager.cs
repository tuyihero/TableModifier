using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Reflection;

namespace KeyChanger
{
    public class WinManager
    {
        #region 静态
        
        private static WinManager _Instance = null;
        public static WinManager Instance 
        {
            get
            {
                if(_Instance == null)
                    _Instance = new WinManager();
                return _Instance;
            }
        }

        private WinManager()
        {
            
        }

        #endregion

        #region 应用列表
        
        WinInfoItemCollection _SysWinTitles = new WinInfoItemCollection();
        public WinInfoItemCollection GetSysWinTitles()
        {
            _SysWinTitles.Clear();

            //user32.EnumWindowsProc ewp = new user32.EnumWindowsProc(ADA_EnumWindowsProc);
            //user32.EnumWindows(ewp, 0);
            Process[] localAll = Process.GetProcesses();
            foreach (Process localProcess in localAll)
            {
                if (!string.IsNullOrEmpty(localProcess.MainWindowTitle))
                {
                    WinInfoItem winInfo = new WinInfoItem();
                    winInfo.Title = localProcess.MainWindowTitle;

                    winInfo.HWnd = localProcess.MainWindowHandle;
                    try
                    {
                        Icon icon = Icon.ExtractAssociatedIcon(localProcess.MainModule.FileName);
                        if (icon != null)
                        {
                            winInfo.Icon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(icon.Handle, new Int32Rect(0, 0, icon.Width, icon.Height), BitmapSizeOptions.FromEmptyOptions());
                        }
                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(localProcess.MainWindowTitle + " e:" + e.ToString());
                    }

                    _SysWinTitles.Add(winInfo);
                }
            }

            return _SysWinTitles;
        }

        #endregion

        #region Key and Mouse
        private KeyHook _KeyHook = null;
        public KeyHook KeyHook
        {
            get
            {
                if (_KeyHook == null)
                {
                    _KeyHook = new KeyHook();
                }
                return _KeyHook;
            }
        }

        public void AddKeyDown(EventHandler<KeyboardHookArgs> keyCallBack)
        {
            if (KeyHook.IsKeyUpEmpty() && KeyHook.IsKeyDownEmpty())
                KeyHook.SetWinHook();

            KeyHook.KeyDown += keyCallBack;
        }

        public void RemoveKeyDown(EventHandler<KeyboardHookArgs> keyCallBack)
        {
            KeyHook.KeyDown -= keyCallBack;

            if (KeyHook.IsKeyDownEmpty() && KeyHook.IsKeyDownEmpty())
                KeyHook.RemoveWinHook();
        }

        public void AddKeyUp(EventHandler<KeyboardHookArgs> keyCallBack)
        {
            if (KeyHook.IsKeyUpEmpty() && KeyHook.IsKeyDownEmpty())
                KeyHook.SetWinHook();

            KeyHook.KeyUp += keyCallBack;
        }

        public void RemoveKeyUp(EventHandler<KeyboardHookArgs> keyCallBack)
        {
            KeyHook.KeyUp -= keyCallBack;

            if (KeyHook.IsKeyDownEmpty() && KeyHook.IsKeyDownEmpty())
                KeyHook.RemoveWinHook();
        }

        #endregion

        #region joysticks

        private IntPtr _AppHWnd = IntPtr.Zero;
        public IntPtr AppHWnd
        {
            get
            {
                if (_AppHWnd == IntPtr.Zero)
                {
                    Process curProcess = Process.GetCurrentProcess();
                    _AppHWnd = curProcess.MainWindowHandle;
                }
                return _AppHWnd;
            }
        }

        private Joystick _Joystick = null;
        public Joystick Joystick 
        { 
            get 
            {
                if (_Joystick == null)
                {
                    _Joystick = new Joystick();
                }
                return _Joystick; 
            }
        }

        public void LoadJoystick()
        {
            _Joystick.Register(AppHWnd, API.JOYSTICKID1);
            //_Joystick.Register(AppHWnd, API.JOYSTICKID2);
            //_Joystick.StartGetJoyStick();
        }

        public void UnloadJoystick()
        {
            //_Joystick.UnRegister(API.JOYSTICKID1);
            //_Joystick.UnRegister(API.JOYSTICKID2);
            //_Joystick.RemoveHook();
            //_Joystick.StopGetJoyStick();
        }

        public static JoystickButtons GetClickButton(JoystickButtons btns)
        {
            if ((btns & JoystickButtons.UP) == JoystickButtons.UP)
                return JoystickButtons.UP;

            if ((btns & JoystickButtons.Down) == JoystickButtons.Down)
                return JoystickButtons.Down;

            if ((btns & JoystickButtons.Left) == JoystickButtons.Left)
                return JoystickButtons.Left;

            if ((btns & JoystickButtons.Right) == JoystickButtons.Right)
                return JoystickButtons.Right;

            if ((btns & JoystickButtons.B1) == JoystickButtons.B1)
                return JoystickButtons.B1;

            if ((btns & JoystickButtons.B2) == JoystickButtons.B2)
                return JoystickButtons.B2;

            if ((btns & JoystickButtons.B3) == JoystickButtons.B3)
                return JoystickButtons.B3;

            if ((btns & JoystickButtons.B4) == JoystickButtons.B4)
                return JoystickButtons.B4;

            if ((btns & JoystickButtons.B5) == JoystickButtons.B5)
                return JoystickButtons.B5;

            if ((btns & JoystickButtons.B6) == JoystickButtons.B6)
                return JoystickButtons.B6;

            if ((btns & JoystickButtons.B7) == JoystickButtons.B7)
                return JoystickButtons.B7;

            if ((btns & JoystickButtons.B8) == JoystickButtons.B8)
                return JoystickButtons.B8;

            if ((btns & JoystickButtons.B9) == JoystickButtons.B9)
                return JoystickButtons.B9;

            if ((btns & JoystickButtons.B10) == JoystickButtons.B10)
                return JoystickButtons.B10;

            if ((btns & JoystickButtons.POVUp) == JoystickButtons.POVUp)
                return JoystickButtons.POVUp;

            if ((btns & JoystickButtons.POVDown) == JoystickButtons.POVDown)
                return JoystickButtons.POVDown;

            if ((btns & JoystickButtons.POVLeft) == JoystickButtons.POVLeft)
                return JoystickButtons.POVLeft;

            if ((btns & JoystickButtons.POVRight) == JoystickButtons.POVRight)
                return JoystickButtons.POVRight;

            if ((btns & JoystickButtons.MoveZDown) == JoystickButtons.MoveZDown)
                return JoystickButtons.MoveZDown;

            if ((btns & JoystickButtons.MoveZUp) == JoystickButtons.MoveZUp)
                return JoystickButtons.MoveZUp;

            return JoystickButtons.None;
        }

        public void AddJoystickClick(EventHandler<JoystickEventArgs> joyCallBack)
        {
            if (Joystick.IsClickEventEmpty())
                LoadJoystick();

            Joystick.Click += joyCallBack;
        }

        public void RemoveJoystickClick(EventHandler<JoystickEventArgs> joyCallBack)
        {
            Joystick.Click -= joyCallBack;

            if (Joystick.IsClickEventEmpty())
                UnloadJoystick();
        }

        public void AddJoystickMove(EventHandler<JoystickEventArgs> joyCallBack)
        {
            if (Joystick.IsClickEventEmpty())
                LoadJoystick();

            Joystick.Move += joyCallBack;
        }

        public void RemoveJoystickMove(EventHandler<JoystickEventArgs> joyCallBack)
        {
            Joystick.Move -= joyCallBack;

            if (Joystick.IsClickEventEmpty())
                UnloadJoystick();
        }

        #endregion

        #region hook and sendevent

        public void HookInput()
        {
            //注册手柄事件
            AddJoystickMove(JoyStickClick);

            //注册键盘事件
            AddKeyDown(KeyboardClick);
            AddKeyUp(KeyboardClick);
        }

        public void RemoveHookInput()
        {
            RemoveJoystickMove(JoyStickClick);

            RemoveKeyDown(KeyboardClick);
            RemoveKeyUp(KeyboardClick);
        }

        public void HookJoyStick()
        {
            //注册手柄事件
            AddJoystickMove(JoyStickClick);
        }

        public void RemoveHookJoyStick()
        {
            RemoveJoystickMove(JoyStickClick);
        }

        public void HookKeyboard()
        {
            //注册键盘事件
            AddKeyDown(KeyboardClick);
            AddKeyUp(KeyboardClick);
        }

        public void RemoveHookKeyboard()
        {
            RemoveKeyDown(KeyboardClick);
            RemoveKeyUp(KeyboardClick);
        }

        public void JoyStickClick(object sender, JoystickEventArgs e)
        {
            byte[] keyState = new byte[256];
            user32.GetKeyboardState(keyState);

            MatchAndSend(e.Buttons, keyState);
        }

        public void KeyboardClick(object sender, KeyboardHookArgs e)
        {
            byte[] keyState = new byte[256];
            user32.GetKeyboardState(keyState);

            if (MatchAndSend(JoystickButtons.None, keyState))
            {
                KeyHook.IsCallNextHook = false;
            }
        }

        public bool MatchAndSend(JoystickButtons joyBtns, byte[] keyState)
        {
            List<KeyChangeItem> _ChangeKeys = new List<KeyChangeItem>();
            KeyChangeManager.Instance.MatchKeyItem(joyBtns, keyState, ref _ChangeKeys);
            if (_ChangeKeys.Count == 0)
                return false;

            bool isSendDown = false;
            for (int i = _ChangeKeys.Count - 1; i >= 0; --i)
            {
                //这里应该设置匹配优先级，不过先不管了,假定往后的配置优先级更高
                //if(i == _ChangeKeys.Count -1)
                //{
                //    if (SendKeyItem(_ChangeKeys[i]))
                //    {
                //        isSendDown = true;
                //    }
                //}
                //else if(_ChangeKeys[i].IsDown)
                {
                    if (SendKeyItem(_ChangeKeys[i]))
                    {
                        isSendDown = true;
                    }
                }
            }

            
            return isSendDown;
        }

        private bool SendKeyItem(KeyChangeItem keyItem)
        {
            //没有按下，发送按下
            if (!keyItem.IsDown)
            {
                foreach (KeyStoreInfo keyStore in keyItem.ToStores)
                {
                    //先只支持键盘
                    try
                    {
                        if (!keyStore.IsJoystick())
                        {
                            
                            byte keyCode = (byte)keyStore._Keyboard;
                            user32.keybd_event(keyCode, ScanCode(keyCode), 0, 0);
                        }
                    }
                    catch (Exception e)
                    { }
                }
                keyItem.IsDown = true;
                return true;
            }
            //已经按下，发送弹起
            else
            {
                foreach (KeyStoreInfo keyStore in keyItem.ToStores)
                {
                    //先只支持键盘
                    try
                    {
                        if (!keyStore.IsJoystick())
                        {
                            byte keyCode = (byte)keyStore._Keyboard;
                            user32.keybd_event(keyCode, ScanCode(keyCode), user32.KEYEVENTF_KEYUP, 0);
                            //user32.keybd_event(keyCode, ScanCode(keyCode), 0, 0);


                            //byte keyCode2 = (byte)Keys.NumPad0;
                            //user32.keybd_event(keyCode2, ScanCode(keyCode2), 0, 0);
                            //user32.keybd_event(keyCode2, ScanCode(keyCode2), user32.KEYEVENTF_KEYUP, 0);
                        }
                    }
                    catch (Exception e)
                    { }
                }
                keyItem.IsDown = false;
                return false;
            }
        }

        private byte ScanCode(byte pKey)
        {
            byte result = (byte)user32.MapVirtualKey(pKey, 0);
            return result;
        }
        #endregion

    }
}
