using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace KeyChanger
{
    public class WinInfoItem : INotifyPropertyChanged, IDataErrorInfo
    {
        #region 逻辑属性

        public IntPtr HWnd { get; set; }

        #endregion

        #region 显示属性
        private string _Title;
        public String Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged("Title");
            }
        }

        private ImageSource _Icon;
        public ImageSource Icon
        {
            get { return _Icon; }
            set
            {
                _Icon = value;
                OnPropertyChanged("Icon");
            }
        }

        private string _Input;
        public string Input
        {
            get { return _Input; }
            set
            {
                _Input = value;
                OnPropertyChanged("Input");
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
