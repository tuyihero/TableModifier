using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KeyChanger
{
    public class KeyStoreInfo : INotifyPropertyChanged, IDataErrorInfo
    {
        #region 逻辑
        public Keys _Keyboard = Keys.None;
        public JoystickButtons _Joystick = JoystickButtons.None;

        public bool IsJoystick()
        {
            if (_Joystick != JoystickButtons.None)
                return true;

            return false;
        }

        public void SetStoreValue(Keys key)
        {
            _Joystick = JoystickButtons.None;
            _Keyboard = key;
            RefreshBtnName();
        }

        public void SetStoreValue(JoystickButtons joystick)
        {
            _Keyboard = Keys.None;
            _Joystick = joystick;
            RefreshBtnName();
        }

        public void RefreshBtnName()
        {
            if (IsJoystick())
            {
                BtnName = _Joystick.ToString();
            }
            else
            {
                BtnName = _Keyboard.ToString();
            }
        }
        #endregion

        #region 显示
        private string _BtnName = "";
        public string BtnName
        {
            get 
            {
                return _BtnName; 
            }
            set
            {
                _BtnName = value;
                OnPropertyChanged("BtnName");
            }
        }

        #endregion

        #region 接口 INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

        #region 接口 IDataErrorInfo 数据校验
        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                var vc = new ValidationContext(this, null, null);
                vc.MemberName = columnName;
                var res = new List<ValidationResult>();
                var result = Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this, null), vc, res);
                if (res.Count > 0)
                {
                    return string.Join(Environment.NewLine, res.Select(r => r.ErrorMessage).ToArray());
                }
                return string.Empty;
            }
        }

        protected class StringEmpty : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var name = value as string;

                if (String.IsNullOrEmpty(name))
                {
                    return false;
                }
                return true;
            }

            public override string FormatErrorMessage(string name)
            {
                return "不能为空";
            }
        }

        protected class StringNotDefault : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                var name = value as string;

                if (name == "新建项")
                {
                    return false;
                }
                return true;
            }

            public override string FormatErrorMessage(string name)
            {
                return "请输入新建项的名字";
            }
        }

        #endregion
    }
}
