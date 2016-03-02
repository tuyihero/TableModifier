using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UITemplate.Controls
{
    public class FilePathControl : Control
    {

        #region 属性

        private TextBox _InputText = null;


        #endregion

        #region UI

        public FilePathControl()
        {
            this.DefaultStyleKey = typeof(FilePathControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _InputText = GetTemplateChild("InputText") as TextBox;
            //_InputText.LostFocus += OnInputTextKeyDown;

            Button fileExplore = GetTemplateChild("OpenFileExplore") as Button;
            fileExplore.Click += OpenFileExplore;

            _InputText.Focus();

        }

        private void TextLostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void OpenFileExplore(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                _InputText.Text = fbd.SelectedPath;
            }
        }

        public string GetPath()
        {
            return _InputText.Text;
        }
        #endregion

    }
}
