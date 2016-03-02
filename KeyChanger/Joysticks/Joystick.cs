using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Threading;

namespace KeyChanger
{
    public class Joystick : IDisposable
    {
        #region 事件定义
        /// <summary>
        /// 按钮被单击
        /// </summary>
        public event EventHandler<JoystickEventArgs> Click;
        public bool IsClickEventEmpty()
        {
            return (Click == null && Move == null);
        }

        public event EventHandler<JoystickEventArgs> Move;
        
        /// <summary>
        /// 按钮被弹起
        /// </summary>
        public event EventHandler<JoystickEventArgs> ButtonUp;
        /// <summary>
        /// 按钮已被按下
        /// </summary>
        public event EventHandler<JoystickEventArgs> ButtonDown;
        /// <summary>
        /// 触发单击事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnClick(JoystickEventArgs e)
        {
            EventHandler<JoystickEventArgs> h = this.Click;
            if (h != null) h(this, e);
        }

        protected void OnMove(JoystickEventArgs e)
        {
            EventHandler<JoystickEventArgs> h = this.Move;
            if (h != null) h(this, e);
        }
        /// <summary>
        /// 触发按钮弹起事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnButtonUp(JoystickEventArgs e)
        {
            EventHandler<JoystickEventArgs> h = this.ButtonUp;
            if (h != null) h(this, e);
        }
        /// <summary>
        /// 触发按钮按下事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnButtonDown(JoystickEventArgs e)
        {
            EventHandler<JoystickEventArgs> h = this.ButtonDown;
            if (h != null) h(this, e);
        }
        /// <summary>
        /// 是否已注册消息
        /// </summary>
        private bool IsRegister = false;

