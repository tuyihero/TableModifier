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
        private string _ProjectPath;
        public string ProjectPath
        {
            get
            {
                return _ProjectPath;
            }
            set
            {
                _ProjectPath = value;
                _CodePath = _ProjectPath + TableGlobalDefine.ELEMENT_DEFAULT_CODE_PATH;
                _ConstructTablePath = _ProjectPath + TableGlobalDefine.ELEMENT_DEFAULT_CONSTRUCT_PATH;
                _ResTablePath = _ProjectPath + TableGlobalDefine.ELEMENT_DEFAULT_RESOURCE_PATH;
                OnPropertyChanged("ProjectPath");
            }
        }

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

        private string _ConstructTablePath;
        public string ConstructTablePath
        {
            get
            {
                return _ConstructTablePath;
            }
            set
            {
                _ConstructTablePath = value;
                OnPropertyChanged("ConstructTablePath");
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

        private string _SelectedProject;
        public string SelectedProject
        {
            get
            {
                return _SelectedProject;
            }
            set
            {
                _SelectedProject = value;
                OnPropertyChanged("SelectedProject");
            }
        }

        private List<string> _ProjectFileNames = new List<string>();
        public List<string> ProjectFileNames
        {
            get
            {
                return _ProjectFileNames;
            }
        }
        #endregion

        #region 逻辑

        public void Init()
        {
            ReadConfig.ReadAllFiles();
        }

        public void InitProject()
        {
            ReadConfig.ReadProjectConfig(SelectedProject);
            if (string.IsNullOrEmpty(_TemplatePath))
            {
                TemplatePath = TableGlobalDefine.ELEMENT_DEFAULT_TEMPLATE_PATH;
            }
        }

        public void SavePath()
        {
            WriteConfig.WriteProjectConfig(SelectedProject);
        }

        #endregion
    }
}
