using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TableContent
{
    public class ShowRowParam
    {
        public ContentFile ShowContentFile;
        public ContentRow ShowContentRow;
    }

    public class TableContentManager
    {
        #region 唯一实例
        private static TableContentManager _Instance = null;
        public static TableContentManager Instance 
        {
            get 
            {
                if (_Instance == null)
                    _Instance = new TableContentManager();
                return _Instance;
            }
        }

        private TableContentManager()
        {
 
        }
        #endregion

        #region 属性

        public ContentFileCollection ContentFiles = new ContentFileCollection();

        public List<string> RemoveFiles = new List<string>();

        #endregion

        #region 方法

        public void AddContentFile(ContentFile file)
        {
            ContentFiles.AddNewItem(file);
        }

        public void InitFiles()
        {
            ContentFiles = new ContentFileCollection();
            ReadContent.ReadAll();

            foreach (ContentFile contentFile in ContentFiles)
            {
                foreach (ContentRow contentRow in contentFile.ContentRow)
                {
                    foreach (ContentItem contentItem in contentRow._ContentItems)
                    {
                        contentItem.CheckValidate();
                        contentItem.SetTableReference();
                    }
                }
            }
        }

        public void SaveFiles()
        {
            WriteContent.WriteAll();
        }

        public ContentFile GetFileByName(string name)
        {
            return ContentFiles.GetFileByName(name);
        }
        #endregion
    }
}