        /// <summary>
        /// 窗口win32
        /// </summary>
        private HwndSource _HWndSource = null;
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="hWnd">需要捕获手柄消息的窗口</param>
        /// <param name="joystickId">要捕获的手柄Id</param>
        public bool Register(IntPtr hWnd, int joystickId)
        {
            bool flag = false;
            int result = 0;
            API.JOYCAPS caps = new API.JOYCAPS();
            if (API.joyGetNumDevs() != 0)
            {
                //拥有手柄.则判断手柄状态
                result = API.joyGetDevCaps(joystickId, ref caps, Marshal.SizeOf(typeof(API.JOYCAPS)));
                if (result == API.JOYERR_NOERROR)
                {
                    //手柄处于正常状态
                    flag = true;
                }

            }

            if (flag)
            {
                //注册消息
                if (!this.IsRegister)
                {
                    _HWndSource = HwndSource.FromHwnd(hWnd);
                    _HWndSource.AddHook(WndProc);
                    //Application.AddMessageFilter(this);
                }
                this.IsRegister = true;

                result = API.joySetCapture(hWnd, joystickId, caps.wPeriodMin * 2, false);
                if (result != API.JOYERR_NOERROR)
                {
                    flag = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <param name="joystickId"></param>
        public void UnRegister(int joystickId)
        {
            if (this.IsRegister)
            {
                int result = API.joyReleaseCapture(joystickId);
            }
        }

        public void RemoveHook()
        {
            _HWndSource.RemoveHook(WndProc);
            IsRegister = false;
        }

        
        #endregion

        #region 消息处理
        #region IMessageFilter 成员

        private JoystickButtons _LastButtonsInfo = JoystickButtons.None;
        /// <summary>
        /// 处理系统消息.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //没有关注的，直接返回
            if (IsClickEventEmpty())
                return IntPtr.Zero;

            bool flag = false;
            if (hwnd != IntPtr.Zero && (wParam != IntPtr.Zero || lParam != IntPtr.Zero))
            {
                if (msg != API.MM_JOY1MOVE && msg != API.MM_JOY2MOVE)
                    return IntPtr.Zero;

                Action<JoystickEventArgs> action = null;
                JoystickButtons buttons = JoystickButtons.None;
                int joystickId = -1;
                API.JoyInfoEx joyInfo1 = new API.JoyInfoEx();
                joyInfo1.dwSize = (uint)Marshal.SizeOf(typeof(API.JoyInfoEx));
                joyInfo1.dwFlags = (int)API.JOY_RETURNALL;

                if (API.joyGetPosEx(API.JOYSTICKID1, ref joyInfo1) == API.JOYERR_NOERROR)
                {
                    buttons = GetButtonFromJoyInfoEx(joyInfo1);
                    action = this.OnClick;
                }
                joystickId = msg == API.MM_JOY1MOVE ? API.JOYSTICKID1 : API.JOYSTICKID2;

                //switch (msg)
                //{
                //    case API.MM_JOY1MOVE:
                //    case API.MM_JOY2MOVE:
                //        //单击事件
                //        //buttons = GetButtonsStateFromMessageParam(wParam.ToInt64(), lParam.ToInt64());
                //        if (API.joyGetPosEx(API.JOYSTICKID1, ref joyInfo1) == API.JOYERR_NOERROR)
                //        {
                //            buttons = GetButtonFromJoyInfoEx(joyInfo1);
                //            action = this.OnClick;
                //        }
                //        joystickId = msg == API.MM_JOY1MOVE ? API.JOYSTICKID1 : API.JOYSTICKID2;
                //        break;
                //    case API.MM_JOY1BUTTONDOWN:
                //    case API.MM_JOY2BUTTONDOWN:
                //        //按钮被按下
                //        //buttons = GetButtonsPressedStateFromMessageParam(wParam.ToInt32(), lParam.ToInt32());
                //        if (API.joyGetPosEx(API.JOYSTICKID1, ref joyInfo1) == API.JOYERR_NOERROR)
                //        {
                //            buttons = GetButtonFromJoyInfoEx(joyInfo1);
                //            action = this.OnButtonDown;
                //        }
                //        joystickId = msg == API.MM_JOY1BUTTONDOWN ? API.JOYSTICKID1 : API.JOYSTICKID2;
                //        break;
                //    case API.MM_JOY1BUTTONUP:
                //    case API.MM_JOY2BUTTONUP:
                //        //按钮被弹起
                //        //buttons = GetButtonsPressedStateFromMessageParam(wParam.ToInt32(), lParam.ToInt32());
                //        if (API.joyGetPosEx(API.JOYSTICKID1, ref joyInfo1) == API.JOYERR_NOERROR)
                //        {
                //            buttons = GetButtonFromJoyInfoEx(joyInfo1);
                //            action = this.OnButtonUp;
                //        }
                //        joystickId = msg == API.MM_JOY1BUTTONUP ? API.JOYSTICKID1 : API.JOYSTICKID2;
                //        break;

                //    case API.MM_JOY1ZMOVE:
                //    case API.MM_JOY2ZMOVE:
                //        break;
                //}
                if (action != null && joystickId != -1 && buttons != JoystickButtons.None)
                {
                    //阻止消息继续传递
                    flag = true;
                    //触发事件
                    action(new JoystickEventArgs(joystickId, buttons));
                }

                if (joystickId != -1 && _LastButtonsInfo != buttons)
                {
                    //无论是否有按键按下，发送move消息
                    this.OnMove(new JoystickEventArgs(joystickId, buttons));
                    _LastButtonsInfo = buttons;

                }
                
            }
            handled = flag;
            return IntPtr.Zero;
        }
        #endregion
        /// <summary>
        /// 根据消息的参数获取按钮的状态值
        /// </summary>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private JoystickButtons GetButtonsStateFromMessageParam(long wParam, long lParam)
        {
            JoystickButtons buttons = JoystickButtons.None;
            if (wParam != 0)
            {
                int i = 1 + 1;
            }
            if ((wParam & API.JOY_BUTTON1) == API.JOY_BUTTON1)
            {
                buttons |= JoystickButtons.B1;
            }
            if ((wParam & API.JOY_BUTTON2) == API.JOY_BUTTON2)
            {
                buttons |= JoystickButtons.B2;
            }
            if ((wParam & API.JOY_BUTTON3) == API.JOY_BUTTON3)
            {
                buttons |= JoystickButtons.B3;
            }
            if ((wParam & API.JOY_BUTTON4) == API.JOY_BUTTON4)
            {
                buttons |= JoystickButtons.B4;
            }
            if ((wParam & API.JOY_BUTTON5) == API.JOY_BUTTON5)
            {
                buttons |= JoystickButtons.B5;
            }
            if ((wParam & API.JOY_BUTTON6) == API.JOY_BUTTON6)
            {
                buttons |= JoystickButtons.B6;
            }
            if ((wParam & API.JOY_BUTTON7) == API.JOY_BUTTON7)
            {
                buttons |= JoystickButtons.B7;
            }
            if ((wParam & API.JOY_BUTTON8) == API.JOY_BUTTON8)
            {
                buttons |= JoystickButtons.B8;
            }
            if ((wParam & API.JOY_BUTTON9) == API.JOY_BUTTON9)
            {
                buttons |= JoystickButtons.B9;
            }
            if ((wParam & API.JOY_BUTTON10) == API.JOY_BUTTON10)
            {
                buttons |= JoystickButtons.B10;
            }

            GetXYButtonsStateFromLParam(lParam, ref buttons);

            return buttons;
        }
        /// <summary>
        /// 根据消息的参数获取按钮的按压状态值
        /// </summary>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private JoystickButtons GetButtonsPressedStateFromMessageParam(int wParam, int lParam)
        {
            JoystickButtons buttons = JoystickButtons.None;
            if ((wParam & API.JOY_BUTTON1CHG) == API.JOY_BUTTON1CHG)
            {
                buttons |= JoystickButtons.B1;
            }
            if ((wParam & API.JOY_BUTTON2CHG) == API.JOY_BUTTON2CHG)
            {
                buttons |= JoystickButtons.B2;
            }
            if ((wParam & API.JOY_BUTTON3CHG) == API.JOY_BUTTON3CHG)
            {
                buttons |= JoystickButtons.B3;
            }
            if ((wParam & API.JOY_BUTTON4CHG) == API.JOY_BUTTON4CHG)
            {
                buttons |= JoystickButtons.B4;
            }

            GetXYButtonsStateFromLParam(lParam, ref buttons);

            return buttons;
        }
        /// <summary>
        /// 获取X,Y轴的状态
        /// </summary>
        /// <param name="lParam"></param>
        /// <param name="buttons"></param>
        private void GetXYButtonsStateFromLParam(long lParam, ref JoystickButtons buttons)
        {
            //处理X,Y轴
            int x = (int)(lParam & 0x0000FFFE);                //低16位存储X轴坐标
            int y = (int)((lParam & 0xFFFE0000) >> 16); //高16位存储Y轴坐标(不直接移位是为避免0xFFFFFF)
            int m = 0x7FFE;                            //中心点的值,
            if (x > m)
            {
                buttons |= JoystickButtons.Right;
            }
            else if (x < m)
            {
                buttons |= JoystickButtons.Left;
            }
            if (y > m)
            {
                buttons |= JoystickButtons.Down;
            }
            else if (y < m)
            {
                buttons |= JoystickButtons.UP;
            }
        }

        private JoystickButtons GetButtonFromJoyInfoEx(API.JoyInfoEx joyInfo)
        {
            JoystickButtons buttons = JoystickButtons.None;

            //pov
            if (joyInfo.dwPOV == API.JOY_POVFORWARD)
            {
                buttons |= JoystickButtons.POVUp;
            }
            else if (joyInfo.dwPOV == API.JOY_POVBACKWARD)
            {
                buttons |= JoystickButtons.POVDown;
            }
            else if (joyInfo.dwPOV == API.JOY_POVLEFT)
            {
                buttons |= JoystickButtons.POVLeft;
            }
            else if (joyInfo.dwPOV == API.JOY_POVRIGHT)
            {
                buttons |= JoystickButtons.POVRight;
            }
            else if (joyInfo.dwPOV == API.JOY_POVFORWARDLEFT)
            {
                buttons |= JoystickButtons.POVUp;
                buttons |= JoystickButtons.POVLeft;
            }
            else if (joyInfo.dwPOV == API.JOY_POVFORWARDRIGHT)
            {
                buttons |= JoystickButtons.POVUp;
                buttons |= JoystickButtons.POVRight;
            }
            else if (joyInfo.dwPOV == API.JOY_POVBACKWARDLEFT)
            {
                buttons |= JoystickButtons.POVDown;
                buttons |= JoystickButtons.POVLeft;
            }
            else if (joyInfo.dwPOV == API.JOY_POVBACKWARDRIGHT)
            {
                buttons |= JoystickButtons.POVDown;
                buttons |= JoystickButtons.POVRight;
            }

            //move 
            if (joyInfo.dwYpos > API.JOY_MOVE_Y_ZERO)
            {
                buttons |= JoystickButtons.Down;
            }
            else if (joyInfo.dwYpos < API.JOY_MOVE_Y_ZERO)
            {
                buttons |= JoystickButtons.UP;
            }

            if (joyInfo.dwXpos > API.JOY_MOVE_X_ZERO)
            {
                buttons |= JoystickButtons.Right;
            }
            else if (joyInfo.dwXpos < API.JOY_MOVE_X_ZERO)
            {
                buttons |= JoystickButtons.Left;
            }

            if (joyInfo.dwZpos > API.JOY_MOVE_Z_ZERO)
            {
                buttons |= JoystickButtons.MoveZUp;
            }
            else if (joyInfo.dwZpos < API.JOY_MOVE_Z_ZERO)
            {
                buttons |= JoystickButtons.MoveZDown;
            }

            //normal button
            if ((joyInfo.dwButtons & API.JOY_BUTTON1) == API.JOY_BUTTON1)
            {
                buttons |= JoystickButtons.B1;
            }
            if ((joyInfo.dwButtons & API.JOY_BUTTON2) == API.JOY_BUTTON2)
            {
                buttons |= JoystickButtons.B2;
            }
            if ((joyInfo.dwButtons & API.JOY_BUTTON3) == API.JOY_BUTTON3)
            {
                buttons |= JoystickButtons.B3;
            }
            if ((joyInfo.dwButtons & API.JOY_BUTTON4) == API.JOY_BUTTON4)
            {
                buttons |= JoystickButtons.B4;
            }
            if ((joyInfo.dwButtons & API.JOY_BUTTON5) == API.JOY_BUTTON5)
            {
                buttons |= JoystickButtons.B5;
            }
            if ((joyInfo.dwButtons & API.JOY_BUTTON6) == API.JOY_BUTTON6)
            {
                buttons |= JoystickButtons.B6;
            }
            if ((joyInfo.dwButtons & API.JOY_BUTTON7) == API.JOY_BUTTON7)
            {
                buttons |= JoystickButtons.B7;
            }
            if ((joyInfo.dwButtons & API.JOY_BUTTON8) == API.JOY_BUTTON8)
            {
                buttons |= JoystickButtons.B8;
            }
            if ((joyInfo.dwButtons & API.JOY_BUTTON9) == API.JOY_BUTTON9)
            {
                buttons |= JoystickButtons.B9;
            }
            if ((joyInfo.dwButtons & API.JOY_BUTTON10) == API.JOY_BUTTON10)
            {
                buttons |= JoystickButtons.B10;
            }

            return buttons;
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _HWndSource.RemoveHook(WndProc);
        }

        #endregion


        #region 后台线程定时获取形式

        private JoystickButtons _ButtonInfos = JoystickButtons.None;
        private Thread _JoyStickThread = null;

        //处理一号手柄，我只有一支懒得弄
        public void StartGetJoyStick()
        {
            _JoyStickThread = new Thread(GetJoyStickThread);
            _JoyStickThread.IsBackground = true;

            _JoyStickThread.Start();

            OnClick(new JoystickEventArgs(API.JOYSTICKID1, _ButtonInfos));
        }

        public void StopGetJoyStick()
        {
            if (_JoyStickThread != null)
            {
                _JoyStickThread.Abort();
                _JoyStickThread = null;
            }
        }

        public void GetJoyStickThread()
        {
            while (true)
            {
                API.JoyInfoEx joyInfo1 = new API.JoyInfoEx();
                joyInfo1.dwSize = (uint)Marshal.SizeOf(typeof(API.JoyInfoEx));
                joyInfo1.dwFlags = (int)API.JOY_RETURNALL;
                if (API.joyGetPosEx(API.JOYSTICKID1, ref joyInfo1) == API.JOYERR_NOERROR)
                {
                    _ButtonInfos = GetButtonFromJoyInfoEx(joyInfo1);
                    if (_ButtonInfos != JoystickButtons.None)
                        return;
                    //OnClick(new JoystickEventArgs(API.JOYSTICKID1, _ButtonInfos));
                }
                Thread.Sleep(10);
            }
        }
        #endregion
    }
}
