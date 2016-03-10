using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace KeyChanger
{
    public class API
    {
        #region 消息定义
        public const int MM_JOY1MOVE = 0x3A0;
        public const int MM_JOY2MOVE = 0x3A1;
        public const int MM_JOY1ZMOVE = 0x3A2;
        public const int MM_JOY2ZMOVE = 0x3A3;
        public const int MM_JOY1BUTTONDOWN = 0x3B5;
        public const int MM_JOY2BUTTONDOWN = 0x3B6;
        public const int MM_JOY1BUTTONUP = 0x3B7;
        public const int MM_JOY2BUTTONUP = 0x3B8;
        #endregion

        #region 按钮定义

        /* constants used with JOYINFO and JOYINFOEX structures and MM_JOY* messages */
        public const int  JOY_BUTTON1         = 0x0001;
        public const int  JOY_BUTTON2         = 0x0002;
        public const int  JOY_BUTTON3         = 0x0004;
        public const int  JOY_BUTTON4         = 0x0008;
        public const int  JOY_BUTTON1CHG      = 0x0100;
        public const int  JOY_BUTTON2CHG      = 0x0200;
        public const int  JOY_BUTTON3CHG      = 0x0400;
        public const int  JOY_BUTTON4CHG      = 0x0800;

        /* constants used with JOYINFOEX */
        public const long  JOY_BUTTON5         = 0x00000010L;
        public const long  JOY_BUTTON6         = 0x00000020L;
        public const long  JOY_BUTTON7         = 0x00000040L;
        public const long  JOY_BUTTON8         = 0x00000080L;
        public const long  JOY_BUTTON9         = 0x00000100L;
        public const long  JOY_BUTTON10        = 0x00000200L;
        public const long  JOY_BUTTON11        = 0x00000400L;
        public const long  JOY_BUTTON12        = 0x00000800L;
        public const long  JOY_BUTTON13        = 0x00001000L;
        public const long  JOY_BUTTON14        = 0x00002000L;
        public const long  JOY_BUTTON15        = 0x00004000L;
        public const long  JOY_BUTTON16        = 0x00008000L;
        public const long  JOY_BUTTON17        = 0x00010000L;
        public const long  JOY_BUTTON18        = 0x00020000L;
        public const long  JOY_BUTTON19        = 0x00040000L;
        public const long  JOY_BUTTON20        = 0x00080000L;
        public const long  JOY_BUTTON21        = 0x00100000L;
        public const long  JOY_BUTTON22        = 0x00200000L;
        public const long  JOY_BUTTON23        = 0x00400000L;
        public const long  JOY_BUTTON24        = 0x00800000L;
        public const long  JOY_BUTTON25        = 0x01000000L;
        public const long  JOY_BUTTON26        = 0x02000000L;
        public const long  JOY_BUTTON27        = 0x04000000L;
        public const long  JOY_BUTTON28        = 0x08000000L;
        public const long  JOY_BUTTON29        = 0x10000000L;
        public const long  JOY_BUTTON30        = 0x20000000L;
        public const long  JOY_BUTTON31        = 0x40000000L;
        public const long  JOY_BUTTON32        = 0x80000000L;

        /* constants used with JOYINFOEX structure */
        public const int  JOY_POVCENTERED         = 65535;
        public const int  JOY_POVFORWARD          = 0;
        public const int  JOY_POVRIGHT            = 9000;
        public const int  JOY_POVBACKWARD         = 18000;
        public const int  JOY_POVLEFT             = 27000;
        public const int JOY_POVFORWARDLEFT = 31500;
        public const int JOY_POVFORWARDRIGHT = 4500;
        public const int JOY_POVBACKWARDLEFT = 22500;
        public const int JOY_POVBACKWARDRIGHT = 13500;
        public const int JOY_MOVE_X_ZERO = 0x7FFF;
        public const int JOY_MOVE_Y_ZERO = 0x7FFE;
        public const int JOY_MOVE_Z_ZERO = 0x7FFF;

        public const long  JOY_RETURNX             = 0x00000001L;
        public const long  JOY_RETURNY             = 0x00000002L;
        public const long  JOY_RETURNZ             = 0x00000004L;
        public const long  JOY_RETURNR             = 0x00000008L;
        public const long  JOY_RETURNU             = 0x00000010L;    /* axis 5 */
        public const long  JOY_RETURNV             = 0x00000020L;     /* axis 6 */
        public const long  JOY_RETURNPOV           = 0x00000040L;
        public const long  JOY_RETURNBUTTONS       = 0x00000080L;
        public const long  JOY_RETURNRAWDATA       = 0x00000100L;
        public const long  JOY_RETURNPOVCTS        = 0x00000200L;
        public const long  JOY_RETURNCENTERED      = 0x00000400L;
        public const long  JOY_USEDEADZONE         = 0x00000800L;
        public const long JOY_RETURNALL = (JOY_RETURNX | JOY_RETURNY | JOY_RETURNZ |
                                                      JOY_RETURNR | JOY_RETURNU | JOY_RETURNV |
                                                      JOY_RETURNPOV | JOY_RETURNBUTTONS);
        public const long  JOY_CAL_READALWAYS      = 0x00010000L;
        public const long  JOY_CAL_READXYONLY      = 0x00020000L;
        public const long  JOY_CAL_READ3           = 0x00040000L;
        public const long  JOY_CAL_READ4           = 0x00080000L;
        public const long  JOY_CAL_READXONLY       = 0x00100000L;
        public const long  JOY_CAL_READYONLY       = 0x00200000L;
        public const long  JOY_CAL_READ5           = 0x00400000L;
        public const long  JOY_CAL_READ6           = 0x00800000L;
        public const long  JOY_CAL_READZONLY       = 0x01000000L;
        public const long  JOY_CAL_READRONLY       = 0x02000000L;
        public const long  JOY_CAL_READUONLY       = 0x04000000L;
        public const long  JOY_CAL_READVONLY       = 0x08000000L;

        /* joystick ID constants */
        public const int  JOYSTICKID1         = 0;
        public const int  JOYSTICKID2         = 1;

        /* joystick driver capabilites */
        public const int  JOYCAPS_HASZ            = 0x0001;
        public const int  JOYCAPS_HASR            = 0x0002;
        public const int  JOYCAPS_HASU            = 0x0004;
        public const int  JOYCAPS_HASV            = 0x0008;
        public const int  JOYCAPS_HASPOV          = 0x0010;
        public const int  JOYCAPS_POV4DIR         = 0x0020;
        public const int  JOYCAPS_POVCTS          = 0x0040;
        #endregion

        #region 错误号定义
        /// <summary>
        /// 没有错误
        /// </summary>
        public const int JOYERR_NOERROR = 0;
        /// <summary>
        /// 参数错误
        /// </summary>
        public const int JOYERR_PARMS = 165;
        /// <summary>
        /// 无法正常工作
        /// </summary>
        public const int JOYERR_NOCANDO = 166;
        /// <summary>
        /// 操纵杆未连接 
        /// </summary>
        public const int JOYERR_UNPLUGGED = 167;
        #endregion

        /// <summary>
        /// 游戏手柄的参数信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct JOYCAPS
        {
            public ushort wMid;
            public ushort wPid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szPname;
            public int wXmin;
            public int wXmax;
            public int wYmin;
            public int wYmax;
            public int wZmin;
            public int wZmax;
            public int wNumButtons;
            public int wPeriodMin;
            public int wPeriodMax;
            public int wRmin;
            public int wRmax;
            public int wUmin;
            public int wUmax;
            public int wVmin;
            public int wVmax;
            public int wCaps;
            public int wMaxAxes;
            public int wNumAxes;
            public int wMaxButtons;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szRegKey;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szOEMVxD;
        }

        /// <summary>
        /// 游戏手柄的信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct JoyInfoEx 
        {
            public uint dwSize;                /* size of structure */
            public uint dwFlags;               /* flags to indicate what to return */
            public uint dwXpos;                /* x position */
            public uint dwYpos;                /* y position */
            public uint dwZpos;                /* z position */
            public uint dwRpos;                /* rudder/4th axis position */
            public uint dwUpos;                /* 5th axis position */
            public uint dwVpos;                /* 6th axis position */
            public uint dwButtons;             /* button states */
            public uint dwButtonNumber;        /* current button number pressed */
            public uint dwPOV;                 /* point of view state */
            public uint dwReserved1;           /* reserved for communication between winmm & driver */
            public uint dwReserved2;           /* reserved for future expansion */
        }


        /// <summary>
        /// 检查系统是否配置了游戏端口和驱动程序。如果返回值为零，表明不支持操纵杆功能。如果返回值不为零，则说明系统支持游戏操纵杆功能。
        /// </summary>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joyGetNumDevs();

        /// <summary>
        /// 获取某个游戏手柄的参数信息
        /// </summary>
        /// <param name="uJoyID">指定游戏杆(0-15)，它可以是JOYSTICKID1或JOYSTICKID2</param>
        /// <param name="pjc"></param>
        /// <param name="cbjc">JOYCAPS结构的大小</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joyGetDevCaps(int uJoyID, ref JOYCAPS pjc, int cbjc);

        /// <summary>
        /// 向系统申请捕获某个游戏杆并定时将该设备的状态值通过消息发送到某个窗口 
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="uJoyID">指定游戏杆(0-15)，它可以是JOYSTICKID1或JOYSTICKID2</param>
        /// <param name="uPeriod">每隔给定的轮询间隔就给应用程序发送有关游戏杆的信息。这个参数是以毫妙为单位的轮询频率。</param>
        /// <param name="fChanged">是否允许程序当操纵杆移动一定的距离后才接受消息</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joySetCapture(IntPtr hWnd, int uJoyID, int uPeriod, bool fChanged);

        /// <summary>
        /// 释放操纵杆的捕获
        /// </summary>
        /// <param name="uJoyID">指定游戏杆(0-15)，它可以是JOYSTICKID1或JOYSTICKID2</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joyReleaseCapture(int uJoyID);

        /// <summary>
        /// 获取操作杆消息
        /// </summary>
        /// <param name="uJoyID">指定游戏杆(0-15)，它可以是JOYSTICKID1或JOYSTICKID2</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joyGetPosEx(int uJoyID, ref JoyInfoEx joyInfo);
    }
}
