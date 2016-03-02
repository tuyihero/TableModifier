using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TableConstruct
{
    public class TableGlobalConfig : INotifyPropertyChanged
    {
        #region 唯一实例
        private static TableGlobalConfig _Instance = null;
        public static TableGlobalConfig Instance 
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new TableGlobalConfig();
                return _Instance;
            }
        }

        private TableGlobalConfig()
        {
 
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

        #region 属性
        private string _TemplatePath;
        public string TemplatePath 
        {
            get
            {
                return _TemplatePath;
            }
            set
            {
                _TemplatePath = value;
                OnPropertyChanged("TemplatePath");
            }
        }


        private string _CodePath;
        public string CodePath
        {
            get
            {
                return _CodePath;
            }
            set
            {
                _CodePath = value;
                OnPropertyChanged("CodePath");
            }
        }

        private string _CodeTablePath;
        public string CodeTablePath
        {
            get
            {
                return _CodeTablePath;
            }
            set
            {
                _CodeTablePath = value;
                OnPropertyChanged("CodeTablePath");
            }
        }

        private string _ResTablePath;
        public string ResTablePath
        {
            get
            {
                return _ResTablePath;
            }
            set
            {
                _ResTablePath = value;
                OnPropertyChanged("ResTablePath");
            }
        }
        #endregion

        #region 逻辑

        public void Init()
        {
            ReadConfig.ReadAll();
            if (string.IsNullOrEmpty(_TemplatePath))
            {
                TemplatePath = TableGlobalDefine.ELEMENT_DEFAULT_TEMPLATE_PATH;
            }
        }

        public void SavePath()
        {
            WriteConfig.WriteAll();
        }

        #endregion
    }
}
