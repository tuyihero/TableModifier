using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TableConstruct;

namespace TableContent
{
    public class ContentRow : TableBaseItem, ITableDisplay
    {
        #region 属性

        private ContentItem _IDItem = null;
        private ContentItem _NameItem = null;
        private ContentItem _DescItem = null;
        public string ID
        {
            get
            {
                return _IDItem.Value;
            }
        }

        public ContentItemCollection _ContentItems = new ContentItemCollection();
        public ContentItemCollection ContentItems
        {
            get
            {
                return _ContentItems;
            }
        }

        public bool IsInit = false;
        private ContentFile _ContentFile = null;
        public ContentFile ContentFile
        {
            get
            {
                return _ContentFile;
            }
        }
        //public ContentItems ContentItems = new ContentItems();

        #endregion

        public ContentRow(ContentFile contentFile)
        {
            _ContentFile = contentFile;
        }

        #region 方法

        public void AddContentItem(ContentItem item)
        {
            if (item.ItemConstruct.ItemCode == ConstructConfig.NEW_FILE_DEFAULT_ID_CODE)
            {
                _IDItem = item;

                Name = _IDItem.Value;
            }
            else if (item.ItemConstruct.ItemCode == ConstructConfig.NEW_FILE_DEFAULT_NAME_CODE)
            {
                _NameItem = item;

                //默认的 id的初始化在name之前
                if (_IDItem != null)
                {
                    if (ContentConfig.IsContentIDInvalid(_IDItem.Value))
                    {
                        Name = _IDItem.Value + "." + _NameItem.Value;
                    }
                    else
                    {
                        Name = _IDItem.Value;
                    }
                }
            }
            else if (item.ItemConstruct.ItemCode == ConstructConfig.NEW_FILE_DEFAULT_DESC_CODE)
            {
                _DescItem = item;
            }

            ContentItems.AddNewItem(item);
        }

        public string[] GetItemsStr()
        {
            string[] items = new string[ContentItems.Count];
            for (int i = 0; i < ContentItems.Count; ++i)
            {
                var contentItem = ContentItems[i] as ContentItem;
                //items[i] = "\"" + ContentItems[i].Value.ToString() + "\"";
                items[i] = contentItem.Value.ToString();
            }

            return items;
        }

        public void SetItemShowError(bool itemShowError)
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

            _ContentFile.SetRowShowError(IsShowError);
        }

        //表索引的反向
        private List<ContentItem> _RowReferenceItems = new List<ContentItem>();
        public void AddReferenceItem(ContentItem contentItem)
        {
            if (!_RowReferenceItems.Contains(contentItem))
            {
                _RowReferenceItems.Add(contentItem);
            }
        }

        public void DelReferenceItem(ContentItem contentItem)
        {
            if (_RowReferenceItems.Contains(contentItem))
            {
                _RowReferenceItems.Remove(contentItem);
            }
        }

        public void ModifyRowID(string oldValue, string newValue)
        {
            if (_IDItem != null && _NameItem != null)
            {
                Name = newValue + "." + _NameItem.Value;
            }

            foreach (ContentItem contentItem in _RowReferenceItems)
            {
                if (contentItem.ItemConstruct.ItemType2.Count == 1)
                {
                    contentItem.Value = newValue;
                }
                else
                {
                    contentItem.Value = ContentItem.GetComboValue(_ContentFile.Name, newValue);
                }
            }
        }

        public void ModifyRowName(string oldValue, string newValue)
        {
            if (_IDItem != null && _NameItem != null)
            {
                Name = _IDItem.Value + "." + newValue;
            }
        }
        #endregion

        #region 私有

        private bool CheckError()
        {
            IsShowError = false;
            for (int i = 0; i < ContentItems.Count; ++i)
            {
                var contentItem = ContentItems[i] as ContentItem;
                if (contentItem.IsShowError)
                {
                    IsShowError = true;
                    break;
                }
            }
            return IsShowError;
        }

        #endregion

        #region 接口ITableDisplay

        public string DisplayName
        {
            get
            {
                return _IDItem.Value + "." + _NameItem.Value;
            }
        }

        public string DisplayTips
        {
            get
            {
                return _DescItem.Value;
            }
        }

        public string WriteValue
        {
            get
            {
                return _IDItem.Value;
            }
        }

        #endregion

    }
}
