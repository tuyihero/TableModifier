using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableConstruct
{
    public class ConstructFold
    {
        #region 唯一实例
        private static ConstructFold _Instance = null;
        public static ConstructFold Instance 
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new ConstructFold();
                return _Instance;
            }
        }

        private ConstructFold()
        {
 
        }
        #endregion

        #region 初始化
        private bool _Init = false;
        public void InitFold()
        {
            if (string.IsNullOrEmpty(TableGlobalConfig.Instance.SelectedProject))
            {
                return;
            }

            if (!_Init)
            {
                TableGlobalConfig.Instance.InitProject();
                ReadConstruct.ReadProject();
                ReadEnuminfos.ReadProject();
                ReadInnerStruct.ReadProject();
                _Init = true;
            }
        }

        public void Save()
        {
            WriteConstruct.WriteProject();
            WriteEnuminfos.WriteProject();
            WriteInnerStruct.WriteProject();
        }
        #endregion

        #region Logic

        public TableBaseCollection ConstructFiles = new TableBaseCollection();

        public List<string> ModifyFiles = new List<string>();
        public List<string> RemoveFiles = new List<string>();

        public void AddFile(ConstructFile file)
        {
            ConstructFiles.AddNewItem(file);
        }

        public ConstructFile CreateNewFile(string name)
        {
            ConstructFile file = new ConstructFile(name);
            AddFile(file);
            ModifyFiles.Add(file.Name);

            return file;
        }

        public void RemoveFile(string name)
        {
            ConstructFiles.RemoveByName(name);
            RemoveFiles.Add(name);
        }

        public void RenameFile(string orgName, string newName)
        {
            ConstructFile file = ConstructFiles.GetByName(orgName) as ConstructFile;
            if (file != null)
            {
                if (string.IsNullOrEmpty(file.OldName))
                {
                    file.OldName = file.Name;
                    //WriteConstruct.WriteFileOldName(file, file.OldName);
                }
                file.Name = newName;
                RemoveFiles.Add(orgName);
                ModifyFiles.Add(newName);
            }
        }
        #endregion
    }
}
