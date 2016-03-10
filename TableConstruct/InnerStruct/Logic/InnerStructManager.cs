using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableConstruct
{
    public class InnerStructManager
    {
         #region 唯一实例

        private static InnerStructManager _Instance = null;
        public static InnerStructManager Instance 
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new InnerStructManager();
                return _Instance;
            }
        }

        private InnerStructManager()
        {
 
        }

        #endregion

        #region logic

        public TableBaseCollection InnerStructInfoCollection = new TableBaseCollection();

        public List<string> ModifyFiles = new List<string>();
        public List<string> RemoveFiles = new List<string>();

        public InnerStructInfo CreateNewEnum(string name)
        {
            if (InnerStructInfoCollection.GetByName(name) != null)
                return null;

            InnerStructInfo enumInfo = new InnerStructInfo();
            enumInfo.Name = name;
            InnerStructInfoCollection.AddNewItem(enumInfo);

            return enumInfo;
        }

        public void AddEnum(InnerStructInfo enumInfo)
        {
            InnerStructInfoCollection.AddNewItem(enumInfo);
        }

        public void RemoveFile(string name)
        {
            InnerStructInfoCollection.RemoveByName(name);
            RemoveFiles.Add(name);
        }

        public void RenameFile(string orgName, string newName)
        {
            InnerStructInfo file = InnerStructInfoCollection.GetByName(orgName) as InnerStructInfo;
            if (file != null)
            {
                file.Name = newName;
                RemoveFiles.Add(orgName);
            }
        }
        #endregion
    }
}
