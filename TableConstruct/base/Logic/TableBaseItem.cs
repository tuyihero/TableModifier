using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TableConstruct
{
    public class TableBaseItem : INotifyPropertyChanged, IDataErrorInfo
    {
        #region 接口 INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }

            _WriteFlag = true;
            if (_ParentCollection != null)
            {
                _ParentCollection.WriteFlag = true;
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

        #region 显示属性

        protected string _Name;

        [StringEmpty]
        [StringNotDefault]
        public String Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool _IsShowError = false;
        public bool IsShowError
        {
            get
            {
                return _IsShowError;
            }
            set
            {
                _IsShowError = value;
                OnPropertyChanged("IsShowError");
            }
        }

        private string _ErrorMsg = "";
        public string ErrorMsg
        {
            get
            {
                return _ErrorMsg;
            }
            set
            {
                _ErrorMsg = value;
                OnPropertyChanged("ErrorMsg");
            }
        }
        #endregion

        #region 逻辑属性

        protected TableBaseCollection _ParentCollection = null;
        public TableBaseCollection ParentCollection 
        {
            get
            {
                return _ParentCollection;
            }
            set
            {
                _ParentCollection = value;
            }
        }

        public bool _WriteFlag = false;
        public bool WriteFlag
        {
            get
            {
                return _WriteFlag;
            }
            set
            {
                _WriteFlag = value;

                if (_WriteFlag)
                {
                    if (_ParentCollection != null)
                    {
                        _ParentCollection.WriteFlag = true;
                    }
                }
            }
        }
        #endregion
    }
}
