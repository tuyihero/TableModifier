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
    public class DialogGetString : Window
    {

        #region 属性

        private TextBox _InputText = null;
        private string _DefaultText;

        private string _InputString = "";

        #endregion

        #region UI

        public DialogGetString()
        {
            this.DefaultStyleKey = typeof(DialogGetString);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _InputText = GetTemplateChild("InputText") as TextBox;
            _InputText.KeyDown += OnInputTextKeyDown;

            Button btnOK = GetTemplateChild("BtnOK") as Button;
            btnOK.Click += Button_OK;

            Button btnCancel = GetTemplateChild("BtnCancel") as Button;
            btnCancel.Click += Button_Cancel;

            _InputText.Focus();

            if (!string.IsNullOrEmpty(_DefaultText))
            {
                _InputText.Text = _DefaultText;
            }
        }

        private void Button_OK(object sender, RoutedEventArgs e)
        {
            _InputString = _InputText.Text;
            DialogResult = true;
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            _InputString = "";
            DialogResult = false;
        }

        private void OnInputTextKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                _InputString = _InputText.Text;
                DialogResult = true;
            }
        }


        #endregion

        #region Logic

        public string GetResult()
        {
            return _InputString;
        }

        public void SetDefaultText(string defaultText)
        {
            _DefaultText = defaultText;
        }
        #endregion
    }
}
