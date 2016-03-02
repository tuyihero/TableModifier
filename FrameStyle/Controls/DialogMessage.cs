using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UITemplate.Controls
{
    public class DialogMessage
    {
        #region 获取输入字符
        public static string DialogString(string defaultText = "")
        {
            DialogGetString dialog = new DialogGetString();
            dialog.Owner = Application.Current.MainWindow;
            dialog.SetDefaultText(defaultText);

            Point point = Mouse.GetPosition(Application.Current.MainWindow);
            point = Application.Current.MainWindow.PointToScreen(point);
            dialog.Top = point.Y;
            dialog.Left = point.X;

            dialog.ShowDialog();

            return dialog.GetResult();
        }

        #endregion
    }
}
