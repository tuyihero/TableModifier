using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TableConstruct
{
    public class ConstructFile : TableBaseItem
    {
        public TableBaseCollection ConstructItems = new TableBaseCollection();

        public ConstructFile(string name, bool createDefaultColumn = true)
        {
            Name = name;

            //新建表，自动添加ID，名称，描述项
            if (createDefaultColumn)
            {
                ConstructItem idItem = CreateNewItem(ConstructConfig.NEW_FILE_DEFAULT_ID_NAME);
                idItem.ItemCode = ConstructConfig.NEW_FILE_DEFAULT_ID_CODE;
                idItem.ItemType1 = ConstructConfig.NEW_FILE_DEFAULT_ID_TYPE1;
                if (idItem.ItemType2.Count != 0)
                {
                    idItem.ItemType2[0] = new TableBaseItem() { Name = ConstructConfig.NEW_FILE_DEFAULT_ID_TYPE2 };
                }
                else
                {
                    idItem.ItemType2.AddNewItem(new TableBaseItem() { Name = ConstructConfig.NEW_FILE_DEFAULT_ID_TYPE2 });
                }
                idItem.ItemDefault = ConstructConfig.NEW_FILE_DEFAULT_ID_DEFAULT;
                idItem.ItemRepeat = ConstructConfig.NEW_FILE_DEFAULT_ID_REPEAT;
                idItem.ItemReadOnly = true;

                ConstructItem nameItem = CreateNewItem(ConstructConfig.NEW_FILE_DEFAULT_NAME_NAME);
                nameItem.ItemCode = ConstructConfig.NEW_FILE_DEFAULT_NAME_CODE;
                nameItem.ItemType1 = ConstructConfig.NEW_FILE_DEFAULT_NAME_TYPE1;
                if (nameItem.ItemType2.Count != 0)
                {
                    nameItem.ItemType2[0] = new TableBaseItem() { Name = ConstructConfig.NEW_FILE_DEFAULT_NAME_TYPE2 };
                }
                else
                {
                    nameItem.ItemType2.AddNewItem(new TableBaseItem() { Name = ConstructConfig.NEW_FILE_DEFAULT_NAME_TYPE2 });
                }
                nameItem.ItemDefault = ConstructConfig.NEW_FILE_DEFAULT_NAME_DEFAULT;
                nameItem.ItemRepeat = ConstructConfig.NEW_FILE_DEFAULT_NAME_REPEAT;
                nameItem.ItemReadOnly = true;

                ConstructItem descItem = CreateNewItem(ConstructConfig.NEW_FILE_DEFAULT_DESC_NAME);
                descItem.ItemCode = ConstructConfig.NEW_FILE_DEFAULT_DESC_CODE;
                descItem.ItemType1 = ConstructConfig.NEW_FILE_DEFAULT_DESC_TYPE1;
                if (descItem.ItemType2.Count != 0)
                {
                    descItem.ItemType2[0] = new TableBaseItem() { Name = ConstructConfig.NEW_FILE_DEFAULT_DESC_TYPE2 };
                }
                else
                {
                    descItem.ItemType2.AddNewItem(new TableBaseItem() { Name = ConstructConfig.NEW_FILE_DEFAULT_DESC_TYPE2 });
                }
                descItem.ItemDefault = ConstructConfig.NEW_FILE_DEFAULT_DESC_DEFAULT;
                descItem.ItemRepeat = ConstructConfig.NEW_FILE_DEFAULT_DESC_REPEAT;
                descItem.ItemReadOnly = true;
            }
        }

        #region 方法

        public ConstructItem CreateNewItem()
        {
            return CreateNewItem("newItem");
        }

        public ConstructItem CreateNewItem(string name)
        {
            ConstructItem item = new ConstructItem(name);
            item.ItemType2.AddNewItem(new TableBaseItem());
            ConstructItems.AddNewItem(item);

            return item;
        }

        public string[] GetColumnTitles()
        {
            List<string> titles = new List<string>();
            foreach (ConstructItem constructItem in ConstructItems)
            {
                if (constructItem.ItemRepeat > 1)
                {
                    for (int i = 0; i < constructItem.ItemRepeat; ++i)
                    {
                        string repeatItem = constructItem.Name + (i + 1).ToString();
                        repeatItem = "\"" + repeatItem + "\"";
                        titles.Add(repeatItem);

                    }
                }
                else
                {
                    titles.Add("\"" + constructItem.Name + "\"");
                    //titles.Add(constructItem.Name);
                }
            }

            return titles.ToArray();
        }

        public bool IsNeedWrite()
        {
            foreach (ConstructItem constructItem in ConstructItems)
            {
                if (constructItem.ItemType2.WriteFlag)
                    return true;
            }
            return ConstructItems.WriteFlag | WriteFlag;
        }

        public void AlreadyWrite()
        {
            WriteFlag = false;
            ConstructItems.WriteFlag = false;

            foreach (ConstructItem constructItem in ConstructItems)
            {
                constructItem.ItemType2.WriteFlag = false;
            }
        }
        #endregion

        #region 显示属性

        protected string _Class;
        [StringEmpty]
        [StringNotDefault]
        public String Class
        {
            get { return _Class; }
            set
            {
                _Class = value;
                OnPropertyChanged("Class");
            }
        }

        protected string _Desc;
        [StringEmpty]
        [StringNotDefault]
        public String Desc
        {
            get { return _Desc; }
            set
            {
                _Desc = value;
                OnPropertyChanged("Desc");
            }
        }

        public string OldName { get; set; }
        #endregion

        #region 逻辑属性

        private bool _NeedReloadContent = false;
        public bool NeedReloadContent
        {
            get
            {
                return _NeedReloadContent;
            }
            set
            {
                _NeedReloadContent = value;
            }
        }

        #endregion

    }
}
