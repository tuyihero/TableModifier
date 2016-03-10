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
        // Fields
        private TextBox _InputText = null;

        // Methods
        public FilePathControl()
        {
            base.DefaultStyleKey = typeof(FilePathControl);
        }

        public string GetPath()
        {
            return this._InputText.Text;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._InputText = base.GetTemplateChild("InputText") as TextBox;
            Button templateChild = base.GetTemplateChild("OpenFileExplore") as Button;
            templateChild.Click += new RoutedEventHandler(this.OpenFileExplore);
            this._InputText.Focus();
        }

        private void OpenFileExplore(object sender, RoutedEventArgs e)
        {
            //FolderBrowserDialog dialog = new FolderBrowserDialog();
            //dialog.ShowDialog();
            //if (dialog.SelectedPath != string.Empty)
            //{
            //    this._InputText.Text = dialog.SelectedPath;
            //}
        }

        private void TextLostFocus(object sender, RoutedEventArgs e)
        {
        }
    }
}

