using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;

namespace KeyChanger
{
    public class KeyChangeItem : INotifyPropertyChanged, IDataErrorInfo
    {
        #region 显示
        private KeyStoreInfoCollection _FromStores = new KeyStoreInfoCollection();
        public KeyStoreInfoCollection FromStores
        {
            get 
            {
                return _FromStores; 
            }
            set
            {
                _FromStores = value;
                OnPropertyChanged("FromStores");
            }
        }

        private KeyStoreInfoCollection _ToStores = new KeyStoreInfoCollection();
        public KeyStoreInfoCollection ToStores
        {
            get { return _ToStores; }
            set
            {
                _ToStores = value;
                OnPropertyChanged("ToStore");
            }
        }
        #endregion

        #region 数据

        private bool _IsDown = false;
        public bool IsDown
        {
            get { return _IsDown; }
            set { _IsDown = value; }
        }

        public bool IsMatchFrom(JoystickButtons joyBtns, byte[] keyState)
        {
            if (FromStores.Count == 0)
                return false;

            bool isMatch = false;
            foreach (KeyStoreInfo keyInfo in FromStores)
            {
                if (keyInfo.IsJoystick())
                {
                    if ((joyBtns & keyInfo._Joystick) == keyInfo._Joystick)
                    {
                        isMatch = true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    int idx = (int)keyInfo._Keyboard;
                    if (idx >= 0 && idx < 256)
                    {
                        if ((keyState[idx] & 0xff) == 1)
                        {
                            isMatch = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return isMatch;
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
