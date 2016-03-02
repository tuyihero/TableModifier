using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TableConstruct;

namespace TableContent
{
    public class ContentFile : TableBaseItem
    {
        #region 属性

        public ContentRowCollection ContentRow = new ContentRowCollection();

        private ConstructFile _ConstuctFile = null;
        public ConstructFile ConstructFile
        {
            get
            {
                return _ConstuctFile;
            }
        }

        public bool IsInit = false;
        #endregion

        #region 方法

        public ContentFile(ConstructFile construct)
        {
            _ConstuctFile = construct;
        }

        public void AddContentRow(ContentRow row)
        {
            ContentRow.AddNewItem(row);
        }

        public ContentRow GetRowByID(string id)
        {
            return ContentRow.GetRowByID(id);
        }

        public ContentRow GreateRow(string id)
        {
            if (ContentRow.ContainsID(id))
                return null;

            ContentRow row = new ContentRow(this);

            //初始化值
            foreach (ConstructItem constructItem in _ConstuctFile.ConstructItems)
            {
                for (int i = 0; i < constructItem.ItemRepeat; ++i)
                {
                    ContentItem contentItem = new ContentItem(row, constructItem, i + 1);
                    if (constructItem.ItemCode == ConstructConfig.NEW_FILE_DEFAULT_ID_CODE)
                    {
                        contentItem.Value = id;
                    }
                    else
                    {
                        contentItem.Value = constructItem.ItemDefault;
                    }
                    row.AddContentItem(contentItem);
                }
            }

            AddContentRow(row);

            return row;
        }

        public bool IsNeedWrite()
        {
            foreach (ContentRow rowInfo in ContentRow)
            {
                if (rowInfo.WriteFlag | rowInfo._ContentItems.WriteFlag)
                    return true;
            }
            return WriteFlag;
        }

        public void AlreadyWrite()
        {
            foreach (ContentRow rowInfo in ContentRow)
            {
                rowInfo.WriteFlag = false;
                rowInfo._ContentItems.WriteFlag = false;
            }
            WriteFlag = false;
        }

        public void SetRowShowError(bool itemShowError)
        {
            if (IsInit)
            {
                if (itemShowError)
                {
                    IsShowError = true;
                }
            }
            else
            {
                if (itemShowError)
                {
                    IsShowError = true;
                }
                else
                {
                    CheckError();
                }
            }
        }
        #endregion

        #region 私有

        private bool CheckError()
        {
            IsShowError = false;
            for (int i = 0; i < ContentRow.Count; ++i)
            {
                var contentRow = ContentRow[i] as ContentRow;
                if (contentRow.IsShowError)
                {
                    IsShowError = true;
                    break;
                }
            }
            return IsShowError;
        }

        #endregion
    }
}
