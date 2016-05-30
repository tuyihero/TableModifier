using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableConstruct
{
    public class EnumManager
    {
         #region 唯一实例

        private static EnumManager _Instance = null;
        public static EnumManager Instance 
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new EnumManager();
                return _Instance;
            }
        }

        private EnumManager()
        {
 
        }

        #endregion

        #region logic

        public TableBaseCollection EnumInfoCollection = new TableBaseCollection();

        public List<string> ModifyFiles = new List<string>();
        public List<string> RemoveFiles = new List<string>();

        public EnumInfo CreateNewEnum(string name)
        {
            if (EnumInfoCollection.GetByName(name) != null)
                return null;

            EnumInfo enumInfo = new EnumInfo();
            enumInfo.InitNewEnum();
            enumInfo.Name = name;
            EnumInfoCollection.AddNewItem(enumInfo);

            return enumInfo;
        }

        public EnumInfo GetEnum(string name)
        {
            TableBaseItem tableBase = EnumInfoCollection.GetByName(name);
            return tableBase as EnumInfo;
        }

        public void AddEnum(EnumInfo enumInfo)
        {
            EnumInfoCollection.AddNewItem(enumInfo);
        }

        public void RemoveFile(string name)
        {
            EnumInfoCollection.RemoveByName(name);
            RemoveFiles.Add(name);
        }

        public void RenameFile(string orgName, string newName)
        {
            TableBaseItem file = EnumInfoCollection.GetByName(orgName);
            if (file != null)
            {
                file.Name = newName;
                RemoveFiles.Add(orgName);
            }
        }
        #endregion
    }
}
